using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public class ResourceArea : BaseObj
{
    
    private int _capacity;
    private ResourceType _resource;
    private List<ResourcePerson> _resourcePersons;
    private bool _isAccessible;
    private int _requiredWorkers;
    private List<Person> _currentWorkers;
    private bool _peopleListOpen = false;
    private bool _randomAssign = true;

    // + ResourceArea(name: string, xPos: float, yPos: float, width: int, height: int, type: ResourceType)
    public ResourceArea(string name, float xPos, float yPos, int width, int height, Texture2D resourceAreaIcon,  ResourceType type)
        : base(name, xPos, yPos, width, height, resourceAreaIcon)
    {
        _capacity = 4000; //default - 2000
        _resource = type;
        _isAccessible = true;
        _requiredWorkers = 5;
        _currentWorkers = new List<Person>();
        _resourcePersons = new List<ResourcePerson>();
    }

    public override void Draw()
    {
        base.Draw();
        _currentWorkers.RemoveAll(person => person.IsFainted);  
    }

    public override void DisplayDetails()
    {
        string resourceAreaStatus = _isAccessible ? "Active" : "Inactive";

        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 230, GetScreenHeight()-245, 200);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Resource", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Resource: {_resource.ToString().ToLower()}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Max Workers: {_requiredWorkers}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Current: {_currentWorkers.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Capacity: {_capacity}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Status: {resourceAreaStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);


        if(_capacity > 0)
        {

            Rectangle buttonRect = new Rectangle((GetScreenWidth() / 2) + 460, GetScreenHeight()-85, 265, 45 );
            DrawRectangleRec(buttonRect, Color.Red);
            Util.MakeButton(buttonRect, "Assign",(GetScreenWidth() / 2) + 480, GetScreenHeight()-85, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Gold, () => Util.OpenAssignList());
            // DisplayShopUI();
            if(Util.AssignListOpen)
            {
                Util.AssignList(AssignType.ResourceArea, this, "Assign Workers", _currentWorkers, _requiredWorkers, AssignWorker);
                // DisplayPeopleList();
            }
        
        }

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }            
    }

    public ResourceType ResourceType => _resource;

    public bool IsAccessible => _isAccessible;

    public void AssignWorker(Person person)
    {
        if (_isAccessible)
        {
            PlaySound(RunTime.infoSound);
            person.AssignWork(this);
            _currentWorkers.Add(person);
        }
    }

    public void RemoveWorker(Person person)
    {
        if (_currentWorkers.Contains(person))
        {
            person.RemoveResourceArea();
            _currentWorkers.Remove(person);
            person.IsWorking = false;
        }
    }

    public void Extract(Inventory inv)
    {
        if(_capacity > 0)
        {
            inv.Increase(1);
            _capacity -= 1;
        }
        else
        {
            _currentWorkers.Where(person => person.IsWorking).ToList().ForEach(person => { person.IsWorking = false; person.RemoveResourceArea(); });
             _currentWorkers.Clear();
            RunTime.gameScreen.AddMessage($"{Name} is out of resources.", AlertType.ERROR);
        }
        // if (_currentWorkers.Count > 0 && type == _resource.Type)
        // {
        //     int extractionRate = _currentWorkers.Count; // Simple extraction based on worker count
        //     inv.AddResource(type, extractionRate);
        // }
    }

}

public class ResourcePerson
{
    private Person person;
    private bool isSelected;

    public ResourcePerson(Person person, bool isSelected)
    {
        this.person = person;
        this.isSelected = false;
    }

    public Person Person => person;
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

}