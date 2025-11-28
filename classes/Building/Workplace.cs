using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;

public abstract class Workplace : Building
{
    protected bool _peopleListOpen = false;
    protected List<ResourcePerson> _resourcePersons;
    protected bool _randomAssign;
    protected int _requiredWorkers;
    protected List<Person> _currentWorkers;
    protected int _requiredFood;
    protected int _currentFood;
    private bool _isOperating;

    public List<Person> CurrentWorkers => _currentWorkers;
    public int MaxWorkers => _requiredWorkers;

    // Constructor chains up to Building, setting default Workplace properties
    public Workplace(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost , int stoneCost, int capacityLimit = 2)
        : base(name, xPos, yPos, width, height, buildingIcon, woodCost, stoneCost, capacityLimit)
    {
        _resourcePersons = new List<ResourcePerson>();
        _requiredWorkers = 1;
        _currentWorkers = new List<Person>();
        _requiredFood = 1;
        _currentFood = 0;
        _isOperating = false;
    }

    public override void Draw()
    {
        base.Draw();
        _currentWorkers.RemoveAll(person => person.IsFainted);
    }   
    
    // Abstract methods specific to Workplace
    public abstract void Upgrade(Dictionary<ResourceType, int> consumables);
    
    public virtual void AssignWorker(Person person)
    {
        if (_currentWorkers.Count < MaxWorkers)
        {
            PlaySound(RunTime.infoSound);
            person.AssignWork(this);
            _currentWorkers.Add(person);
        }
    }

    public abstract void RemoveWorker(Person person);

    // TakeDamage is inherited from Building and must be implemented by concrete classes.
    public override abstract void TakeDamage(int hitpoint);

    public bool IsOperating
    {
        get => _isOperating;
        set => _isOperating = value;
    }
}