using System.Collections.Generic;
using Raylib_cs;

namespace Game;
public class Land : BaseObj
{
    private Building _building; 
    
    public Land(string name, float xPos, float yPos, int width, int height, Texture2D landIcon)
        : base(name, xPos, yPos, width, height, landIcon)
    {
        _building = null;
    }
    
    public override void Draw()
    {
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Color);
        
        // if (_building != null)
        // {
        //     _building.Draw();
        // }
    }

    public override void Unload()
    {
        // if (_building != null)
        // {
        //     _building.Unload();
        // }
    }

    public override Dictionary<string, string> ViewDetails()
    {
        Dictionary<string, string> details = new Dictionary<string, string>
        {
            {"Type", "Land"},
            {"Status", _building != null ? "Occupied" : "Empty"},
        };
        
        if (_building != null)
        {
            details.Add("Building", _building.GetType().Name);
        }
        
        return details;
    }
}