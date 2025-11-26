using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class Land : BaseObj
{
    private GameScreen _gameScreen;
    private bool _shopOpen = false;
    private Building? _building; 
    
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
            _building?.DisplayDetails();
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
        Inventory stoneInv = _gameScreen.GetInventory(ResourceType.STONE);
        Inventory woodInv = _gameScreen.GetInventory(ResourceType.WOOD);
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
            // _gameScreen.AddBaseObj(_building)
            _shopOpen = false;
        }

        // Replace manual building blocks with a loop-driven layout
        string[] labels = new string[] { "HUT", "CLINIC", "KITCHEN", "CANNON", "TOWER" };
        Texture2D[] icons = new Texture2D[] { RunTime.Hut, RunTime.Clinic, RunTime.Cookery, RunTime.Cannon, RunTime.Tower };
        int[] woodCosts = new int[] { 50, 150, 120, 0, 150 };
        int[] stoneCosts = new int[] { 30, 100, 0, 150, 250 };

        int cols = 3;
        int optionW = 300;
        int optionH = 200;
        int xPad = 50;
        int yPad = 80;
        int xSpacing = 350;
        int ySpacing = 220;

        for (int i = 0; i < labels.Length; i++)
        {
            int col = i % cols;
            int row = i / cols;
            float bx = shopRect.X + xPad + col * xSpacing;
            float by = shopRect.Y + yPad + row * ySpacing;

            Rectangle optRect = new Rectangle(bx, by, optionW, optionH);
            DrawRectangleRec(optRect, new Color(150, 150, 150, 200));
            Util.ScaledDrawTexture(icons[i], optRect.X, optRect.Y, 150);

            Rectangle optNameRect = new Rectangle(optRect.X + 150, optRect.Y + 80 - 30, 150, 30);
            DrawRectangleRec(optNameRect, new Color(255, 204, 106, 0));
            Util.UpdateText(optNameRect, labels[i], (int)optRect.X + 150, (int)optRect.Y + 80, 25, (int)TextAlign.TEXT_ALIGN_CENTRE, (int)TextAlign.TEXT_ALIGN_MIDDLE);

            int wCost = woodCosts[i];
            int sCost = stoneCosts[i];
            int iconX = (int)optRect.X + 165;
            int iconWoodY = (int)optRect.Y + 115;
            int iconStoneY = (int)optRect.Y + 150;

            if (wCost > 0)
            {
                Util.ScaledDrawTexture(RunTime.Wood, iconX, iconWoodY, 50);
                Rectangle woodNumRect = new Rectangle(optRect.X + 210, optRect.Y + 110, 50, 35);
                DrawRectangleRec(woodNumRect, new Color(255, 204, 106, 0));
                Util.UpdateText(woodNumRect, wCost.ToString(), (int)optRect.X + 210, (int)optRect.Y + 110, 25, (int)TextAlign.TEXT_ALIGN_RIGHT, (int)TextAlign.TEXT_ALIGN_MIDDLE);
            }

            if (sCost > 0)
            {
                Util.ScaledDrawTexture(RunTime.Stone, iconX, iconStoneY, 50);
                Rectangle stoneNumRect = new Rectangle(optRect.X + 210, optRect.Y + 150, 50, 35);
                DrawRectangleRec(stoneNumRect, new Color(255, 204, 106, 0));
                Util.UpdateText(stoneNumRect, sCost.ToString(), (int)optRect.X + 210, (int)optRect.Y + 145, 25, (int)TextAlign.TEXT_ALIGN_RIGHT, (int)TextAlign.TEXT_ALIGN_MIDDLE);
            }

            // Click handling
            if (GetMousePosition().X > optRect.X && GetMousePosition().X < optRect.X + optRect.Width && GetMousePosition().Y > optRect.Y && GetMousePosition().Y < optRect.Y + optRect.Height && IsMouseButtonPressed(MouseButton.Left))
            {
                // check resources
                if (woodInv.TotalNum < wCost || stoneInv.TotalNum < sCost)
                {
                    _gameScreen.AddMessage($"Not enough resources to build {labels[i]}!!!", AlertType.ERROR);
                }
                else
                {
                    // create building instances consistent with previous behavior
                    switch (labels[i])
                    {
                        case "HUT":
                            _building = new Hut("Hut", X + 100 / 2, Y, 100, 100, RunTime.Hut);
                            break;
                        case "CLINIC":
                            _building = new Clinic("Clinic", X + 100 / 2 - 15, Y, 120, 120, RunTime.Clinic);
                            break;
                        case "KITCHEN":
                            _building = new Kitchen("Kitchen", X + 100 / 2 - 10, Y, 120, 120, RunTime.Cookery);
                            break;
                        case "CANNON":
                            _building = new Cannon("Cannon", X + 40, Y, 100, 100, RunTime.Cannon, 1, 10, 10, 5);
                            break;
                        case "TOWER":
                            _building = new WatchTower("Tower", X + 50, Y, 100, 100, RunTime.Tower, 1, 15, 10, 5);
                            break;
                    }

                    if (_building != null)
                    {
                        _gameScreen.Build(_building);
                        _gameScreen.AddMessage($"{labels[i]} built successfully!", AlertType.INFO);
                    }
                }

                _shopOpen = false;
            }
        }
    }
}