namespace Game;
public class Meat : Resource
{
    public Meat(int capacity) : base(capacity)
    {
    }

    public int Capacity => _capacity; // Readonly property shorthand

    public override void GetIcon()
    {
    }

    public override void GetResource(Inventory inv)
    {
        inv.Increase(1);
        _capacity--;
    }
}