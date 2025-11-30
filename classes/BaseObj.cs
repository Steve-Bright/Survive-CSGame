using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Game;
public abstract class BaseObj
{
    private string _name;
    private float _x;
    private float _y;
    private int _width;
    private int _height;
    private Texture2D _icon;
    private bool _isSelected;
    private bool _canBeSeen = true;

    public float X 
    { 
        get => _x; 
        set => _x = value; 
    }

    public float Y 
    { 
        get => _y; 
        set => _y = value; 
    }

    public int Width 
    { 
        get => _width; 
        set => _width = value; 
    }

    public int Height 
    { 
        get => _height; 
        set => _height = value; 
    }
    
    public Texture2D Icon
    { 
        get => _icon; 
        set => _icon = value; 
    }

    public BaseObj(string name, float xPos, float yPos, int width, int height, Texture2D icon)
    {
        _name = name;
        _x = xPos;
        _y = yPos;
        _width = width;
        _height = height;
        _icon = icon;
    }

    public abstract void DisplayDetails();

    public virtual void Draw()
    {
        if(_canBeSeen)
        {
            Util.ScaledDrawTexture(_icon, _x, _y, _width);
        }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }

    public string Name
    {
        get { return _name; }
    }   

    public bool CanBeSeen
    {
        get { return _canBeSeen; }
        set { _canBeSeen = value; }
    }

}