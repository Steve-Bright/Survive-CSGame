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

    public virtual void DisplayDetails()
    {
            //placeholder virtual method. 
            
            // Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
            // closeIndicator.Draw();
            // Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
            // Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
            // Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-50, 262 , 50, 1, 1);
            // Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 1, 2);
            // bottomIndicator.Draw();
            // indicatorInsideBottom.Draw();
            // photoIndicator.Draw();

            // Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 28 );
            // DrawRectangleRec(nameRect, new Color(255, 204, 106));
            // Util.UpdateText(nameRect, "John Doe", (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            // Util.ScaledDrawTexture(_icon, (GetScreenWidth() / 2) + 230, GetScreenHeight()-260, 200);
            // Util.UpdateText(_name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28);

            // Util.UpdateText("Type: Person", (GetScreenWidth() / 2) + 480, GetScreenHeight()-240, 28);
            // Util.UpdateText("Status: Active", (GetScreenWidth() / 2) + 740, GetScreenHeight()-240, 28);
            // Util.UpdateText("Health: 100%", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
            // Util.UpdateText("Effect: None", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
            // Util.UpdateText("WalkRate: 5", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
            // Util.UpdateText("Work Rate: 5", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);

            // Util.UpdateText("Req. Workers: 10", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
            // Util.UpdateText("Current: 10", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28); 
            // Util.UpdateText("Max Patients: 10", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
            // Util.UpdateText("Current: 10", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
            // Util.UpdateText("Req. Food: 50", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);
            // Util.UpdateText("Current: 50", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 

            // RunTime.detailsShown = true;
            // if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
            // {
            //     _isSelected = false;
            //     RunTime.detailsShown = false;
            // }
    }

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