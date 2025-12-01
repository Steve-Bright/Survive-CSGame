using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Game;

public class BuildingFactory
{
    public BuildingFactory()
    {
        
    }

    public Building CreateBuilding(BuildingType buildingType, float xPos, float yPos)
    {
        switch(buildingType)
        {
            case BuildingType.Hut:
                // new Hut("Hut", X + 100 / 2, Y, 100, 100, RunTime.Hut);
                return new Hut("Hut", xPos, yPos, 100, 100, RunTime.Hut);
            case BuildingType.Cannon:
                return new Cannon("Cannon", xPos, yPos, 100, 100, RunTime.Cannon, 300);
            case BuildingType.Clinic:
                return new Clinic("Clinic", xPos, yPos, 120, 120, RunTime.Clinic);
            default:
                return new Kitchen("Kitchen", xPos, yPos, 120, 120, RunTime.Cookery);
        }
    }
}