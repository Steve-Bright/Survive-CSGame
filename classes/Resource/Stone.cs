namespace Game;
public class Stone : Resource
{
    public Stone(int capacity) : base(capacity)
    {
    }

    public int Capacity => _capacity;

    public override void GetIcon()
    {
    }

    public override void GetResource(Inventory inv)
    {
        inv.Increase(1);
        _capacity--;
    }
}