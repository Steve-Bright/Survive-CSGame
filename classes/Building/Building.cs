
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public abstract class Building : BaseObj
{
    private int _capacityLimit;
    private int _woodCost;
    private int _stoneCost;
    private int _level;
    private int _currentHealth;
    private Dictionary<ResourceType, int> _requiredToBuild;
    private int _requiredBuilder;
    private List<Person> _listOfBuilders;
    private int _maxHealth;
    private int _buildingTime;

    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set => _currentHealth = Math.Clamp(value, 0, _maxHealth); 
    }

    public int Level => _level;

    public Building(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost, int stoneCost, int capacityLimit = 0)
        : base(name, xPos, yPos, width, height, buildingIcon)
    {
        _level = 0;
        _maxHealth = 100;
        _currentHealth = 100;
        _requiredToBuild = new Dictionary<ResourceType, int>();
        _requiredBuilder = 1;
        _listOfBuilders = new List<Person>();
        _buildingTime = 100;
        _woodCost = woodCost;
        _stoneCost = stoneCost;
        _capacityLimit = capacityLimit;
    }

    public void Build(Land land, Dictionary<ResourceType, int> consumables)
    {
        // Console.WriteLine($"Attempting to build on {land.AreaSize} land using resources.");
    }

    public void Upgrade(Dictionary<ResourceType, int> consumables)
    {
        Console.WriteLine($"Attempting to upgrade Building to level {_level + 1}.");
        _level++;
    }

    public void Repair(Dictionary<ResourceType, int> consumables)
    {
        Console.WriteLine("Attempting to repair Building.");
    }

    public abstract void TakeDamage(int hitpoint);

    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string>
        {
            {"Name", base.X.ToString()},
            {"Level", _level.ToString()},
            {"Health", $"{CurrentHealth}/{_maxHealth}"},
        };
    }

    public int WoodCost => _woodCost;
    public int StoneCost => _stoneCost;

    public abstract void Clone(); 

    public int CapacityLimit{
        get => _capacityLimit;
    }

    public override void DisplayDetails()
    {
        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 260, GetScreenHeight()-245, 150);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        // Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        // Util.UpdateText($"Health: {_currentHealth}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Health: {_currentHealth}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Health: {_currentHealth} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Max People: {_requiredBuilder}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        // Util.UpdateText($"Current: {_listOfBuilders.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

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
}