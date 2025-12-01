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
    private bool _isHealing = false;
    private bool _isSleeping = false;
    private bool _isWorking;
    private bool _nightShift;
    private float _workRate;
    private float _workTimer = 0f;
    
    private List<Inventory> _inventories;
    private Vector2 _destionationXY;
    private BaseObj? _destination;
    private ResourceArea? _resourceArea;
    private Building _placeHut;
    private Workplace? _workPlaceAsWorkplace;
    private Defense? _defenseBuilding;

    public bool IsWorking 
    { 
        get => _isWorking; 
        set => _isWorking = value; 
    }

    public int CurrentEnergy 
    { 
        get => _currentEnergy; 
    }
    
    public Person(string name, float xPos, float yPos, int width, int height, float maxHealth,Texture2D personIcon, Calendar calendar, List<Inventory> inventories )
        : base(name, xPos, yPos, width, height, maxHealth, personIcon, calendar)
    {
        _workRate = 1f;
        _maxEnergy = 100;
        _currentEnergy = 100;
        _isFainted = false;
        _isWorking = false;
        _nightShift = false;
        _inventories = inventories;
    }

    public void AssignHut(Building hut)
    {
        _placeHut = hut;
    }

    public void RemoveHut()
    {
        _placeHut = null;
    }

    public void ConsumeFood(int foodNum)
    {
        Inventory foodInventory = RunTime.gameScreen.GetInventory(ResourceType.FOOD);

        if(foodInventory.TotalNum >= foodNum){
             _currentEnergy += foodNum * 15;
            foodInventory.Decrease(foodNum);
            if (_currentEnergy > _maxEnergy)
            {
                _currentEnergy = _maxEnergy;
            }
        }
    }

    public void Work()
    {
        if(_isSleeping) return;
        if (!_isWorking) return;
        if (_isFainted) return;
        if (_resourceArea == null && _workPlaceAsWorkplace == null && _defenseBuilding == null) return;
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
                    clinic.Heal();
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
        CurrentHealth -= healthNum;
        if(CurrentHealth <= 0)
        {
            _isFainted = true;
            _isWalking = false;
            _isWorking = false;
            _nightShift = false;
            _resourceArea = null;
            _workPlaceAsWorkplace = null;
            _defenseBuilding = null;
            RunTime.gameScreen.AddMessage("" + Name + " has fainted!", AlertType.ERROR);
        }
    }

    public override void Draw()
    {
        if (!_isFainted)
        {
           if (!_isWalking)
        {
            if(!_isSleeping && !_isWorking && !_nightShift)
            {
                Random rng = new Random();
                SetDestination(new Vector2(rng.Next(0, GetScreenWidth()- Width), rng.Next(0, GetScreenHeight() - Height)));
            }
            else
            {
                base.Draw();
                Sleep();
                Work();
            }

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
                finalDestinationX = (int)_destionationXY.X;
                finalDestinationY = (int)_destionationXY.Y;
            }
            else
            {
                finalDestinationX = (int)_destination.X + 30;
                finalDestinationY = (int)_destination.Y + 50;
            }

            
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
    }

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
        int mainWalkRate = 600 / calendar.DayCriteria;
        switch (calendar.CurrentWeather)
        {
            case Weather.Normal:
                WalkRate = mainWalkRate * 1;
                break;
            case Weather.Stormy:
                if(CurrentEnergy > 50)
                {
                    _currentEnergy -= 10;
                }
                WalkRate = (int) Math.Round(mainWalkRate * 0.7f);
                break;
            case Weather.Snowy:
                if(CurrentEnergy > 50)
                {
                    _currentEnergy -= 20;
                }
                WalkRate = (int) Math.Round(mainWalkRate * 0.5f);
                break;
            default: 
                WalkRate = mainWalkRate * 1;
                break;
        }

        if(calendar.IsDay){
            DailyEnergyReduce();
             ConsumeFood(1);
            if(_defenseBuilding != null){
                _isSleeping = true;
            }else{
                 _isSleeping = false;
                if(ResourceArea != null){
                    SetDestination(ResourceArea);
                }else if(WorkPlaceAsWorkplace != null){
                    SetDestination(WorkPlaceAsWorkplace);
                }
            }
        }else{
            if(_defenseBuilding == null){
                _isWalking = true;
                _isSleeping = true;
            }else{
                SetDestination(_defenseBuilding);
            }

        }
    }

    public bool IsFainted
    {
        get { return _isFainted; }
        set { _isFainted = value; }
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

    public void AssignWork(BaseObj target){
        QuitWork();
        if(target is Defense){
            _nightShift = true;
            _defenseBuilding = (Defense)target;
        }else{
            _isWorking = true;
            if(target is ResourceArea){
                _resourceArea = (ResourceArea)target;
            }else if(target is Workplace){
                _workPlaceAsWorkplace = (Workplace)target;
            }
        }
    }

    public void QuitWork(){
        _isWorking = false;
        _nightShift = false;
        if(_resourceArea != null){
            _resourceArea.RemoveWorker(this);
            _resourceArea = null;
        }
        if(_workPlaceAsWorkplace != null){
            _workPlaceAsWorkplace.RemoveWorker(this);
            _workPlaceAsWorkplace = null;
        }
        if(_defenseBuilding != null){
            _defenseBuilding.Remove(this);
            _defenseBuilding = null;
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

    public Defense? DefenseBuilding
    {
        get { return _defenseBuilding; }
    }

    public bool IsSleeping
    {
        get { return _isSleeping; }
    }

    public bool NightShift
    {
        get { return _nightShift; }
        set { _nightShift = value; }
    }

    public void Heal(int healNum)
    {
        CurrentHealth += (float) healNum * 0.5f;
        Console.WriteLine("Healing by: " + (float) Math.Round(healNum * 0.5f));
        Console.WriteLine("heal : " + CurrentHealth);
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void BecomePatient(){
        _isHealing = true;
    }

    public void RecoverFromIllness(){
        _isHealing = false;
    }

    public bool IsHealing
    {
        get { return _isHealing; }
    }
}