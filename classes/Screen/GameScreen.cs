using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public class GameScreen : Screen
{
    private List<Inventory> _allInventories;
    private bool _isClicked = false;
    private List<BaseObj> _allObjects;
    private List<BaseObj> _pendingRemovals;
    private Calendar _mainCalendar;
    private AlertList _alertList;
    private int _currentPersonDisplayPage = 0;
    private bool _peopleListOpen = false;

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        _allInventories = new List<Inventory>();
        _allObjects = new List<BaseObj>();
        _pendingRemovals = new List<BaseObj>();
        _mainCalendar = RunTime.currentCalendar;
        _mainCalendar.StartCalendar();
        _alertList = new AlertList();
    }

    public void QueueRemove(BaseObj obj)
    {
        if (obj == null) return;
        if (!_pendingRemovals.Contains(obj))
            _pendingRemovals.Add(obj);
    }
    public void AddBaseObj(BaseObj newObj)
    {
        _allObjects.Add(newObj);
    }

    public bool Build(Building building)
    {
        int buildingCount = 0;
        for(int i = 0; i < _allObjects.Count; i++){
            if(_allObjects[i] is Building && _allObjects[i].Name == building.Name && _allObjects[i].CanBeSeen){
                buildingCount ++;
            }
        }
        if(buildingCount >= building.CapacityLimit){
            AddMessage($"Cannot build more {building.Name}s. Limit reached.", AlertType.ERROR);
            return false;
        }else{
            _allObjects.Add(building);
            foreach(Inventory inventory in _allInventories)
            {
                if(inventory.Type == ResourceType.WOOD)
                {
                    inventory.TotalNum -= building.WoodCost;
                }
                else if(inventory.Type == ResourceType.STONE)
                {
                    inventory.TotalNum -= building.StoneCost;
                }
            }
            PlaySound(RunTime.buildSound);
            return true;
        }
            
    }

    public void AddInventory(Inventory newInventory)
    {
        _allInventories.Add(newInventory);
    }

    override public void Display()
    {
        if(IsMouseButtonPressed(MouseButton.Left))
        {   
          _isClicked = true;
        }

        if(_isClicked)
        {
            PlaySound(RunTime.clickSound);
            _isClicked = false;
        }
        
        Texture2D displayBg = MainBackground;
        DrawTexture(displayBg, 0, 0, Color.White);

        foreach(BaseObj eachObj in _allObjects)
        {
            eachObj.Draw();
            if(eachObj is WatchTower){
                Console.WriteLine("Drawing watchtower");
            }
        }

        BaseObj toShowDetails = null;

        foreach(BaseObj obj in _allObjects)
        {
            if (obj.IsSelected)
            {
                toShowDetails = obj;
                break;
            }
        }

        if(toShowDetails == null)
        {
            foreach(BaseObj obj in _allObjects)
            {
                Vector2 mousePos = GetMousePosition();
                if(mousePos.X > obj.X && mousePos.X < obj.X + obj.Width &&  mousePos.Y > obj.Y && mousePos.Y < obj.Y + obj.Height && IsMouseButtonPressed(MouseButton.Right)  && !obj.IsSelected)
                {
                    RunTime.detailsShown = true;
                    obj.IsSelected = true;
                }
            }

        }else if(toShowDetails != null)
        {
            toShowDetails.DisplayDetails();
        }

        _alertList.DisplayAllAlerts();

        if(Util.PeopleListOpen)
        {
            Util.PeopleList("People List", GetPersonLists());
        }
        AddIndicators();

        if (_pendingRemovals.Count > 0)
        {
            foreach (var r in _pendingRemovals)
            {
                _allObjects.Remove(r);
            }
            _pendingRemovals.Clear();
        }
    }

    public int GetPersonCount()
    {
        int count = 0;
        foreach(BaseObj obj in _allObjects)
        {
            if(obj is Person && !((Person)obj).IsFainted)
            {
                count ++;
            }
        }

        return count;
    }

    public List<Person> GetPersonLists(){
        List<Person> persons = new List<Person>();
        foreach (BaseObj obj in _allObjects)
        {
            if (obj is Person p && !p.IsFainted)
                persons.Add(p);
        }
        return persons;
    }

    public List<Building> GetBuildingLists()
    {
        List<Building> buildings = new List<Building>();
        foreach (BaseObj obj in _allObjects)
        {
            if (obj is Building b)
                buildings.Add(b);
        }
        return buildings;
    }

    public List<Enemy> GetEnemyLists()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (BaseObj obj in _allObjects)
        {
            if (obj is Enemy e)
                enemies.Add(e);
        }
        return enemies;
    }

    

    public int GetFoodCount()
    {
        int foodCount = 0;
        foreach(Inventory inventory in _allInventories)
        {
            if(inventory.Type == ResourceType.FOOD)
            {
                foodCount = inventory.TotalNum;
                break;
            }
        }
        return foodCount;
    }

    public int GetWoodCount()
    {
        int woodCount = 0;
        foreach(Inventory inventory in _allInventories)
        {
            if(inventory.Type == ResourceType.WOOD)
            {
                woodCount = inventory.TotalNum;
                break;
            }
        }
        return woodCount;
    }

    public int GetStoneCount()
    {
        int stoneCount = 0;
        foreach(Inventory inventory in _allInventories)
        {
            if(inventory.Type == ResourceType.STONE)
            {
                stoneCount = inventory.TotalNum;
                break;
            }
        }
        return stoneCount;
    }

    public int GetMeatCount()
    {
        int meatCount = 0;
        foreach(Inventory inventory in _allInventories)
        {
            if(inventory.Type == ResourceType.MEAT)
            {
                meatCount = inventory.TotalNum;
                break;
            }
        }
        return meatCount;
    }

    public int GetEnemyCount()
    {
        int enemyCount = 0;
        foreach(BaseObj obj in _allObjects)
        {
            if(obj is Enemy)
            {
                enemyCount ++;
            }
        }
        return enemyCount;
    }

    public Weather CurrentWeather()
    {
        return _mainCalendar.CurrentWeather;
    }   

    public Inventory GetInventory(ResourceType type)
    {
        foreach(Inventory inventory in _allInventories)
        {
            if(inventory.Type == type)
            {
                return inventory;
            }
        }
        return null;
    }

    private void AddIndicators()
    {
        int freePeople = 0;

        foreach(BaseObj obj in _allObjects)
        {
            if(obj is Person)
            {
                Person person = (Person)obj;
                if(!person.IsWorking && !person.IsFainted && !person.NightShift)
                {
                    freePeople += 1;
                }
            }
        }


        int ltIndiH = 100;
        int ltIndiW = 700;
        int tIndiH = 100;

        Indicator indicatorOne = new Indicator(0, 0, ltIndiW, ltIndiH, 3);
        Indicator indicatorInsideOne = new Indicator(ltIndiW/3, 0, ltIndiW/3, ltIndiH, 2);
        indicatorOne.Draw();
        List<float> xValues = indicatorOne.ColumnXValues;
        foreach(float eachX in xValues)
        {
            Indicator eachIndicator = new Indicator(eachX, 0, indicatorOne.ColumnWidths, tIndiH, 1);
            eachIndicator.Draw();
        }
        indicatorInsideOne.Draw();

        Util.ScaledDrawTexture(RunTime.PersonDown, 10, 10, ltIndiH-30);
        Rectangle currentPersonRect = new Rectangle(90, 30, 40 , 40 );
        DrawRectangleRec(currentPersonRect, new Color(255, 204, 106));

        Util.UpdateText(currentPersonRect, $"{freePeople}", 90, 30, 40, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        Util.UpdateText("/", 130, 30, 40);
        Util.UpdateText($"{GetPersonCount()}", 160, 30, 40);

        if(GetMousePosition().X > indicatorOne.X && GetMousePosition().X < indicatorOne.X + (indicatorOne.Width / 3) &&  GetMousePosition().Y > indicatorOne.Y && GetMousePosition().Y < (indicatorOne.Y + indicatorOne.Height) && IsMouseButtonPressed(MouseButton.Left))
        {
            Util.OpenPeopleList();
            // open people list and start at first page (zero-based)
            // _currentPersonDisplayPage = 0;
            // _peopleListOpen = true;
        }

        Texture2D[] weatherIcons = { RunTime.Sun, RunTime.snowyIcon, RunTime.stormyIcon };
        Texture2D currentWeatherIcon;
        switch(_mainCalendar.CurrentWeather){
            case Weather.Sunny:
                currentWeatherIcon = weatherIcons[0];
                break;
            case Weather.Snowy:
                currentWeatherIcon = weatherIcons[1];
                break;
            default:
                currentWeatherIcon = weatherIcons[2];
                break;
        }

        Util.ScaledDrawTexture(currentWeatherIcon, (int)Math.Round(ltIndiW/2.8) + 10, 5, ltIndiH-40);
        Util.UpdateText($"{_mainCalendar.CurrentWeather}",(int)Math.Round(ltIndiW/2.8) , 68, 28);

        int resSize = ltIndiH - 60;
        Util.ScaledDrawTexture(RunTime.Food, ltIndiW /2 + 5, 2, resSize);
        Util.ScaledDrawTexture(RunTime.Wood, ltIndiW /2 + 5, 35, resSize);
        Util.ScaledDrawTexture(RunTime.Stone, ltIndiW /2 + 5, 60, resSize);
        
        Rectangle foodRect = new Rectangle(ltIndiW /2 + 50, 2, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        Rectangle stoneRect = new Rectangle(ltIndiW /2 + 50, 61, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        Util.UpdateText(foodRect, $"{GetFoodCount()}",ltIndiW /2 + 50 , 5, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        Util.UpdateText(woodRect, $"{GetWoodCount()}",ltIndiW /2 + 50 , 32, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        Util.UpdateText(stoneRect, $"{GetStoneCount()}",ltIndiW /2 + 50 , 63, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Rectangle clockRect = new Rectangle(ltIndiW/3 * 2, 2, ltIndiW/3 , ltIndiH /3 * 2 );
        DrawRectangleRec(clockRect, new Color(255, 204, 106));
        Indicator dayIndicator = new Indicator(GetScreenWidth()/2 - 100, 0, 150, tIndiH, 1);
        dayIndicator.Draw();

        UpdateTime(clockRect, ltIndiW - 170, 15, _mainCalendar.CurrentTime);  
    }


    private void UpdateTime(Rectangle formatRect, int clockX, int clockY, float currentTime){

        _mainCalendar.CurrentTime = currentTime + GetFrameTime();
        int roundedTime = (int)Math.Floor(_mainCalendar.CurrentTime);
        //swamhtet this is 1 hour per 2 sec. need to change. must changed to 25
        int roundedTimeRemainder = roundedTime % (_mainCalendar.DayCriteria / 12);
        int roundedTimeResult = roundedTime / (_mainCalendar.DayCriteria / 12);
        if( roundedTimeRemainder == 0 && roundedTimeResult != _mainCalendar.PreviousRoundedResult ){
            _mainCalendar.PreviousRoundedResult = roundedTimeResult;
            if(_mainCalendar.HourSystem >= 23){
                _mainCalendar.PassMidnight();
            }else{
                _mainCalendar.HourSystem +=  1;
            }
                
            if(roundedTime >= _mainCalendar.DayCriteria && roundedTime < _mainCalendar.NightCriteria && _mainCalendar.isDay != false){
                _mainCalendar.ToggleNight();
                SpawnEnemies();
            }
            else if(roundedTime >= _mainCalendar.NightCriteria){
                _mainCalendar.EndADay();
                _mainCalendar.CheckWinLostCondition();
                KillAllEnemies();
            }
        }
        Util.UpdateText(formatRect, $"{_mainCalendar.HourSystem}:00", clockX, clockY, 50, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        string msg = _mainCalendar.IsDay ? "BUILD" : "DEFEND";
        formatRect.Y += 40;
        Util.UpdateText(formatRect, $"{msg}", clockX, clockY + 40, 35, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Red);
        Rectangle clockRect = new Rectangle(GetScreenWidth()/2 - 100, 0, 150, 100);
        
        Util.UpdateText(clockRect, $"Day: {_mainCalendar.CurrentDay} ", GetScreenWidth()/2 - 70, 30, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_CENTRE);  
    }

    public void AddMessage(string message, AlertType alertType)
    {
        switch(alertType){
            case AlertType.ERROR:
                PlaySound(RunTime.errorSound);
                break;
            case AlertType.WARNING:
                PlaySound(RunTime.warningSound);
                break;
            case AlertType.INFO:
                PlaySound(RunTime.infoSound);
                break;
        }

        _alertList.AddAlert(new Alert(message, alertType));
    }

    public int CurrentPersonDisplayPage
    {
        get { return _currentPersonDisplayPage; }
        set { _currentPersonDisplayPage = value; }
    }

    public bool PeopleListOpen
    {
        get { return _peopleListOpen; }
        set { _peopleListOpen = value; }
    }

    public List<Inventory> Inventories{
        get { return _allInventories; }
    }

    public Calendar MainCalendar{
        get { return _mainCalendar; }
    }

    private void SpawnEnemies()
    {
        Console.WriteLine("Current day " + _mainCalendar.CurrentDay);
        List<Enemy> enemiesrightNow = new List<Enemy>();
        Enemy enemy1 = new Enemy("Zombie 1", 0, 0, 55, 55, 100, RunTime.zombie_down, RunTime.currentCalendar);
        for(int i = 0; i <= _mainCalendar.CurrentDay; i++){
            Enemy clonedEnemy = enemy1.CloneEnemy();
            clonedEnemy.SetRandomLocation();
            enemiesrightNow.Add(clonedEnemy);
        }
        Console.WriteLine("Enemies right now " + enemiesrightNow.Count);

        foreach(Enemy enemiesRightNow in enemiesrightNow)
        {   
            _allObjects.Add(enemiesRightNow);   
        }
    }

    public void DestroyBuilding(Building building)
    {
        // building.CanBeSeen = false;
        
        foreach(BaseObj obj in _allObjects)
        {
            if(building is Hut){
                ((Hut)building).ReleaseAllResidents();
            }

            if(obj is Land)
            {
                if (((Land)obj).Building == building)
                {
                    ((Land)obj).RemoveBuilding();
                }
            }
        }
        QueueRemove(building);
    }   

    private void KillAllEnemies()
    {
        _allObjects.RemoveAll(obj => obj is Enemy);
    }   
}