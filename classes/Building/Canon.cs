using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class Cannon : Defense
{
    public Cannon(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int maxPerson, float critChance, int range, int hitRate, int woodCost = (int) LandCosts.CanonWoodCost, int stoneCost = (int) LandCosts.CanonStoneCost)
        : base(name, xPos, yPos, width, height,  buildingIcon,  maxPerson, critChance, range, hitRate, woodCost, stoneCost)
    {
    }

    public override void Draw()
    {
        // Console.WriteLine("Current worker count: " + _currentWorkers.Count);
        if(!RunTime.gameScreen.MainCalendar.IsDay){
            if(_currentWorkers.Count > 0){
            Enemy? nearestEnemy = DetectEnemy();
            if (_enemyFound && nearestEnemy != null){
                if(!_isAttacking){
                    Attack(nearestEnemy);
                }else{
                    Shoot(nearestEnemy);
                    Console.WriteLine("Cannon width " + Width + " X position " + X);
                    Vector2 weaponPos = new Vector2(X + ( Width / 2 ), Y + 20);
                    Vector2 enemyPos = new Vector2(nearestEnemy.X + (nearestEnemy.Width / 2), nearestEnemy.Y + (nearestEnemy.Height / 2));
                    DrawLineEx(weaponPos, enemyPos, 10, new Color(0, 87, 173));
                    if(nearestEnemy.IsDead){   
                        nearestEnemy = null;
                        _enemyFound = false;
                        _isAttacking = false;
                    }
                    Icon = RunTime.cannonatk;
                }
            }
            else{ 
                Icon = RunTime.Cannon;
            }
            }
        }else{
            if(_isAttacking){
                _isAttacking = false;
                Icon = RunTime.Cannon;
            }
        }
        base.Draw();
    }

    // public override void Attack(BaseObj target)
    // {
    //     if (_currentWorkers.Count > 0)
    //     {
    //         Console.WriteLine("Cannon firing a heavy projectile.");
    //     }
    // }
    
    // public override void Assign(Person person)
    // {
    //     if (_currentWorkers.Count < _requiredWorkers) _currentWorkers.Add(person);
    // }
    
    public override void Remove(Person person)
    {
        _currentWorkers.Remove(person);
    }
    
    public override void TakeDamage(int hitpoint)
    {
        // _currentDurability -= hitpoint;
        // if (_currentDurability <= 0) Unload();
    }

    public void ReleaseAllResidents(){
        foreach(Person person in _currentWorkers){
            person.IsWorking = false;
            person.NightShift = false;
            person.RemoveWorkPlaceAsWorkplace();
        }
        _currentWorkers.Clear();
    }
    
    public override void Clone() { }
}