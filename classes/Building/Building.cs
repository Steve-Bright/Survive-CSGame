using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Game;
public abstract class Building : BaseObj
{
    private int _level;
    private int _currentHealth;
    private Dictionary<ResourceType, int> _requiredToBuild;
    private int _requiredBuilder;
    private List<Person> _listOfBuilders;
    private int _maxHealth;
    private int _buildingTime;

    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set => _currentHealth = Math.Clamp(value, 0, _maxHealth); 
    }

    public int Level => _level;

    public Building(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon)
        : base(name, xPos, yPos, width, height, buildingIcon)
    {
        _level = 0;
        _maxHealth = 100;
        _currentHealth = 0;
        _requiredToBuild = new Dictionary<ResourceType, int>();
        _requiredBuilder = 1;
        _listOfBuilders = new List<Person>();
        _buildingTime = 100;
    }

    public void Build(Land land, Dictionary<ResourceType, int> consumables)
    {
        // Console.WriteLine($"Attempting to build on {land.AreaSize} land using resources.");
    }

    public void Upgrade(Dictionary<ResourceType, int> consumables)
    {
        Console.WriteLine($"Attempting to upgrade Building to level {_level + 1}.");
        _level++;
    }

    public void Repair(Dictionary<ResourceType, int> consumables)
    {
        Console.WriteLine("Attempting to repair Building.");
    }

    public abstract void TakeDamage(int hitpoint);

    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string>
        {
            {"Name", base.X.ToString()},
            {"Level", _level.ToString()},
            {"Health", $"{CurrentHealth}/{_maxHealth}"},
        };
    }

    public abstract void Clone(); 
}