using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public class ResourceArea : BaseObj
{
    
    private int _capacity;
    private ResourceType _resource;
    private bool _isAccessible;
    private int _requiredWorkers;
    private List<Person> _currentWorkers;

    // + ResourceArea(name: string, xPos: float, yPos: float, width: int, height: int, type: ResourceType)
    public ResourceArea(string name, float xPos, float yPos, int width, int height, Texture2D resourceAreaIcon,  ResourceType type)
        : base(name, xPos, yPos, width, height, resourceAreaIcon)
    {
        _capacity = 1000;
        _resource = type;
        _isAccessible = true;
        _requiredWorkers = 10;
        _currentWorkers = new List<Person>();
    }

    public override void DisplayDetails()
    {
        string resourceAreaStatus = _isAccessible ? "Active" : "Inactive";

        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 230, GetScreenHeight()-245, 200);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Resource", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Resource: {_resource.ToString().ToLower()}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Max Workers: {_requiredWorkers}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Current: {_currentWorkers.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Capacity: {_capacity}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Status: {resourceAreaStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        // Util.UpdateText($"Max Energy: TEST", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText($"Current: test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }            
    }

    public ResourceType Resource => _resource;

    public bool IsAccessible => _isAccessible;

    public void AssignWorker(List<Person> workers)
    {
        _currentWorkers.Clear();
        if (_isAccessible)
        {
            _currentWorkers.AddRange(workers);
        }
    }

    // + Extract(type: ResourceType, inv: Inventory)
    public void Extract(ResourceType type, Inventory inv)
    {
        // if (_currentWorkers.Count > 0 && type == _resource.Type)
        // {
        //     int extractionRate = _currentWorkers.Count; // Simple extraction based on worker count
        //     inv.AddResource(type, extractionRate);
        // }
    }


    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string>
        {
            {"Resource", _resource.ToString()},
            {"Accessible", _isAccessible.ToString()},
            {"Workers", _currentWorkers.Count.ToString()},
            {"Required", _requiredWorkers.ToString()}
        };
    }

    // public override void Draw()
    // {
        // Color drawColor;
        // switch (_resource.Type)
        // {
        //     case ResourceType.WOOD:
        //         drawColor = new Color(139, 69, 19, 255); // Brown
        //         break;
        //     case ResourceType.STONE:
        //         drawColor = Color.GRAY;
        //         break;
        //     case ResourceType.FOOD:
        //     case ResourceType.MEAT:
        //         drawColor = Color.YELLOW;
        //         break;
        //     default:
        //         drawColor = Color.BLUE;
        //         break;
        // }
        
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, drawColor);
        // Raylib.DrawText(_resource.Type.ToString(), (int)X + 5, (int)Y + 5, 10, Color.BLACK);
    // }

    // + Unload() : void
    public override void Unload()
    {
        // Logic to dispose of any Raylib textures or resources specific to this area
    }
}