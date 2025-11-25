using System;
using static Raylib_cs.Raylib;
using Raylib_cs;
namespace Game;
public enum Target 
{
    PERSON,
    BUILDING
}

public enum EnemyType 
{
    Snake,
    Zombie
}

public class Enemy : Entity
{
    private int _attackRate;
    private Target _preferableTarget;
    private EnemyType _enemyType;
    private float _spawnRate;

    public Enemy(string name, float xPos, float yPos, int width, int height, int maxHealth, Texture2D enemyIcon, Calendar calendar)
        : base(name, xPos, yPos, width, height, maxHealth, enemyIcon, calendar)
    {
        _attackRate = 1; 
        _preferableTarget = Target.PERSON; 
        _enemyType = EnemyType.Zombie;
        _spawnRate = 1f; 
    }

    public void Attack(BaseObj target)
    {
        Console.WriteLine($"Enemy attacks {target.GetType().Name}. Preferable target: {_preferableTarget}.");
    }

    public void Deploy()
    {
        Console.WriteLine($"Enemy ({_enemyType}) deployed.");
    }

    public void Walk(BaseObj destination)
    {
    }

    public void GetDamaged(int healthNum)
    {
        // CurrentHealth -= healthNum;
        // if (CurrentHealth <= 0)
        // {
        //     Die();
        // }
    }

    public void Die()
    {
        // Console.WriteLine($"Enemy {_enemyType} has died.");
        // Unload();
    }

    public override void Draw()
    {
        // Color enemyColor = (_enemyType == EnemyType.Zombie) ? Color.Green : Color.Brown;
        // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, enemyColor);
    }


    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string>
        {
            {"Type", _enemyType.ToString()},
            {"Health", $"{CurrentHealth}/{MaxHealth}"},
            {"Target", _preferableTarget.ToString()},
        };
    }

    public override void Update(Calendar calendar)
    {
    }

    public override void Clone()
    {
    }
}