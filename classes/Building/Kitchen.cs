using Raylib_cs;

namespace Game;
public class Kitchen : Workplace
{
    private int _cookingRate;

    public Kitchen(string name, float xPos, float yPos, int width, int height, Texture2D kitchenIcon)
        : base(name, xPos, yPos, width, height, kitchenIcon)
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
    
    public override void Draw()
    {
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Color.ORANGE);
    }
    
    public override void Unload() { }
    public override void Clone() { }
}