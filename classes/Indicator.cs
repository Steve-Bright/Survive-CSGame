using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;

public class Indicator
{
    private float _x;
    private float _y;
    private float _w;
    private float _h;
    private float _columnWidths;

    private int _columns;
    private Color _baseColor;
    private float _outlineThick;
    private Color _outlineColor;
    private List<float> _allColumnX;

    public Indicator(float xPos, float yPos, float width, float height, int cols)
    {
        if (cols < 1 || cols > 3)
        {
            throw new ArgumentException("Haiya cannot lah, only support 1 to 3 columns!");
        }
        
        _allColumnX = new List<float>();
        _x = xPos;
        _y = yPos;
        _w = width;
        _h = height;
        _columns = cols;
        _columnWidths = width / cols;

        _baseColor = new Color(255, 204, 106);
        // _baseColor = customizedColor;
        _outlineThick = 2;
        _outlineColor = Color.Black;
    }

    private float X
    {
        get { return _x; }
        set { _x = value; }
    }

    private float Y
    {
        get { return _y; }
        set { _y = value; }
    }


    private float Width
    {
        get { return _w; }
        set { _w = value; }
    }

    private float Height
    {
        get { return _h; }
        set { _h = value; }
    }

    private Color BaseColor
    {
        get { return _baseColor; }
        set { _baseColor = value; }
    }
    private float OutlineThick
    {
        get { return _outlineThick; }
        set { _outlineThick = value; }
    }
    private Color OutlineColor
    {
        get { return _outlineColor; }
        set { _outlineColor = value; }
    }

    public void Draw()
    {

        for (int i = 0; i < _columns; i++)
        {
            float columnX = _x + (i * _columnWidths);
            _allColumnX.Add(columnX);
            Rectangle indicatorRect = new Rectangle(columnX, _y, _columnWidths, _h);
            DrawRectangleRec(indicatorRect, _baseColor);
            DrawRectangleLinesEx(indicatorRect, _outlineThick, _outlineColor);
        }

        // Rectangle indicatorOne = new Rectangle(x, y, w, h);
        // DrawRectangleRec(indicatorOne, baseColor);
        // DrawRectangleLinesEx(indicatorOne, 2, outlineColor);
    }

    public List<float> ColumnXValues
    {
        get{ return _allColumnX;}
    }

    public float ColumnWidths
    {
        get{return _columnWidths;}
    }
}