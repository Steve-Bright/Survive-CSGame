using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class Kitchen : Workplace
{
    private int _cookingRate;

    public Kitchen(string name, float xPos, float yPos, int width, int height, Texture2D kitchenIcon, int woodCost = 120, int stoneCost = 0)
        : base(name, xPos, yPos, width, height, kitchenIcon, woodCost, stoneCost)
    {
        _cookingRate = 1; // e.g., 1 unit per cycle
    }

    public override void Upgrade(Dictionary<ResourceType, int> consumables)
    {
        // Logic to consume resources and improve cookingRate or durability
        Console.WriteLine("Kitchen upgraded.");
        _cookingRate++;
    }

    public override void Assign(Person person)
    {
        if (_currentWorkers.Count < MaxWorkers)
        {
            _currentWorkers.Add(person);
        }
    }

    public override void Fire(Person person)
    {
        _currentWorkers.Remove(person);
    }

    public void Cook(Inventory rawFoodInventory)
    {
        // if (_currentWorkers.Count > 0 && rawFoodInventory.Remove(ResourceType.MEAT, 1))
        // {
        //     // Successfully removed 1 raw food, now produce cooked food
        //     rawFoodInventory.Add(ResourceType.FOOD, _cookingRate);
        //     Console.WriteLine($"Kitchen cooked {_cookingRate} units of food.");
        // }
    }

    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0)
        // {
        //     Console.WriteLine("Kitchen destroyed.");
        //     Unload();
        // }
    }
    
    // Abstract BaseObj/Entity Implementations
    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string> { {"Cooking Rate", _cookingRate.ToString()}, {"Workers", _currentWorkers.Count.ToString()} };
    }
    
    

    public override void Clone() { }

    public override void DisplayDetails()
    {
       string clinicStatus = IsOperating ? "Operating" : "Idle";

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

        Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Status: {clinicStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Cook Rate: {_cookingRate}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Health: {_currentHealth} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Max Workers: {MaxWorkers}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Current: {CurrentWorkers.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        // Util.UpdateText($"Max Workers: {MaxWorkers}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText($"Current: {CurrentWorkers.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
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