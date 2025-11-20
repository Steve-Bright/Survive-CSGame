using System;
using Raylib_cs;
namespace Game;
public abstract class BaseObj
{
    private string _name;
    private float _x;
    private float _y;
    private int _width;
    private int _height;
    private Texture2D _icon;

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

    public abstract Dictionary<string, string> ViewDetails();

    public abstract void Draw();

    public abstract void Unload();
}