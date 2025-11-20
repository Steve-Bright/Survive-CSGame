namespace Game;
public abstract class Resource 
{
    protected int _capacity;
    protected string _iconPath;

    public int Capacity => _capacity;
    
    public Resource(int capacity)
    {
        _capacity = capacity;
        _iconPath = String.Empty;
    }

    public abstract void GetIcon();

    public abstract void GetResource(Inventory inv);
}