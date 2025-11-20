using System;
using Raylib_cs;

namespace Game;
// Entity no longer implements ISubscriber
public abstract class Entity : BaseObj 
{
    private int _maxHealth;
    private Calendar _calendar;
    private int _currentHealth;
    private int _walkRate;
    private bool _isAccessible;
    
    public int MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }
    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set => _currentHealth = Math.Clamp(value, 0, _maxHealth); 
    }
    public int WalkRate { get => _walkRate; set => _walkRate = value; }
    public bool IsAccessible { get => _isAccessible; set => _isAccessible = value; }
    
    public Entity(string name, float xPos, float yPos, int width, int height, int maxHealth, int walkRate, Texture2D entityIcon, Calendar calendar)
        : base(name, xPos, yPos, width, height, entityIcon) 
    {
        _calendar = calendar;
        calendar.Subscribe(this);
        _maxHealth = maxHealth;
        _currentHealth = maxHealth; 
        _walkRate = walkRate;
        _isAccessible = true;
    }

    public abstract void Update(Calendar calendar); 

    public abstract void Clone(); 


}