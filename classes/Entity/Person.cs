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
    private bool _isSleeping = false;
    private bool _isWorking;
    private float _workRate;
    private float _workTimer = 0f;
    
    private List<Inventory> _inventories;
    private Vector2 _destionationXY;
    private BaseObj? _destination;
    private ResourceArea? _resourceArea;
    private Building _placeHut;
    private Workplace? _workPlaceAsWorkplace;

    public bool IsWorking 
    { 
        get => _isWorking; 
        set => _isWorking = value; 
    }

    public int CurrentEnergy 
    { 
        get => _currentEnergy; 
    }
    
    public Person(string name, float xPos, float yPos, int width, int height, int maxHealth,Texture2D personIcon, Calendar calendar, List<Inventory> inventories )
        : base(name, xPos, yPos, width, height, maxHealth, personIcon, calendar)
    {
        _workRate = 1f;
        _maxEnergy = 100;
        _currentEnergy = 100;
        _isFainted = false;
        _isWorking = false;
        _inventories = inventories;
    }

    public void AssignHut(Building hut)
    {
        _placeHut = hut;
    }

    public void ConsumeFood(int foodNum)
    {
        Inventory foodInventory = RunTime.gameScreen.GetInventory(ResourceType.FOOD);

        if(foodInventory.TotalNum >= foodNum){
             _currentEnergy += foodNum * 10;
            foodInventory.Decrease(foodNum);
            if (_currentEnergy > _maxEnergy)
            {
                _currentEnergy = _maxEnergy;
            }
        }
        // Console.WriteLine($"Person consumed food, energy increased by {foodNum}. Current energy: {CurrentEnergy}");
    }

    public void Work()
    {
        if(_isSleeping) return;
        if (!_isWorking) return;
        if (_isFainted) return;
        if (_resourceArea == null && _workPlaceAsWorkplace == null) return;

        // _resourceArea.Extract(_inventories.Find(inv => inv.Type == _resourceArea.ResourceType)!);
        _workTimer += GetFrameTime();
        if (_workTimer < _workRate) return;

        if(_resourceArea != null){
            ResourceType resourceType = _resourceArea.ResourceType;
            foreach (Inventory inventory in _inventories)
            {
                if (inventory.Type == resourceType)
                {
                    _resourceArea.Extract(inventory);
                }
            }
        }else if(_workPlaceAsWorkplace != null){
            // Workplace workPlace = _workPlaceAsWorkplace;
            // workPlace.Produce();
            Workplace workPlace = _workPlaceAsWorkplace;
            switch(workPlace)
            {
                case Kitchen kitchen:
                    foreach (Inventory inventory in _inventories)
                    {
                        if (inventory.Type == ResourceType.FOOD)
                        {
                            kitchen.Cook(inventory);
                        }
                    }
                    break;
                case Clinic clinic:
                    // clinic.Heal();
                    break;
                // Add more cases for other Workplace types as needed
                default:
                    break;
            }
        }

        _workTimer -= _workRate;
        if (_workTimer < 0f) _workTimer = 0f;
    }


    public void Sleep()
    {
        if(_isSleeping == true){
            _isWalking = true;
            SetDestination(_placeHut);
        }
    }

    public void GetDamaged(int healthNum)
    {
    }

    public override void Draw()
    {
        if (!_isWalking)
        {
            if(!_isSleeping && !_isWorking)
            {
                Console.WriteLine("Hello");
                Random rng = new Random();
                SetDestination(new Vector2(rng.Next(0, GetScreenWidth()- Width), rng.Next(0, GetScreenHeight() - Height)));
            }
            else
            {
                Console.WriteLine("?");
                base.Draw();
                Sleep();
                Work();
            }

            // base.Draw();
            // Sleep();
            // Work();

        }
        else
        {

            int finalWalkRate = (int)Math.Ceiling(WalkRate * (float)Math.Round((CurrentEnergy / 100.0),1 ));
            int finalDestinationX;
            int finalDestinationY;
            if (_destination == null && _destionationXY == null)
            {
                base.Draw();
                return;
            }else if(_destination == null && _destionationXY != null){
                Console.WriteLine("Self x " + X+ " y " + Y +  " Destination XY not null " + _destionationXY.X + " " + _destionationXY.Y);
                finalDestinationX = (int)_destionationXY.X;
                finalDestinationY = (int)_destionationXY.Y;
            }
            else
            {
                finalDestinationX = (int)_destination.X + 30;
                finalDestinationY = (int)_destination.Y + 50;
            }

            Console.WriteLine("Walk rate " + finalWalkRate);
            
            // int finalDestinationX = (int)_destination.X + 30;
            // int finalDestinationY = (int)_destination.Y + 50;
        

            if(MathF.Abs(this.X - finalDestinationX ) > finalWalkRate){
                if(finalDestinationX > this.X){
                    this.Icon = RunTime.PersonRight;
                }else{
                    this.Icon = RunTime.PersonLeft;
                }
                this.X += MathF.Sign(finalDestinationX - this.X) * finalWalkRate;
            }else if(MathF.Abs(this.Y - finalDestinationY ) > finalWalkRate){
                if(finalDestinationY > this.Y){
                    this.Icon = RunTime.PersonDown;
                }else{
                    this.Icon = RunTime.PersonUp;
                }
                this.Y += MathF.Sign(finalDestinationY - this.Y) * finalWalkRate;
            }

            if(MathF.Abs(this.X - finalDestinationX) <= finalWalkRate && MathF.Abs(this.Y - finalDestinationY) <= finalWalkRate){
                this.Icon = RunTime.PersonDown;
                _isWalking = false;
                if(_destination is ResourceArea){
                    // ((ResourceArea)_destination).AssignWorker(this);
                    _destination = null;
                }
                else if(_destination is Building){
                    _destination = null;
                }
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
    
    public void SetDestination(Vector2 destinationXY)
    {
        _destionationXY = destinationXY;
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

    private void DailyEnergyReduce()
    {
        if(_currentEnergy > 40)
        {
            if(CurrentEnergy > 60){
                _currentEnergy -= 10;
                if (_currentEnergy < 0)
                {
                    _currentEnergy = 0;
                }
            }else{
                _currentEnergy -= 5;
            }

        }
    }

    public override void Update(Calendar calendar)
    {
        if(calendar.IsDay){
            if(_isSleeping){
                DailyEnergyReduce();
                ConsumeFood(1);
                _isSleeping = false;
                if(ResourceArea != null){
                    SetDestination(ResourceArea);
                }else if(WorkPlaceAsWorkplace != null){
                    SetDestination(WorkPlaceAsWorkplace);
                }
                Console.WriteLine($"{Name} woke up.");
            }
        }else{
            if(!_isSleeping){
                _isWalking = true;
                _isSleeping = true;
            }
        }
    }

    public bool IsFainted
    {
        get { return _isFainted; }
        set { _isFainted = value; }
    }

    public override void Clone()
    {
    }

    public ResourceArea? ResourceArea
    {
        get { return _resourceArea; }
    }

    public Workplace? WorkPlaceAsWorkplace
    {
        get { return _workPlaceAsWorkplace; }
    }

    public Building PlaceHut
    {
        get { return _placeHut; }
    }

    public void AssignWork(ResourceArea resourceArea)
    {
        _isWorking = true;
        if(_workPlaceAsWorkplace != null){
            _workPlaceAsWorkplace.RemoveWorker(this);
            _workPlaceAsWorkplace = null;
        }
        _resourceArea = resourceArea;
    }   

    public void AssignWork(Workplace workplace)
    {
        _isWorking = true;
        if(_resourceArea != null){
            _resourceArea = null;
        }
        _workPlaceAsWorkplace = workplace;
    }

    public void QuitWork(){
        _isWorking = false;
        if(_resourceArea != null){
            _resourceArea.RemoveWorker(this);
            _resourceArea = null;
        }
        if(_workPlaceAsWorkplace != null){
            _workPlaceAsWorkplace.RemoveWorker(this);
            _workPlaceAsWorkplace = null;
        }
    }


    public void RemoveResourceArea()
    {
        _resourceArea = null;
    }

    public void RemoveWorkPlaceAsWorkplace()
    {
        _workPlaceAsWorkplace = null;
    }

    public bool IsSleeping
    {
        get { return _isSleeping; }
    }
}