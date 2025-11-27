using Raylib_cs;

namespace Game;
public class Cannon : Defense
{
    public Cannon(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int maxPerson, float critChance, int range, int hitRate, int woodCost = 0, int stoneCost = 150)
        : base(name, xPos, yPos, width, height,  buildingIcon,  maxPerson, critChance, range, hitRate, woodCost, stoneCost)
    {
    }

    public override void Attack()
    {
        if (_currentWorkers.Count > 0)
        {
            Console.WriteLine("Cannon firing a heavy projectile.");
        }
    }
    
    // public override void Assign(Person person)
    // {
    //     if (_currentWorkers.Count < _requiredWorkers) _currentWorkers.Add(person);
    // }
    
    public override void Remove(Person person)
    {
        _currentWorkers.Remove(person);
    }
    
    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0) Unload();
    }

    
    
    public override void Clone() { }
}