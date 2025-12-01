using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;

public abstract class Workplace : Building
{
    protected bool _peopleListOpen = false;
    protected List<ResourcePerson> _resourcePersons;
    protected bool _randomAssign;
    protected int _currentFood;
    private bool _isOperating;


    // Constructor chains up to Building, setting default Workplace properties
    public Workplace(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost , int stoneCost, int capacityLimit = 2)
        : base(name, xPos, yPos, width, height, buildingIcon, woodCost, stoneCost, capacityLimit)
    {
        _resourcePersons = new List<ResourcePerson>();
        _currentFood = 0;
        _isOperating = false;
    }

    public override void Draw()
    {
        base.Draw();
        _currentPeople.RemoveAll(person => person.IsFainted);
    }   
    
    
    public virtual void AssignWorker(Person person)
    {
        Console.WriteLine("Current people count : " + _currentPeople.Count);
        if (_currentPeople.Count < _maxPeople)
        {
            PlaySound(RunTime.infoSound);
            person.AssignWork(this);
            _currentPeople.Add(person);
        }
    }

    public abstract void RemoveWorker(Person person);

    public override void ReleaseAllPeople(){
        foreach(Person person in _currentPeople){
            person.IsWorking = false;
            person.RemoveWorkPlaceAsWorkplace();

        }
        _currentPeople.Clear();
    }

    public bool IsOperating
    {
        get => _isOperating;
        set => _isOperating = value;
    }
}