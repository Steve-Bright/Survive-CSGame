namespace Game;

using System;

public class Inventory
{
    private ResourceType _type;
    private int _totalNum;

    public Inventory(ResourceType type)
    {
        _type = type;
        _totalNum = 0;
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
}