using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public class GameScreen : Screen
{
    private List<Inventory> _allInventories;
    private bool _isClicked = false;
    private List<BaseObj> _allObjects;
    private Calendar _mainCalendar;
    private AlertList _alertList;
    private int _currentPersonDisplayPage = 0;
    private bool _peopleListOpen = false;

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        _allInventories = new List<Inventory>();
        _allObjects = new List<BaseObj>();
        _mainCalendar = RunTime.currentCalendar;
        _mainCalendar.StartCalendar();
        _alertList = new AlertList();
    }
    public void AddBaseObj(BaseObj newObj)
    {
        _allObjects.Add(newObj);
    }

    public void Build(Building building)
    {
        int buildingCount = 0;
        for(int i = 0; i < _allObjects.Count; i++){
            if(_allObjects[i] is Building && _allObjects[i].Name == building.Name){
                buildingCount ++;
            }
        }
        if(buildingCount >= building.CapacityLimit){
            AddMessage($"Cannot build more {building.Name}s. Limit reached.", AlertType.ERROR);
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

        // DisplayPeopleList();
        if(_peopleListOpen)
        {
            DisplayPeopleList();
        }
        AddIndicators();
    }

    public int GetPersonCount()
    {
        int count = 0;
        foreach(BaseObj obj in _allObjects)
        {
            if(obj is Person)
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
            if (obj is Person p)
                persons.Add(p);
        }
        return persons;
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
                if(!person.IsWorking && !person.IsFainted)
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
            // open people list and start at first page (zero-based)
            _currentPersonDisplayPage = 0;
            _peopleListOpen = true;
        }

        Util.ScaledDrawTexture(RunTime.Sun, (int)Math.Round(ltIndiW/2.8) + 10, 5, ltIndiH-40);
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
        //swamhtet this is 1 hour per 2 sec. need to change.
        int roundedTimeRemainder = roundedTime % 2;
        int roundedTimeResult = roundedTime / 2;
        if( roundedTimeRemainder == 0 && roundedTimeResult != _mainCalendar.PreviousRoundedResult ){
            _mainCalendar.PreviousRoundedResult = roundedTimeResult;
            if(_mainCalendar.HourSystem >= 23){
                _mainCalendar.PassMidnight();
            }else{
                _mainCalendar.HourSystem +=  1;
            }
                
            if(roundedTime >= _mainCalendar.DayCriteria && roundedTime < _mainCalendar.NightCriteria && _mainCalendar.isDay != false){
                _mainCalendar.ToggleNight();
            }
            else if(roundedTime >= _mainCalendar.NightCriteria){
                _mainCalendar.EndADay();
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
        _alertList.AddAlert(new Alert(message, alertType));
    }

    private void DisplayPeopleList()
    {
        int personLimitPerPage = 5;
        int currentPerson = 0;
        int listWidth = 1100;
        int listHeight = 600;
        Rectangle peopleListRect = new Rectangle((GetScreenWidth()  - listWidth ) / 2, (GetScreenHeight() - listHeight) / 2, listWidth, listHeight);
        DrawRectangleRec(peopleListRect,  new Color(200, 200, 200, 225));

        Util.UpdateText(peopleListRect, "\nPeople List", (int) peopleListRect.X, (int) peopleListRect.Y + 20, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_TOP);

        Rectangle closeBtnRect = new Rectangle(peopleListRect.X + peopleListRect.Width - 45, peopleListRect.Y, 45, 45);
        DrawRectangleRec(closeBtnRect, new Color(255, 100, 100, 200));
        Util.ScaledDrawTexture(RunTime.CloseIcon, closeBtnRect.X, closeBtnRect.Y, 45);

        if(GetMousePosition().X > closeBtnRect.X && GetMousePosition().X < closeBtnRect.X + closeBtnRect.Width &&  GetMousePosition().Y > closeBtnRect.Y && GetMousePosition().Y < closeBtnRect.Y + closeBtnRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _currentPersonDisplayPage = 0;
            _peopleListOpen = false;
        }

        Rectangle personOne = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 80, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personOne, new Color(217, 217, 219, 255));

        Rectangle personTwo = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 170, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personTwo, new Color(217, 217, 219, 255));

        Rectangle personThree = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 260, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personThree, new Color(217, 217, 219, 255));

        Rectangle personFour = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 350, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFour, new Color(217, 217, 219, 255));

        Rectangle personFive = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 440, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFive, new Color(217, 217, 219, 255));

        Rectangle[] personRectangles = new Rectangle[] { personOne, personTwo, personThree, personFour, personFive };

        List<Person> persons = GetPersonLists();

        int personCountTotal = persons.Count;
        int pageIndex = Math.Max(0, _currentPersonDisplayPage);
        int startIndex = pageIndex * personLimitPerPage;

        for (int slot = 0; slot < personLimitPerPage; slot++)
        {
            int personIndex = startIndex + slot;
            if (personIndex >= personCountTotal) break;
            Person person = persons[personIndex];
            currentPerson++;
            string idleStatus = person.IsWorking ? "Working" : "Idle";

            Util.ScaledDrawTexture(RunTime.PersonDown, personRectangles[slot].X + 10, personRectangles[slot].Y + 10, 50);

            Rectangle personNameRect = new Rectangle((int) personRectangles[slot].X + 70, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(personNameRect, new Color(255, 204, 106, 255));
            Util.UpdateText(personNameRect, person.Name, (int) personRectangles[slot].X + 80, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle statusRect = new Rectangle((int) personRectangles[slot].X + 300, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(statusRect, new Color(255, 204, 106, 255));
            Util.UpdateText(statusRect, idleStatus, (int) personRectangles[slot].X + 310, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle energyRect = new Rectangle((int) personRectangles[slot].X + 520, (int) personRectangles[slot].Y + 10, 250, 50);
            DrawRectangleRec(energyRect, new Color(255, 204, 106, 255));
            Util.UpdateText(energyRect, "E: 100%, W: 100", (int) personRectangles[slot].X + 560, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            if(person.IsWorking)
            {
                Rectangle cancelRect = new Rectangle((int) personRectangles[slot].X + 790, (int) personRectangles[slot].Y + 10, 200, 50);
                DrawRectangleRec(cancelRect, new Color(255, 100, 100, 200));
                Util.UpdateText(cancelRect, "Cancel", (int) personRectangles[slot].X + 790, (int)personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);
                if(GetMousePosition().X > cancelRect.X && GetMousePosition().X < cancelRect.X + cancelRect.Width &&  GetMousePosition().Y > cancelRect.Y && GetMousePosition().Y < cancelRect.Y + cancelRect.Height && IsMouseButtonPressed(MouseButton.Left))
                {
                    person.IsWorking = false;
                    if(person.WorkPlace != null)
                    {
                        person.WorkPlace.RemoveWorker(person);
                    }else if(person.WorkPlaceAsWorkplace != null)
                    {
                        // person.WorkPlaceAsWorkplace.RemoveWorker(person);
                    }
                }
            }
        }


        if ((pageIndex + 1) * personLimitPerPage < personCountTotal)
        {
            Rectangle nextBtn = new Rectangle(peopleListRect.X + peopleListRect.Width - 150, peopleListRect.Y + peopleListRect.Height - 60, 140, 50);
            DrawRectangleRec(nextBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(nextBtn, 2, Color.Black);

            Util.MakeButton(nextBtn, "Next", (int)(peopleListRect.X + peopleListRect.Width - 150), (int)(peopleListRect.Y + peopleListRect.Height - 60), 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                _currentPersonDisplayPage += 1;
            });
        }

        if (pageIndex > 0)
        {
            Rectangle prevBtn = new Rectangle(peopleListRect.X + 10, peopleListRect.Y + peopleListRect.Height - 60, 140, 50);
            DrawRectangleRec(prevBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(prevBtn, 2, Color.Black);
            Util.MakeButton(prevBtn, "Prev", (int)prevBtn.X, (int)prevBtn.Y, 28, (int)TextAlign.TEXT_ALIGN_CENTRE, (int)TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                if (_currentPersonDisplayPage > 0) _currentPersonDisplayPage--;
            });
        }
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
}