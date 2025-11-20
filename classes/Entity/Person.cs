using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Game;
public class Person : Entity
{
    private int _maxEnergy;
    private int _currentEnergy;
    private bool _isFainted;
    private bool _isWorking;
    private float _workRate;

    public bool IsWorking 
    { 
        get => _isWorking; 
        private set => _isWorking = value; 
    }

    public int CurrentEnergy 
    { 
        get => _currentEnergy; 
        private set => _currentEnergy = Math.Clamp(value, 0, _maxEnergy); 
    }
    
    public Person(string name, float xPos, float yPos, int width, int height, int maxHealth, int walkRate, Texture2D personIcon, Calendar calendar)
        : base(name, xPos, yPos, width, height, maxHealth, walkRate, personIcon, calendar)
    {
        _maxEnergy = 100;
        _currentEnergy = 100;
        _isFainted = false;
        _isWorking = false;
        _workRate = 1f;
    }

    public void ConsumeFood(int foodNum)
    {
        // CurrentEnergy += foodNum;
        // Console.WriteLine($"Person consumed food, energy increased by {foodNum}. Current energy: {CurrentEnergy}");
    }

    public void Work()
    {
        // if (_isFainted || CurrentEnergy <= 0)
        // {
        //     Console.WriteLine("Cannot work. Person is fainted or out of energy.");
        //     IsWorking = false;
        //     return;
        // }

        // IsWorking = true;
        // CurrentEnergy -= _workRate / 2;
        // Console.WriteLine("Person is working.");
    }

    public void Walk(BaseObj destination)
    {
        // if (_isFainted)
        // {
        //     Console.WriteLine("Cannot walk. Person is fainted.");
        //     return;
        // }
        
        // IsWorking = false;
    }

    public void Sleep()
    {
        // IsWorking = false;
        // _isFainted = false;
        // CurrentEnergy += _maxEnergy / 5;
        // Console.WriteLine("Person is sleeping and recovering energy.");
    }

    public void GetDamaged(int healthNum)
    {
        // CurrentHealth -= healthNum;
        // Console.WriteLine($"Person took {healthNum} damage. Health remaining: {CurrentHealth}");

        // if (CurrentHealth <= 0)
        // {
        //     _isFainted = true;
        //     IsWorking = false;
        //     Console.WriteLine("Person has fainted due to low health.");
        // }
    }

    // public override void Draw()
    // {
    //     int frameWidth = Icon.Width;
    //     int frameHeight = Icon.Height;
    //     int expectedWidth = 100;

    //     //srcRectangle
    //     Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
    //     float scale = (float)expectedWidth / frameWidth;
    //     // Expected Rectangle
    //     Rectangle destRec = new Rectangle( X, Y, frameWidth * scale,  frameHeight * scale);

    //      // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
    //     Vector2 origin = new Vector2(0);
    //     DrawTexturePro(Icon, sourceRec, destRec, origin, 0, Color.White);  

    //     // DrawTexture(Icon, (int)X, (int)Y, Color.White);
    //     // Color personColor = _isFainted ? Color.GRAY : Color.BLUE;
    //     // Raylib.DrawRectangle((int)X, (int)Y, Width, Height, personColor);
    //     // Raylib.DrawText("PERSON", (int)X + 5, (int)Y + 5, 10, Color.WHITE);
    // }

    public override void Unload()
    {
    }

    public override Dictionary<string, string> ViewDetails()
    {
        return new Dictionary<string, string>
        {
            {"Health", $"{CurrentHealth}/{MaxHealth}"},
            {"Energy", $"{CurrentEnergy}/{_maxEnergy}"},
            {"Status", _isFainted ? "Fainted" : IsWorking ? "Working" : "Idle"},
        };
    }

    public override void Update(Calendar calendar)
    {
    }

    public override void Clone()
    {
    }
}