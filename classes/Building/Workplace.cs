using Raylib_cs;

namespace Game;

public abstract class Workplace : Building
{
    protected int _requiredWorkers;
    protected List<Person> _currentWorkers;
    protected int _requiredFood;
    protected int _currentFood;

    // Assuming a default durability for Workplaces, and default max workers.
    protected const int DEFAULT_MAX_WORKERS = 5;

    public List<Person> CurrentWorkers => _currentWorkers;
    public int MaxWorkers => DEFAULT_MAX_WORKERS;

    // Constructor chains up to Building, setting default Workplace properties
    public Workplace(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost , int stoneCost, int capacityLimit = 2)
        : base(name, xPos, yPos, width, height, buildingIcon, woodCost, stoneCost, capacityLimit)
    {
        _requiredWorkers = 1;
        _currentWorkers = new List<Person>();
        _requiredFood = 1;
        _currentFood = 0;
    }
    
    // Abstract methods specific to Workplace
    public abstract void Upgrade(Dictionary<ResourceType, int> consumables);
    public abstract void Assign(Person person);
    public abstract void Fire(Person person);

    // TakeDamage is inherited from Building and must be implemented by concrete classes.
    public override abstract void TakeDamage(int hitpoint);
}