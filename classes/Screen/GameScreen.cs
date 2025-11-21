using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public class GameScreen : Screen
{
    private List<ResourceArea> _resourceAreas;
    private List<Inventory> _allInventories;
    private List<BaseObj> _allObjects;
    private Calendar _mainCalendar;

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        _resourceAreas = new List<ResourceArea>();
        _allInventories = new List<Inventory>();
        _allObjects = new List<BaseObj>();
        _mainCalendar = RunTime.currentCalendar;
        _mainCalendar.StartCalendar();
    }

    public void AddResourceArea(ResourceArea newResourceArea)
    {
        _resourceAreas.Add(newResourceArea);
    }

    public void AddBaseObj(BaseObj newObj)
    {
        _allObjects.Add(newObj);
    }

    public void AddInventory(Inventory newInventory)
    {
        _allInventories.Add(newInventory);
    }

    override public void Display()
    {
        Texture2D displayBg = MainBackground;
        DrawTexture(displayBg, 0, 0, Color.White);

        foreach(BaseObj eachObj in _allObjects)
        {
            eachObj.Draw();
        }

        foreach(ResourceArea eachResourceArea in _resourceAreas)
        {
            eachResourceArea.Draw();
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
}