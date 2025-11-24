using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class Land : BaseObj
{
    private GameScreen _gameScreen;
    private bool _shopOpen = false;
    private Building _building; 
    
    public Land(string name, float xPos, float yPos, int width, int height, Texture2D landIcon, GameScreen gameScreen)
        : base(name, xPos, yPos, width, height, landIcon)
    {
        _building = null;
        _gameScreen = gameScreen;
    }
    

    public override void DisplayDetails()
    {
        string landStatus = (_building == null) ? "Empty" : "Occupied";
        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 230, GetScreenHeight()-240, 200);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Land", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        
        Util.UpdateText($"Status: {landStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);

        if(_building == null)
        {
            Rectangle buttonRect = new Rectangle((GetScreenWidth() / 2) + 460, GetScreenHeight()-125, 265, 45 );
            DrawRectangleRec(buttonRect, Color.Red);
            Util.MakeButton(buttonRect, "Build",(GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Gold, () => _shopOpen = true);
            // DisplayShopUI();
            if(_shopOpen == true)
            {
                DisplayShopUI();
            }
        }
        else
        {
            _building.DisplayDetails();
        }

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }        
    }

    public override Dictionary<string, string> ViewDetails()
    {
        Dictionary<string, string> details = new Dictionary<string, string>
        {
            {"Type", "Land"},
            {"Status", _building != null ? "Occupied" : "Empty"},
        };
        
        if (_building != null)
        {
            details.Add("Building", _building.GetType().Name);
        }
        
        return details;
    }

    private void DisplayShopUI()
    {
        int shopWidth = 1100;
        int shopHeight = 600;
        Rectangle shopRect = new Rectangle((GetScreenWidth()  - shopWidth ) / 2, (GetScreenHeight() - shopHeight) / 2, shopWidth, shopHeight);
        DrawRectangleRec(shopRect, new Color(200, 200, 200, 225));

        
        Util.UpdateText(shopRect, "\nBuildings", (int) shopRect.X, (int) shopRect.Y + 20, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_TOP);
        
        
        Rectangle closeBtnRect = new Rectangle(shopRect.X + shopRect.Width - 45, shopRect.Y, 45, 45);
        DrawRectangleRec(closeBtnRect, new Color(255, 100, 100, 200));
        Util.ScaledDrawTexture(RunTime.CloseIcon, closeBtnRect.X, closeBtnRect.Y, 45);

        if(GetMousePosition().X > closeBtnRect.X && GetMousePosition().X < closeBtnRect.X + closeBtnRect.Width &&  GetMousePosition().Y > closeBtnRect.Y && GetMousePosition().Y < closeBtnRect.Y + closeBtnRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new Hut("Hut", X, Y, 180, 180, RunTime.Hut);
            _gameScreen.AddBaseObj(_building);
            _shopOpen = false;
        }

        Rectangle building1Rect = new Rectangle(shopRect.X + 50, shopRect.Y + 80, 300, 200);
        DrawRectangleRec(building1Rect, new Color(150, 150, 150, 200));
        Util.ScaledDrawTexture(RunTime.Hut, building1Rect.X , building1Rect.Y, 150);
        Rectangle nameRect = new Rectangle(building1Rect.X + 150, building1Rect.Y + 80 - 30, 150 , 30 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106, 0));
        Util.UpdateText(nameRect, "HUT", (int) building1Rect.X + 150, (int)building1Rect.Y + 80, 25, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Wood, building1Rect.X + 165, building1Rect.Y + 115, 50);

        Rectangle woodNumRect = new Rectangle(building1Rect.X + 210, building1Rect.Y + 110, 50 , 35 );
        DrawRectangleRec(woodNumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(woodNumRect, "50", (int) building1Rect.X + 210, (int)building1Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Stone, building1Rect.X + 165, building1Rect.Y + 150, 50);
        Rectangle stoneNumRect = new Rectangle(building1Rect.X + 210, building1Rect.Y + 150, 50 , 35 );
        DrawRectangleRec(stoneNumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(stoneNumRect, "30", (int) building1Rect.X + 210, (int)building1Rect.Y + 145, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        if(GetMousePosition().X > building1Rect.X && GetMousePosition().X < building1Rect.X + building1Rect.Width &&  GetMousePosition().Y > building1Rect.Y && GetMousePosition().Y < building1Rect.Y + building1Rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new Hut("Hut", X + 100/2, Y, 100, 100, RunTime.Hut);
            _gameScreen.AddBaseObj(_building);
            _shopOpen = false;
        }

        //hut done

        Rectangle building2Rect = new Rectangle(shopRect.X + 400, shopRect.Y + 80, 300, 200);
        DrawRectangleRec(building2Rect, new Color(150, 150, 150, 200));
        Util.ScaledDrawTexture(RunTime.Clinic, building2Rect.X, building2Rect.Y, 150);

        Rectangle name2Rect = new Rectangle(building2Rect.X + 150, building2Rect.Y + 80 - 30, 150 , 30 );
        DrawRectangleRec(name2Rect, new Color(255, 204, 106, 0));
        Util.UpdateText(name2Rect, "CLINIC", (int) building2Rect.X + 150, (int)building2Rect.Y + 80, 25, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Wood, building2Rect.X + 165, building2Rect.Y + 115, 50);
        Rectangle wood2NumRect = new Rectangle(building2Rect.X + 210, building2Rect.Y + 110, 50 , 35 );
        DrawRectangleRec(wood2NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(wood2NumRect, "150", (int) building2Rect.X + 210, (int)building2Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Rectangle stone2NumRect = new Rectangle(building2Rect.X + 210, building2Rect.Y + 150, 50 , 35 );
        DrawRectangleRec(stone2NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(stone2NumRect, "100", (int) building2Rect.X + 210, (int)building2Rect.Y + 145, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Stone, building2Rect.X + 165, building2Rect.Y + 150, 50);

        if(GetMousePosition().X > building2Rect.X && GetMousePosition().X < building2Rect.X + building2Rect.Width &&  GetMousePosition().Y > building2Rect.Y && GetMousePosition().Y < building2Rect.Y + building2Rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new Clinic("Clinic", X + 100/2 -15, Y, 120, 120, RunTime.Clinic);
            _gameScreen.AddBaseObj(_building);
            _shopOpen = false;
        }

        //clinic done


        Rectangle building3Rect = new Rectangle(shopRect.X + 750, shopRect.Y + 80, 300, 200);
        DrawRectangleRec(building3Rect, new Color(150, 150, 150, 200));
        Util.ScaledDrawTexture(RunTime.Cookery, building3Rect.X, building3Rect.Y, 150);

        Rectangle name3Rect = new Rectangle(building3Rect.X + 150, building3Rect.Y + 80 - 30, 150 , 30 );
        DrawRectangleRec(name3Rect, new Color(255, 204, 106, 0));
        Util.UpdateText(name3Rect, "KITCHEN", (int) building3Rect.X + 150, (int)building3Rect.Y + 80, 25, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);
        Util.ScaledDrawTexture(RunTime.Wood, building3Rect.X + 165, building3Rect.Y + 115, 50);

        Rectangle wood3NumRect = new Rectangle(building3Rect.X + 210, building3Rect.Y + 110, 50 , 35 );
        DrawRectangleRec(wood3NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(wood3NumRect, "120", (int) building3Rect.X + 210, (int)building3Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        if(GetMousePosition().X > building3Rect.X && GetMousePosition().X < building3Rect.X + building3Rect.Width &&  GetMousePosition().Y > building3Rect.Y && GetMousePosition().Y < building3Rect.Y + building3Rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new Kitchen("Cookery", X + 50/2 + 5, Y, 120, 120, RunTime.Cookery);
            _gameScreen.AddBaseObj(_building);
            _shopOpen = false;
        }

        //cookery done


        Rectangle building4Rect = new Rectangle(shopRect.X + 50, shopRect.Y + 300, 300, 200);
        DrawRectangleRec(building4Rect, new Color(150, 150, 150, 200));
        Util.ScaledDrawTexture(RunTime.Cannon, building4Rect.X, building4Rect.Y, 130);

        Rectangle name4Rect = new Rectangle(building4Rect.X + 130, building4Rect.Y + 80 - 30, 150 , 30 );
        DrawRectangleRec(name4Rect, new Color(255, 204, 106, 0));
        Util.UpdateText(name4Rect, "CANNON", (int) building4Rect.X + 150, (int)building4Rect.Y + 80, 25, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Stone, building4Rect.X + 155, building4Rect.Y + 115, 50);
        Rectangle wood4NumRect = new Rectangle(building4Rect.X + 210, building4Rect.Y + 110, 50 , 35 );
        DrawRectangleRec(wood4NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(wood4NumRect, "200", (int) building4Rect.X + 210, (int)building4Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE); 

        if(GetMousePosition().X > building4Rect.X && GetMousePosition().X < building4Rect.X + building4Rect.Width &&  GetMousePosition().Y > building4Rect.Y && GetMousePosition().Y < building4Rect.Y + building4Rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new Cannon("Cannon", X + 40, Y, 100, 100, RunTime.Cannon, 1, 10, 10, 5);
            _gameScreen.AddBaseObj(_building);
            _shopOpen = false;
        }

        Rectangle building5Rect = new Rectangle(shopRect.X + 400, shopRect.Y + 300, 300, 200);
        DrawRectangleRec(building5Rect, new Color(150, 150, 150, 200));
        Util.ScaledDrawTexture(RunTime.Tower, building5Rect.X, building5Rect.Y, 130);

        Rectangle name5Rect = new Rectangle(building5Rect.X + 130, building5Rect.Y + 80 - 30, 150 , 30 );
        DrawRectangleRec(name5Rect, new Color(255, 204, 106, 0));
        Util.UpdateText(name5Rect, "TOWER", (int) building5Rect.X + 150, (int)building5Rect.Y + 80, 25, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.ScaledDrawTexture(RunTime.Wood, building5Rect.X + 155, building5Rect.Y + 115, 50);
        Rectangle wood5NumRect = new Rectangle(building5Rect.X + 210, building5Rect.Y + 110, 50 , 35 );
        DrawRectangleRec(wood5NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(wood5NumRect, "150", (int) building5Rect.X + 210, (int)building5Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE); 

        Util.ScaledDrawTexture(RunTime.Stone, building5Rect.X + 150, building5Rect.Y + 150, 50);
        Rectangle stone5NumRect = new Rectangle(building5Rect.X + 210, building5Rect.Y + 150, 50 , 35 );
        DrawRectangleRec(stone5NumRect, new Color(255, 204, 106, 0));
        Util.UpdateText(stone5NumRect, "250", (int) building5Rect.X + 210, (int)building5Rect.Y + 110, 25, (int) TextAlign.TEXT_ALIGN_RIGHT, (int) TextAlign.TEXT_ALIGN_MIDDLE); 

        if(GetMousePosition().X > building5Rect.X && GetMousePosition().X < building5Rect.X + building5Rect.Width &&  GetMousePosition().Y > building5Rect.Y && GetMousePosition().Y < building5Rect.Y + building5Rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _building = new WatchTower("Tower", X + 40, Y, 110, 120, RunTime.Tower, 2, 15, 15, 7);
            _gameScreen.AddBaseObj(_building);  
            _shopOpen = false;
        }
    }
}