using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Game;

public class Hut : Building
{
    private int _hutId;
    private int _maxPerson;
    private List<Person> _currentPeople;

    public int MaxPersonCount => _maxPerson;
    public List<Person> AllPeople => _currentPeople;

    public Hut(string name, float xPos, float yPos, int width, int height, Texture2D hutIcon, int woodCost = 50, int stoneCost = 30, int capacityLimit = 6)
        : base(name, xPos, yPos, width, height, hutIcon, woodCost, stoneCost, capacityLimit)
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
    
    
    public override void Clone() { }

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

        Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Max People: {_maxPerson}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Current: {_currentPeople.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Health: {_currentHealth} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        // // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Max Patients: {_maxPatients}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        // Util.UpdateText($"Current: {_currentPatients.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

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