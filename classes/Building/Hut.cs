using Raylib_cs;
namespace Game;

public class Hut : Building
{
    private int _hutId;
    private int _maxPerson;
    private List<Person> _currentPeople;

    public int MaxPersonCount => _maxPerson;
    public List<Person> AllPeople => _currentPeople;

    public Hut(string name, float xPos, float yPos, int width, int height, Texture2D hutIcon)
        : base(name, xPos, yPos, width, height, hutIcon)
    {
        // _hutId = 1;
        _maxPerson = 10;
        _currentPeople = new List<Person>();
    }
    
    public void Assign(Person person)
    {
        // if (_currentPeople.Count < _maxPerson)
        // {
        //     _currentPeople.Add(person);
        // }
    }
    
    public void Remove(Person person)
    {
        // _currentPeople.Remove(person);
    }
    
    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0)
        // {
        //     Console.WriteLine("Hut destroyed.");
        //     Unload();
        // }
    }
    
    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string> { {"Occupancy", "test"} };
    }
    
    public override void Draw()
    {
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Color.BEIGE);
    }
    
    public override void Unload() { }
    public override void Clone() { }
}