using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class Cannon : Defense
{
    public Cannon(string name, float xPos, float yPos, int width, int height, Texture2D buildingIcon, int range)
        : base(name, xPos, yPos, width, height,  buildingIcon, range,(int) LandCosts.CanonWoodCost,  (int) LandCosts.CanonStoneCost)
    {
    }

    public override void Draw()
    {
        if(!RunTime.gameScreen.MainCalendar.IsDay){
            if(_currentPeople.Count > 0){
            Enemy? nearestEnemy = DetectEnemy();
            if (_enemyFound && nearestEnemy != null){
                if(!_isAttacking){
                    Attack(nearestEnemy);
                }else{
                    Shoot(nearestEnemy);
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

    
    public override void Remove(Person person)
    {
        _currentPeople.Remove(person);
    }

    public override void ReleaseAllPeople(){
        foreach(Person person in _currentPeople){
            person.IsWorking = false;
            person.NightShift = false;
            person.RemoveWorkPlaceAsWorkplace();
        }
        _currentPeople.Clear();
    }
    
}