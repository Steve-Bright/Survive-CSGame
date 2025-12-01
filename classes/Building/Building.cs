
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public abstract class Building : BaseObj
{
    private int _capacityLimit;
    private int _woodCost;
    private int _stoneCost;
    private int _currentHealth;
    private int _maxHealth;

    protected List<Person> _currentPeople;
    protected int _maxPeople;

    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set => _currentHealth = Math.Clamp(value, 0, _maxHealth); 
    }


    public Building(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost, int stoneCost, int capacityLimit = 0, int maxPeople = 1)
        : base(name, xPos, yPos, width, height, buildingIcon)
    {
        _currentPeople = new List<Person>();
        _maxHealth = 100;
        _currentHealth = 100;
        _woodCost = woodCost;
        _stoneCost = stoneCost;
        _capacityLimit = capacityLimit;
        _maxPeople = maxPeople;
    }

    public int WoodCost => _woodCost;
    public int StoneCost => _stoneCost;

    public abstract void ReleaseAllPeople();

    public int CapacityLimit{
        get => _capacityLimit;
    }

    public virtual void GetDamaged(int healthNum)
    {
        CurrentHealth -= healthNum;
        if(CurrentHealth <= 0)
        {
            RunTime.gameScreen.DestroyBuilding(this);
        }
    }
}