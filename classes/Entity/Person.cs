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
    private bool _isWalking = false;
    private bool _isWorking;
    private float _workRate;
    
    private BaseObj _destination;

    public bool IsWorking 
    { 
        get => _isWorking; 
        set => _isWorking = value; 
    }

    public int CurrentEnergy 
    { 
        get => _currentEnergy; 
    }
    
    public Person(string name, float xPos, float yPos, int width, int height, int maxHealth,Texture2D personIcon, Calendar calendar)
        : base(name, xPos, yPos, width, height, maxHealth, personIcon, calendar)
    {
        _workRate = 1f;
        _maxEnergy = 100;
        _currentEnergy = 100;
        _isFainted = false;
        _isWorking = false;
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

    public override void Draw()
    {
        if (!_isWalking)
        {
            base.Draw();
        }
        else
        {
            
            if(MathF.Abs(this.X - _destination.X) > WalkRate){
                if(_destination.X > this.X){
                    this.Icon = RunTime.PersonRight;
                }else{
                    this.Icon = RunTime.PersonLeft;
                }
                this.X += MathF.Sign(_destination.X - this.X) * WalkRate;
            }else if(MathF.Abs(this.Y - _destination.Y) > WalkRate){
                if(_destination.Y > this.Y){
                    this.Icon = RunTime.PersonDown;
                }else{
                    this.Icon = RunTime.PersonUp;
                }
                this.Y += MathF.Sign(_destination.Y - this.Y) * WalkRate;
            }

            if(MathF.Abs(this.X - _destination.X) <= WalkRate && MathF.Abs(this.Y - _destination.Y) <= WalkRate){
                this.Icon = RunTime.PersonDown;
                _isWalking = false;
            }
            base.Draw();
        }
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

    public void SetDestination(BaseObj destination)
    {
        _destination = destination;
        _isWalking = true;
    }


    public override void DisplayDetails()
    {
        string generalStatus;
        if (_isFainted)
        {
            generalStatus = "Fainted";
        }
        else if (IsWorking)
        {
            generalStatus = "Working";
        }
        else
        {
            generalStatus = "Idle";
        }

        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 230, GetScreenHeight()-260, 200);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Person", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Status: {generalStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText("Effect: None", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Walk Rate: {WalkRate}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Work Rate: {_workRate * 100}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        Util.UpdateText($"Max Energy: {_maxEnergy}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        Util.UpdateText($"Current: {_currentEnergy}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }        
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

    public bool IsFainted
    {
        get { return _isFainted; }
        set { _isFainted = value; }
    }

    public override void Clone()
    {
    }
}