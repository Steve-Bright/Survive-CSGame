
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public abstract class Building : BaseObj
{
    private int _capacityLimit;
    private int _woodCost;
    private int _stoneCost;
    private int _currentHealth;
    private int _requiredBuilder;
    private List<Person> _listOfBuilders;
    private int _maxHealth;

    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set => _currentHealth = Math.Clamp(value, 0, _maxHealth); 
    }


    public Building(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int woodCost, int stoneCost, int capacityLimit = 0)
        : base(name, xPos, yPos, width, height, buildingIcon)
    {
        _maxHealth = 100;
        _currentHealth = 100;
        _requiredBuilder = 1;
        _listOfBuilders = new List<Person>();
        _woodCost = woodCost;
        _stoneCost = stoneCost;
        _capacityLimit = capacityLimit;
    }


    public abstract void TakeDamage(int hitpoint);

    public int WoodCost => _woodCost;
    public int StoneCost => _stoneCost;


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