using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Game;
public abstract class Defense : Building
{
    protected int _maxPerson;
    protected List<Person> _currentPeople;
    protected float _critChance;
    protected int _range;
    protected int _hitRate;
    protected int _coolDownTime;

    public int MaxPersonCount => _maxPerson;
    public List<Person> AllPeople => _currentPeople;

    public Defense(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon,  int maxPerson, float critChance, int range, int hitRate, int woodCost = 60, int stoneCost = 60, int capacityLimit = 4)
        : base(name, xPos, yPos, width, height,  buildingIcon, woodCost, stoneCost, capacityLimit)
    {
        _maxPerson = maxPerson;
        _currentPeople = new List<Person>();
        _critChance = critChance;
        _range = range;
        _hitRate = hitRate;
    }
    
    // Abstract methods specific to Defense
    public abstract void Attack();
    public abstract void Assign(Person person);
    public abstract void Remove(Person person);
    
    public override abstract void TakeDamage(int hitpoint);

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

        Util.UpdateText("Type: Defense", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        // Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Range: {_range} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Hit Rate: {_hitRate}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Crit Chance: {_critChance}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Cooldown: {_coolDownTime}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        Util.UpdateText($"Max Person: {_maxPerson}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        Util.UpdateText($"Current: {_currentPeople.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
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