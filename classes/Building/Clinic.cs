using Raylib_cs;
namespace Game;
public class Clinic : Workplace
{
    private int _maxPatients;
    private List<Person> _currentPatients;
    private int _healingRate;

    public List<Person> CurrentPatients => _currentPatients;
    public int MaxPatients => _maxPatients;

    public Clinic(string name, float xPos, float yPos, int width, int height, Texture2D clinicIcon, int woodCost = 150, int stoneCost = 100)
        : base(name, xPos, yPos, width, height, clinicIcon, woodCost, stoneCost)
    {
        _maxPatients = 5;
        _currentPatients = new List<Person>();
        _healingRate = 1;
    }

    public override void Upgrade(Dictionary<ResourceType, int> consumables)
    {
        // Logic to consume resources and improve maxPatients or healingRate
        Console.WriteLine("Clinic upgraded.");
        _maxPatients += 2;
    }

    public override void Assign(Person person)
    {
        // Assigning a worker (doctor/nurse)
        if (_currentWorkers.Count < MaxWorkers)
        {
            _currentWorkers.Add(person);
        }
    }

    public override void Fire(Person person)
    {
        // Firing a worker
        _currentWorkers.Remove(person);
    }
    
    public void Admit(Person person)
    {
        if (_currentPatients.Count < _maxPatients)
        {
            _currentPatients.Add(person);
        }
    }
    
    public void Heal()
    {
        if (_currentWorkers.Count > 0 && _currentPatients.Count > 0)
        {
            // Logic to apply healing based on _healingRate and workers
            Console.WriteLine($"Clinic is healing {_currentPatients.Count} patients.");
        }
    }
    
    public void Release(Person person)
    {
        _currentPatients.Remove(person);
    }

    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0)
        // {
        //     Console.WriteLine("Clinic destroyed.");
        //     Unload();
        // }
    }

    // Abstract BaseObj/Entity Implementations
    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string> { {"Max Patients", _maxPatients.ToString()}, {"Patients", _currentPatients.Count.ToString()} };
    }
    
    
    public override void Clone() { }
}