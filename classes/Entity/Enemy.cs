using System;
using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
namespace Game;


internal enum Side{
    Left,
    Right,
    Top,
    Bottom
}

public class Enemy : Entity
{
    private bool _hasDied;
    private Side _spawnSide;
    private float _detectionRadius = 300;
    private BaseObj? _detectedTarget = null;
    private bool _isChasing = false;
    // attack animation state (no damage applied here)
    private bool _isAttacking = false;
    private float _attackTimer = 0f;
    private float _attackFrameTime = 0.2f; // seconds per attack frame
    private float _attackDuration = 1.0f; // total animation duration

    public Enemy(string name, float xPos, float yPos, int width, int height, float maxHealth, Texture2D enemyIcon, Calendar calendar)
        : base(name, xPos, yPos, width, height, maxHealth, enemyIcon, calendar)
    {
        _hasDied = false;
        _spawnSide = Side.Left;
    }

    public void Attack(BaseObj target)
    {

        _isAttacking = true;
        _attackTimer = 0f;
    }

    public override void Draw()
    {
        if(!RunTime.currentCalendar.IsDay && !_hasDied){
                    // If currently playing attack animation, draw attack frames and return
            if (_isAttacking)
            {
                _attackTimer += GetFrameTime();
                int frame = (int)(_attackTimer / _attackFrameTime) % 2; // two-frame attack

                switch (_spawnSide)
                {
                    case Side.Left:
                        Icon = frame == 0 ? RunTime.zombie_leftAtk : RunTime.zombie_leftAtk2;
                        break;
                    case Side.Right:
                        Icon = frame == 0 ? RunTime.zombie_rightAtk : RunTime.zombie_rightAtk2;
                        break;
                    case Side.Top:
                        Icon = frame == 0 ? RunTime.zombie_upAtk : RunTime.zombie_upAtk2;
                        break;
                    case Side.Bottom:
                        Icon = frame == 0 ? RunTime.zombie_downAtk : RunTime.zombie_downAtk2;
                        break;
                }

                if(_detectedTarget is Person)
                {
                    ((Person)_detectedTarget).GetDamaged(1) ;
                }else if(_detectedTarget is Building)
                {
                    ((Building)_detectedTarget).GetDamaged(1);
                }

                base.Draw();

                if (_attackTimer >= _attackDuration)
                {
                    _isAttacking = false;
                    _attackTimer = 0f;
                }

            }else{
               DetectSurroundings();
            if (_isChasing && _detectedTarget is BaseObj target)
            {
                float dx = target.X - this.X;
                float dy = target.Y - this.Y;

                if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2)
                {
                    // reached target
                    _isChasing = false;
                    Attack(target);
                    base.Draw();

                }else{
                    int chaseSpeed = Math.Min(2, WalkRate);

                    if (MathF.Abs(dx) > MathF.Abs(dy))
                    {

                        // move horizontally toward target
                        if (dx > 0)
                        {
                            X += chaseSpeed;
                            Icon = RunTime.zombie_right;
                        }
                        else
                        {
                            X -= chaseSpeed;
                            Icon = RunTime.zombie_left;
                        }
                    }
                    else
                    {
                        // move vertically toward target
                        if (dy > 0)
                        {
                            Y += chaseSpeed;
                            Icon = RunTime.zombie_down;
                        }
                        else
                        {
                            Y -= chaseSpeed;
                            Icon = RunTime.zombie_up;
                        }
                    }

                    // stop chasing if target moves too far
                    float distSq = (target.X - this.X) * (target.X - this.X) + (target.Y - this.Y) * (target.Y - this.Y);
                    if (distSq > (_detectionRadius * 1.5f) * (_detectionRadius * 1.5f))
                    {
                        _isChasing = false;
                        _detectedTarget = null;
                    }

                    base.Draw();
                    return;
                }
            }else{
                int screenW = GetScreenWidth();
                int screenH = GetScreenHeight();
                int speed = 1;
                int thrW = Math.Max(1, screenW / 5);
                int thrH = Math.Max(1, screenH / 3);

                switch (_spawnSide)
                {
                    case Side.Left:
                    if (X < thrW)
                    {
                        X += speed;
                        Icon = RunTime.zombie_right;
                    }
                    else
                    {
                        if (Y < thrH)
                        {
                            Y += speed; // move down away from top
                            Icon = RunTime.zombie_down;
                        }
                        else if (Y > screenH - Height - thrH)
                            {
                            Y -= speed; // move up away from bottom
                            Icon = RunTime.zombie_up;
                        }
                        else
                            {
                                X += speed; // continue inward
                                Icon = RunTime.zombie_right;
                            }
                    }
                    break;
                    case Side.Right:
                            // move left first; once past threshold, steer vertically if near edges
                            if (X > screenW - thrW - Width)
                            {
                                X -= speed;
                                Icon = RunTime.zombie_left;
                            }
                            else
                            {
                                if (Y < thrH)
                                {
                                    Y += speed; // move down away from top
                                    Icon = RunTime.zombie_down;
                                }
                                else if (Y > screenH - Height - thrH)
                                {
                                    Y -= speed; // move up away from bottom
                                    Icon = RunTime.zombie_up;
                                }
                                else
                                {
                                    X -= speed; // continue inward
                                    Icon = RunTime.zombie_left;
                                }
                            }
                            break;
                        case Side.Top:
                            // move down first; once past threshold, steer horizontally if near edges
                            if (Y < thrH)
                            {
                                Y += speed;
                                Icon = RunTime.zombie_down;
                            }
                            else
                            {
                                if (X < thrW)
                                {
                                    X += speed; // move right away from left
                                    Icon = RunTime.zombie_right;
                                }
                                else if (X > screenW - Width - thrW)
                                {
                                    X -= speed; // move left away from right
                                    Icon = RunTime.zombie_left;
                                }
                                else
                                {
                                    Y += speed; // continue downward
                                    Icon = RunTime.zombie_down;
                                }
                            }
                            break;
                        case Side.Bottom:
                            // move up first; once past threshold, steer horizontally if near edges
                            if (Y > screenH - thrH - Height)
                            {
                                Y -= speed;
                                Icon = RunTime.zombie_up;
                            }
                            else
                            {
                                if (X < thrW)
                                {
                                    X += speed; // move right away from left
                                    Icon = RunTime.zombie_right;
                                }
                                else if (X > screenW - Width - thrW)
                                {
                                    X -= speed; // move left away from right
                                    Icon = RunTime.zombie_left;
                                }
                                else
                                {
                                    Y -= speed; // continue upward
                                    Icon = RunTime.zombie_up;
                                }
                            }
                            break;
                    }

                    base.Draw();
                } 
            }


            
        }
    }

    public void DetectSurroundings()
    {
        List<Person> people = RunTime.gameScreen.GetPersonLists().FindAll(p => !p.IsFainted && p.PlaceHut == null);
        List<Building> buildings = RunTime.gameScreen.GetBuildingLists();
        float bestSq = _detectionRadius * _detectionRadius;
        bool personFound = false;
        BaseObj? found = null;

        foreach (Person p in people)
        {
            float dx = p.X - this.X;
            float dy = p.Y - this.Y;
            float dsq = dx * dx + dy * dy;
            if (dsq <= bestSq)
            {
                bestSq = dsq;
                found = p;
                personFound = true;
            }
        }

        if(!personFound){

            foreach (Building b in buildings)
            {
                float dx = b.X - this.X;
                float dy = b.Y - this.Y;
                float dsq = dx * dx + dy * dy;
                if (dsq <= bestSq)
                {
                    bestSq = dsq;
                    found = b;
                }
            }
        }

        if (found != null)
        {
            _detectedTarget = found;
            _isChasing = true;
        }
        else
        {
            // lose interest if nothing nearby
            _detectedTarget = null;
            _isChasing = false;
        }
    }

    public void GetDamaged(int healthNum)
    {
        CurrentHealth -= (healthNum/3f);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void SetRandomLocation()
    {
        Random _rand = new Random();
        // Spawn the enemy just outside one of the four screen edges so it
        // naturally walks into the visible play area. Use a shared Random
        // instance to avoid identical seeding when called frequently.
        int screenW = GetScreenWidth();
        int screenH = GetScreenHeight();
        int margin = 10; // extra offset outside the visible area

        int side = _rand.Next(0, 4); // 0=left,1=right,2=top,3=bottom
        switch (side)
        {
            case 0: // left
                _spawnSide = Side.Left;
                X = -Width - margin;
                Y = _rand.Next(0, Math.Max(1, screenH - Height));
                break;
            case 1: // right
                _spawnSide = Side.Right;
                X = screenW + margin;
                Y = _rand.Next(0, Math.Max(1, screenH - Height));
                break;
            case 2: // top
                _spawnSide = Side.Top;
                Y = -Height - margin;
                X = _rand.Next(0, Math.Max(1, screenW - Width));
                break;
            default: // bottom
                _spawnSide = Side.Bottom;
                Y = screenH + margin;
                X = _rand.Next(0, Math.Max(1, screenW - Width));
                break;
        }
    }

    public void Die()
    {
        _hasDied = true;
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
                _detectionRadius = 200;
                WalkRate = (int) Math.Round(mainWalkRate * 0.8f);
                break;
            case Weather.Snowy:
                _detectionRadius = 200;
                WalkRate = (int) Math.Round(mainWalkRate * 0.5f);
                break;
            default: 
                WalkRate = mainWalkRate * 1;
                break;
        }
    }

    public Enemy Clone()
    {
        return new Enemy(Name, X, Y, Width, Height, MaxHealth, Icon, RunTime.currentCalendar);
    }

    public override void DisplayDetails(){

        if (!_hasDied)
        {
            Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
            closeIndicator.Draw();
            Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
            Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
            Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-50, 262 , 50, 1, 1);
            Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 1, 2);
            bottomIndicator.Draw();
            indicatorInsideBottom.Draw();
            photoIndicator.Draw();

            Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 28 );
            DrawRectangleRec(nameRect, new Color(255, 204, 106));
            // Util.UpdateText(nameRect, "John Doe", (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 250, GetScreenHeight()-230, 160);
            Util.UpdateText(Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28);

            Util.UpdateText("Type: Enemy", (GetScreenWidth() / 2) + 480, GetScreenHeight()-240, 28);
            Util.UpdateText("Status: Active", (GetScreenWidth() / 2) + 740, GetScreenHeight()-240, 28);
            Util.UpdateText($"Health: {Math.Round(CurrentHealth)}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
            Util.UpdateText("Effect: None", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);

            Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28); 
            Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);
            Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 

            RunTime.detailsShown = true;
            if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
            {
                IsSelected = false;
                RunTime.detailsShown = false;
            }
        }
            
    }

    public bool IsDead
    {
        get { return _hasDied; }
    }
}