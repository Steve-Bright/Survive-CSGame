using System;
using Raylib_cs;

namespace Game;

public class Calendar
{
    private int _dayCriteria,  _nightCriteria;
    private float _currentTime;
    private int _previousRoundedTimeResult;
    private int _hourSystem;
    private int _currentDay;
    private bool _isDay;
    private Weather _currentWeather;

    private readonly List<Entity> _subscribers;

    public Calendar()
    {
        //swamhtet this is to changed later into 300 second for day and 600 seconds for night.
        _dayCriteria = 90;
        _nightCriteria = 180;
        _subscribers = new List<Entity>();
        _currentWeather = Weather.Normal;
    }

    public void StartCalendar(Weather startingWeather = Weather.Normal)
    {
        _previousRoundedTimeResult = 0;
        _hourSystem = 6;
        _currentTime = 0;
        _currentDay = 1;
        _isDay = true;
        _currentWeather = startingWeather;
    }

    public void ChangeWeather()
    {
        Array values = Enum.GetValues(typeof(Weather));
        Random rng = new Random();
        _currentWeather = (Weather)values.GetValue(rng.Next(values.Length));

        Notify(); 
    }

    public void EndADay()
    {

        _hourSystem = 6;
        _currentTime = 0f;
        _isDay = true;
        ChangeWeather();
        Notify();
    }

    public void CheckWinLostCondition()
    {
        int peopleAlive = RunTime.gameScreen.GetPersonLists().FindAll(p => !p.IsFainted).Count;
        if(peopleAlive <= 0)
        {
            RunTime.isGameOver = false;
            RunTime.CurrentWindow = ScreenType.ConditionScreen;
            Console.WriteLine("Lost condition reached!");
        }
        else
        {
            if (_currentDay >= 5)
            {
                RunTime.isGameOver = true;
                RunTime.CurrentWindow = ScreenType.ConditionScreen;
            }
        }
    }

    public void ToggleNight()
    {
        RunTime.gameScreen.AddMessage("Enemies are coming out!", AlertType.WARNING);
        _isDay = false;
        Notify();
    }

    public void PassMidnight(){
        _currentDay++;
        _hourSystem = 0;

    }

    public void PassAnotherDay(){
        _currentDay++;
    }

    public void Subscribe(Entity entity)
    {
        if (!_subscribers.Contains(entity))
            _subscribers.Add(entity);
    }

    public void Unsubscribe(Entity entity)
    {
        if (_subscribers.Contains(entity))
            _subscribers.Remove(entity);
    }

    public void Notify()
    {
        foreach (var entity in _subscribers)
            entity.Update(this);
    }

    public int DayCriteria {
        get => _dayCriteria;
    }

    public int NightCriteria
    {
        get => _nightCriteria;
    }
    public int HourSystem
    {
        get => _hourSystem;
        set => _hourSystem = value;
    }

    public int PreviousRoundedResult
    {
        get => _previousRoundedTimeResult;
        set => _previousRoundedTimeResult = value;
    }

    public bool IsDay
    {
        get => _isDay;
        set => _isDay = value;
    }

    public float CurrentTime {
        get => _currentTime;
        set => _currentTime = value;
    }

     public int CurrentDay
    {
        get => _currentDay;
    }

    public bool isDay
    {
        get => _isDay;
    }
    public Weather CurrentWeather{
        get => _currentWeather;
    }
}