namespace Game;

using System;

public class Inventory
{
    private ResourceType _type;
    private int _totalNum;

    public Inventory(ResourceType type, int initialNum)
    {
        _type = type;
        _totalNum = initialNum;
    }

    public void Increase(int num)
    {
        if (num > 0)
        {
            _totalNum += num;
        }
    }

    public void Decrease(int num)
    {
        if (num > 0)
        {
            _totalNum = Math.Max(0, _totalNum - num);
        }
    }

    public int TotalNum
    {
        get => _totalNum;
    }

    public ResourceType Type
    {
        get => _type;
    }
}