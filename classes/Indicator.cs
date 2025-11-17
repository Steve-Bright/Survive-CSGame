using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;

public class Indicator
{
    private float x;
    private float y;
    private float w;
    private float h;

    private Color baseColor;
    private float outlineThick;
    private Color outlineColor;


    public Indicator(float xPos, float yPos, float width, float height)
    {
        x = xPos;
        y = yPos;
        w = width;
        h = height;

        baseColor = new Color(255, 204, 106);
        outlineThick = 2;
        outlineColor = Color.Black;
    }

    private float X
    {
        get { return x; }
        set { x = value; }
    }

    private float Y
    {
        get { return y; }
        set { y = value; }
    }


    private float Width
    {
        get { return w; }
        set { w = value; }
    }

    private float Height
    {
        get { return h; }
        set { h = value; }
    }

    private Color BaseColor
    {
        get { return baseColor; }
        set { baseColor = value; }
    }
    private float OutlineThick
    {
        get { return outlineThick; }
        set { outlineThick = value; }
    }
    private Color OutlineColor
    {
        get { return outlineColor; }
        set { outlineColor = value; }
    }

    public void Draw()
    {

        Rectangle indicatorOne = new Rectangle(x, y, w, h);
        DrawRectangleRec(indicatorOne, baseColor);
        DrawRectangleLinesEx(indicatorOne, 2, outlineColor);
    }
}