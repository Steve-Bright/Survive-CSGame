using Raylib_cs;
using static Raylib_cs.Raylib;
using System.IO;
using Newtonsoft.Json;

namespace Game;

internal static class Program
{
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        InitWindow(800, 480, "Survive!");
        SetWindowState(ConfigFlags.ResizableWindow);
        SetWindowState(ConfigFlags.MaximizedWindow);
        SetTraceLogLevel(TraceLogLevel.All);
        SetTargetFPS(60);

        List<Texture2D> allTextures = new List<Texture2D>();
        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        allTextures = LoadAsssets();
        
        //mainWindow initialization
        MenuScreen menu = new MenuScreen(RunTime.MenuBg);
        GameScreen mainGame = new GameScreen(RunTime.GamescreenBg);
        
        ScreenFactory screen = new ScreenFactory();
        screen.AddScreen(menu);
        screen.AddScreen(mainGame);

        RunTime.CurrentWindow = ScreenType.Menu;

        SetWindowIcon(gameIcon);

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.White);

            screen.RunScreen(RunTime.CurrentWindow);

            EndDrawing();
        }

        UnloadAssets(allTextures);
        CloseWindow();


    }

    public static List<Texture2D> LoadAsssets()
    {
        List<Texture2D> allTextures = new List<Texture2D>();
        //backgroound asset initialization

        Texture2D menuBg = LoadTexture("./resources/assets/menubg.png");
        Texture2D gameScreenBg = LoadTexture("./resources/assets/gameplaybg.jpg");
        Texture2D character_down = LoadTexture("./resources/assets/char_down.png");
        Texture2D character_up = LoadTexture("./resources/assets/char_up.png");
        Texture2D character_left = LoadTexture("./resources/assets/char_left.png");
        Texture2D character_right = LoadTexture("./resources/assets/char_right.png");
        Texture2D sunIcon = LoadTexture("./resources/assets/sun.png");
        Texture2D foodIcon = LoadTexture("./resources/assets/food.png");
        Texture2D woodIcon = LoadTexture("./resources/assets/wood.png");
        Texture2D stoneIcon = LoadTexture("./resources/assets/stone.png");
        Texture2D forestIcon = LoadTexture("./resources/assets/forest.png");
        Texture2D stoneAreaIcon = LoadTexture("./resources/assets/stonearea.png");
        Texture2D animalArea = LoadTexture("./resources/assets/animalarea.png");
        Texture2D hutIcon = LoadTexture("./resources/assets/hut.png");
        Texture2D cannonIcon = LoadTexture("./resources/assets/cannon.png");
        Texture2D towerIcon = LoadTexture("./resources/assets/tower.png");
        Texture2D clinicIcon = LoadTexture("./resources/assets/clinic.png");
        Texture2D cookeryIcon  = LoadTexture("./resources/assets/cookery.png");
        Texture2D land  = LoadTexture("./resources/assets/land.png");
        Texture2D wallV  = LoadTexture("./resources/assets/wallV.png");
        Texture2D wallH  = LoadTexture("./resources/assets/wallH.png");

        allTextures.Add(menuBg);
        allTextures.Add(gameScreenBg);
        allTextures.Add(character_down);
        allTextures.Add(character_up);
        allTextures.Add(character_left);
        allTextures.Add(character_right);
        allTextures.Add(sunIcon);
        allTextures.Add(foodIcon);
        allTextures.Add(woodIcon);
        allTextures.Add(stoneIcon);
        allTextures.Add(forestIcon);
        allTextures.Add(stoneAreaIcon);
        allTextures.Add(animalArea);
        allTextures.Add(hutIcon);
        allTextures.Add(cannonIcon);
        allTextures.Add(towerIcon);
        allTextures.Add(clinicIcon);
        allTextures.Add(cookeryIcon);
        allTextures.Add(land);
        allTextures.Add(wallV);
        allTextures.Add(wallH);

        RunTime.MenuBg = menuBg;
        RunTime.GamescreenBg = gameScreenBg;
        RunTime.PersonDown = character_down;
        RunTime.PersonUp = character_up;
        RunTime.PersonLeft = character_left;
        RunTime.PersonRight = character_right;
        RunTime.Sun = sunIcon;      
        RunTime.Food = foodIcon;
        RunTime.Wood = woodIcon;
        RunTime.Stone = stoneIcon;
        RunTime.Forest = forestIcon;
        RunTime.StoneArea = stoneAreaIcon;
        RunTime.AnimalArea = animalArea;
        RunTime.Hut = hutIcon;
        RunTime.Cannon = cannonIcon;
        RunTime.Tower = towerIcon;
        RunTime.Clinic = clinicIcon;
        RunTime.Cookery = cookeryIcon;
        RunTime.Land = land;
        RunTime.WallV = wallV;
        RunTime.WallH = wallH;

        return allTextures;     
    }

    public static void UnloadAssets(List<Texture2D> allAssets)
    {
        foreach(Texture2D eachAsset in allAssets)
        {
            UnloadTexture(eachAsset);
        }
    }

}