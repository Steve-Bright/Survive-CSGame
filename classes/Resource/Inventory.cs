namespace Game;

using System;

public class Inventory
{
    private ResourceType _type;
    private float _totalNum;

    public Inventory(ResourceType type, int initialNum)
    {
        _type = type;
        _totalNum = initialNum;
    }

    public void Increase(float num)
    {
        int actualIncrease = (int)Math.Floor(num);
        if (actualIncrease > 0)
        {
            _totalNum += actualIncrease;
        }
    }

    public void Decrease(int num)
    {
        if (num > 0)
        {
            _totalNum = Math.Max(0, _totalNum - num);
        }
    }

    public float TotalNum
    {
        set => _totalNum = value;
        get => _totalNum;
    }

    public ResourceType Type
    {
        get => _type;
    }
}