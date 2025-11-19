using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

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
        Indicator indicatorOne = new Indicator(0, 0, ltIndiW, ltIndiH, 3);
        Indicator indicatorInsideOne = new Indicator(ltIndiW/3, 0, ltIndiW/3, ltIndiH, 2);
        indicatorOne.Draw();
        List<float> xValues = indicatorOne.ColumnXValues;
        foreach(float eachX in xValues)
        {
            Indicator eachIndicator = new Indicator(eachX, 0, indicatorOne.ColumnWidths, 100, 1);
            eachIndicator.Draw();
        }
        indicatorInsideOne.Draw();

        AttachIconToIndicator(RunTime.PersonDown, 10, 10, ltIndiH-30);
        AttachIconToIndicator(RunTime.Sun, (int)Math.Round(ltIndiW/2.8), 5, ltIndiH-40);
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
}