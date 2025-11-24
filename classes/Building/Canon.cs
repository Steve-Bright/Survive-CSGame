using Raylib_cs;

namespace Game;
public class Cannon : Defense
{
    public Cannon(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int maxPerson, float critChance, int range, int hitRate)
        : base(name, xPos, yPos, width, height,  buildingIcon,  maxPerson, critChance, range, hitRate)
    {
    }

    public override void Attack()
    {
        if (_currentPeople.Count > 0)
        {
            Console.WriteLine("Cannon firing a heavy projectile.");
        }
    }
    
    public override void Assign(Person person)
    {
        if (_currentPeople.Count < _maxPerson) _currentPeople.Add(person);
    }
    
    public override void Remove(Person person)
    {
        _currentPeople.Remove(person);
    }
    
    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0) Unload();
    }
    
    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string> { {"Range", _range.ToString()}, {"Hit Rate", _hitRate.ToString()} };
    }
    
    
    public override void Clone() { }
}