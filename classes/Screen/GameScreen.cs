using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;

namespace Game;

public enum TextAlign {
    TEXT_ALIGN_LEFT   = 0,
    TEXT_ALIGN_TOP    = 0,
    TEXT_ALIGN_CENTRE = 1,
    TEXT_ALIGN_MIDDLE = 1,
    TEXT_ALIGN_RIGHT  = 2,
    TEXT_ALIGN_BOTTOM = 2
};

public class GameScreen : Screen
{
    private List<Person> _persons;
    private List<Building> _buildings;
    // private List<ResourceAre
    private Calendar _mainCalendar;

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        _persons = new List<Person>();
        _buildings = new List<Building>();
        _mainCalendar = RunTime.currentCalendar;
        _mainCalendar.StartCalendar();
    }

    public void createEntity(Person newPerson)
    {
        _persons.Add(newPerson);
    }

    public void createBuilding(Building newBuilding)
    {
        _buildings.Add(newBuilding);
    }

    override public void Display()
    {
        // Texture2D displayBg = RunTime.Main
        Texture2D displayBg = MainBackground;
        DrawTexture(displayBg, 0, 0, Color.White);


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

        AttachIconToIndicator(RunTime.PersonDown, 10, 10, ltIndiH-30);
        AttachIconToIndicator(RunTime.Sun, (int)Math.Round(ltIndiW/2.8) + 10, 5, ltIndiH-40);
        UpdateText("Sunny",(int)Math.Round(ltIndiW/2.8) , 68, 28);

        int resSize = ltIndiH - 60;
        AttachIconToIndicator(RunTime.Food, ltIndiW /2 + 5, 5, resSize);
        AttachIconToIndicator(RunTime.Wood, ltIndiW /2 + 5, 35, resSize);
        AttachIconToIndicator(RunTime.Stone, ltIndiW /2 + 5, 60, resSize);
        
        Rectangle foodRect = new Rectangle(ltIndiW /2 + 50, 2, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        Rectangle stoneRect = new Rectangle(ltIndiW /2 + 50, 61, ltIndiH - 40 , 35 );
        DrawRectangleRec(foodRect, new Color(255, 204, 106));

        UpdateText(foodRect, "100",ltIndiW /2 + 50 , 5, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        UpdateText(woodRect, "200",ltIndiW /2 + 50 , 32, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        UpdateText(stoneRect, "300",ltIndiW /2 + 50 , 63, 28, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Rectangle clockRect = new Rectangle(ltIndiW/3 * 2, 2, ltIndiW/3 , ltIndiH /3 * 2 );
        DrawRectangleRec(clockRect, new Color(255, 204, 106));
        Indicator dayIndicator = new Indicator(GetScreenWidth()/2 - 100, 0, 150, tIndiH, 1);
        dayIndicator.Draw();

        UpdateTime(clockRect, ltIndiW - 170, 15, _mainCalendar.CurrentTime);  

        
    }

    private void AttachIconToIndicator(Texture2D icon, int x, int y, int expectedWidth)
    {
;
        int frameWidth = icon.Width;
        int frameHeight = icon.Height;

        //srcRectangle
        Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
        float scale = (float)expectedWidth / frameWidth;
        // Expected Rectangle
        Rectangle destRec = new Rectangle( x, y, frameWidth * scale,  frameHeight * scale);

         // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new Vector2(0);
        DrawTexturePro(icon, sourceRec, destRec, origin, 0, Color.White);       
    }

    private void UpdateText(string text, int x, int y, int fontSize)
    {
        DrawText($"{text}", x, y, fontSize,  Color.Black);
    }
    private void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, hAlign * 0.5f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, vAlign * 0.5f))
            );
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, Color.Black);        
        // DrawText($"{text}", x, y, fontSize,  Color.Black);
    }

    private void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign, Color color)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, hAlign * 0.5f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, vAlign * 0.5f))
            );
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, color);        
        // DrawText($"{text}", x, y, fontSize,  Color.Black);
    }

    private void UpdateText(string text, int x, int y, int fontSize,  Color color)
    {
        DrawText($"{text}", x, y, fontSize, color);
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
        UpdateText(formatRect, $"{_mainCalendar.HourSystem}:00", clockX, clockY, 50, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        string msg = _mainCalendar.IsDay ? "BUILD" : "DEFEND";
        formatRect.Y += 40;
        UpdateText(formatRect, $"{msg}", clockX, clockY + 40, 35, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Red);
        Rectangle clockRect = new Rectangle(GetScreenWidth()/2 - 100, 0, 150, 100);
        
        UpdateText(clockRect, $"Day: {_mainCalendar.CurrentDay} ", GetScreenWidth()/2 - 70, 30, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_CENTRE);  
        // DrawText($"{msg}", clockX, clockY + 40, 30, Color.Red);
        // DrawText($"{_mainCalendar.HourSystem}:00", clockX, clockY, 50, Color.Black);
    }
}