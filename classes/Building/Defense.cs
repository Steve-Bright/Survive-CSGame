using Raylib_cs;


namespace Game;
public abstract class Defense : Building
{
    protected int _maxPerson;
    protected List<Person> _currentPeople;
    protected float _critChance;
    protected int _range;
    protected int _hitRate;

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
}