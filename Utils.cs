using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raymath;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;
public static class Util
{
    public static void ScaledDrawTexture(Texture2D texture, float x, float y, int expectedWidth)
    {
       int frameWidth = texture.Width;
        int frameHeight = texture.Height;

        //srcRectangle
        Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
        float scale = (float)expectedWidth / frameWidth;

        Rectangle destRec = new Rectangle( x, y, frameWidth * scale,  frameHeight * scale);

         // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new Vector2(0);
        DrawTexturePro(texture, sourceRec, destRec, origin, 0, Color.White);   
    }

    public static void ScaledDrawTexture(Texture2D texture, float x, float y, int expectedWidth, int expectedHeight)
    {
        int frameWidth = texture.Width;
        int frameHeight = texture.Height;

        //srcRectangle
        Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
        float scaleX = (float)expectedWidth / frameWidth;
        float scaleY = (float)expectedHeight / frameHeight;
        //destRectangle
        Rectangle destRec = new Rectangle( x, y, expectedWidth * scaleX, expectedHeight * scaleY);

        // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new Vector2(0);
        DrawTexturePro(texture, sourceRec, destRec, origin, 0, Color.White);  
    }

    public static void UpdateText(string text, int x, int y, int fontSize)
    {
        DrawText($"{text}", x, y, fontSize,  Color.Black);
    }
    public static void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign)
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

    public static void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign, Color color)
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

    public static void UpdateText(string text, int x, int y, int fontSize,  Color color)
    {
        DrawText($"{text}", x, y, fontSize, color);
    }

}