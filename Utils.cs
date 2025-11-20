using Raylib_cs;
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

}