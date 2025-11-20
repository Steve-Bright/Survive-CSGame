using Raylib_cs;
namespace Game;
public class Wall : Building
{
    private int _durability;
    
    public Wall(string name, float xPos, float yPos, int width, int height, Texture2D wallIcon, int durability)
        : base(name, xPos, yPos, width, height, wallIcon) 
    {
        _durability = durability; 
    }
    
    public void Upgrade(ResourceType type, int consumables)
    {
    }
    
    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0)
        // {
        //     Console.WriteLine("Wall destroyed.");
        //     Unload();
        // }
    }
    
    // Abstract BaseObj/Entity Implementations
    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string> { {"Durability", "test"} };
    }
    
    public override void Draw()
    {
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Color.DARKGRAY);
    }
    
    public override void Unload() { }
    public override void Clone() { }
}