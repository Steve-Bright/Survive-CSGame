using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Game;
public abstract class Defense : Building
{
    protected List<Person> _currentWorkers;
    protected float _critChance;
    protected int _range;
    protected int _hitRate;
    protected int _coolDownTime;
    private bool _peopleListOpen = false;
    protected bool _isAttacking = false;
    private bool _randomAssign = false;
    protected bool _enemyFound = false;
    protected int _bulletX ;
    protected int _bulletY;
    protected float _attackTimer = 0f;

    protected float _attackDuration = 1.0f; // Example duration for an attack cycle

    protected int _requiredWorkers;
    private List<ResourcePerson> _resourcePersons;

    public Defense(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon,  int maxPerson, float critChance, int range, int hitRate, int woodCost = 60, int stoneCost = 60, int capacityLimit = 4)
        : base(name, xPos, yPos, width, height,  buildingIcon, woodCost, stoneCost, capacityLimit)
    {
        _requiredWorkers = maxPerson;
        _currentWorkers = new List<Person>();
        _critChance = critChance;
        _range = range;
        _hitRate = hitRate;
        _resourcePersons = new List<ResourcePerson>();
    }
    
    // Abstract methods specific to Defense
    public virtual void Attack(BaseObj target)
    {
        _isAttacking = true;
        _attackTimer = 0f;
    }

    public virtual void Assign(Person person){
        if (_currentWorkers.Count < _requiredWorkers){
            PlaySound(RunTime.infoSound);
            person.AssignNightShift(this);
            _currentWorkers.Add(person);
        }
    }
    public abstract void Remove(Person person);
    
    public override abstract void TakeDamage(int hitpoint);

    public override void Draw()
    {
        base.Draw();
    }

    protected Enemy? DetectEnemy()
    {
        Enemy? closest = null;
        float rangeSq = (float)_range * (float)_range;

        List<Enemy> enemies = RunTime.gameScreen.GetEnemyLists().Where(e => !e.IsDead).ToList();
        foreach (Enemy en in enemies)
        {
            float dx = en.X - this.X;
            float dy = en.Y - this.Y;
            float dsq = dx * dx + dy * dy;
            
            if (dsq <= rangeSq)
            {
                if (closest == null)
                {
                    closest = en;
                }
                else
                {
                    float cdx = closest.X - this.X;
                    float cdy = closest.Y - this.Y;
                    float cdsq = cdx * cdx + cdy * cdy;
                    if (dsq < cdsq)
                        closest = en;
                }
            }
        }

        if(closest!= null){
            _enemyFound = true;
        }

        return closest;
    }

    public override void DisplayDetails()
    {
        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 260, GetScreenHeight()-245, 150);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Defense", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        // Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Range: {_range} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Hit Rate: {_hitRate}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Crit Chance: {_critChance}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Cooldown: {_coolDownTime}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        Util.UpdateText($"Max Person: {_requiredWorkers}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        Util.UpdateText($"Current: {_currentWorkers.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 

        Rectangle buttonRect = new Rectangle((GetScreenWidth() / 2) + 460, GetScreenHeight()-45, 265, 45 );
        DrawRectangleRec(buttonRect, Color.Red);
        Util.MakeButton(buttonRect, "Assign",(GetScreenWidth() / 2) + 480, GetScreenHeight()-85, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Gold, () => Util.OpenAssignList());
        if(Util.AssignListOpen)
        {
            Util.AssignList(AssignType.Defense, this, "Assign Workers", _currentWorkers, _requiredWorkers, Assign);
        }

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }        
    }
    protected void Shoot(Enemy target)
    {
        target.GetDamaged(1);
    }
}