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

    public float CurrentTime {
        get => _currentTime;
        set => _currentTime = value;
    }
    public int CurrentDay => _currentDay;
    public bool isDay => _isDay;
    public Weather CurrentWeather => _currentWeather;

    public Calendar()
    {
        //swamhtet this is to changed later into 300 second for day and 600 seconds for night.
        _dayCriteria = 24;
        _nightCriteria = 48;
        _subscribers = new List<Entity>();
        _currentWeather = Weather.Sunny;
    }

    public void StartCalendar(Weather startingWeather = Weather.Sunny)
    {
        _previousRoundedTimeResult = 0;
        _hourSystem = 6;
        _currentTime = 0;
        _currentDay = 1;
        _isDay = true;
        _currentWeather = startingWeather;
    }

    public void EndCalendar()
    {
        Console.WriteLine("Calendar stopped.");
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
        // int peopleAlive = RunTime.gameScreen.GetPersonLists().FindAll(p => !p.IsFainted).Count;
        // if(peopleAlive <= 0)
        // {
        //     RunTime.isGameOver = false;
        //     RunTime.CurrentWindow = ScreenType.ConditionScreen;
        //     Console.WriteLine("Lost condition reached!");
        // }
        // else
        // {
        //     if (_currentDay >= 10)
        //     {
        //         RunTime.isGameOver = true;
        //         RunTime.CurrentWindow = ScreenType.ConditionScreen;
        //     }
        // }
    }

    public void ToggleNight()
    {
        _isDay = false;
        Notify();
    }

    public void PassMidnight(){
        _currentDay++;
        _hourSystem = 0;

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

    public void Update(float deltaTime)
    {
        // _currentTime += deltaTime;

        // if (_currentTime >= 24f)
        //     EndADay();

        // if (_currentTime >= 18f && _isDay)
        //     ToggleDayNight();

        // if (_currentTime >= 6f && !_isDay)
        //     ToggleDayNight();
    }

    public int DayCriteria => _dayCriteria;
    public int NightCriteria => _nightCriteria;
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
}