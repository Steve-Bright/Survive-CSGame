using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;

namespace Game;
public class GameScreen : Screen
{

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        
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

        UpdateText(foodRect, "100",ltIndiW /2 + 50 , 5, 28);
        UpdateText(woodRect, "200",ltIndiW /2 + 50 , 32, 28);
        UpdateText(stoneRect, "300",ltIndiW /2 + 50 , 63, 28);

        // UpdateText(GetFrameTime().ToString("0.00"), 10, GetScreenHeight() - 30, 20, Color.Red);
        Indicator dayIndicator = new Indicator(GetScreenWidth()/2 - 100, 0, 150, tIndiH, 1);
        dayIndicator.Draw();
        
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
    private void UpdateText(Rectangle rect, string text, int x, int y, int fontSize)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, 1f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, 0.5f))
            );
        // Vector2 textPos = (Vector2) {
        //     ,
        //     
        // };
            
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, Color.Black);        
        // DrawText($"{text}", x, y, fontSize,  Color.Black);
    }

    private void UpdateText(string text, int x, int y, int fontSize,  Color color)
    {
        DrawText($"{text}", x, y, fontSize, color);
    }
}