using Raylib_cs;

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
        _requiredWorkers = 1;
        _currentWorkers = new List<Person>();
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