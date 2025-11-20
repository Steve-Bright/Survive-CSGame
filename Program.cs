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
        Calendar mainCalendar = new Calendar();
        RunTime.currentCalendar = mainCalendar;
        MenuScreen menu = new MenuScreen(RunTime.MenuBg);
        GameScreen mainGame = new GameScreen(RunTime.GamescreenBg);
        CreateInitialEntites(mainGame);

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
        UnloadImage(gameIcon);
        UnloadAssets(allTextures);
        CloseWindow();


    }

    public static void CreateInitialEntites(GameScreen gameScreen){
        Random rng = new Random();
        int entityWidth = 55; 
        int entityHeight = 55;
        string[] names = new[]
        {
            "Alice","Bob","Carol","Dave","Eve","Frank","Grace","Heidi","Ivan",
            "Judy","Mallory","Niaj","Olivia","Peggy","Rupert","Sybil","Trent","Uma","Victor"
        };

        foreach (var name in names)
        {
            int x = rng.Next(0, GetScreenWidth() - entityWidth);
            int y = rng.Next(0, GetScreenHeight() - entityHeight);
            Person p = new Person(name, x, y, entityWidth, entityHeight, 100, 2, RunTime.PersonDown, RunTime.currentCalendar);
            gameScreen.createEntity(p);
        }


        //block one
        Land landOne = new Land("Area 1", 500, 200, 180, 180, RunTime.Land);
        Land landTwo = new Land("Area 2", 680, 200, 180, 180, RunTime.Land);
        Land landThree = new Land("Area 3", 500, 340, 180, 180, RunTime.Land);
        Land landFour = new Land("Area 4", 680, 340, 180, 180, RunTime.Land);

        //block two
        Land landFive = new Land("Area 5", 1040, 200, 180, 180, RunTime.Land);
        Land landSix = new Land("Area 6", 1220, 200, 180, 180, RunTime.Land);
        Land landSeven = new Land("Area 7", 1040, 340, 180, 180, RunTime.Land);
        Land landEight = new Land("Area 8", 1220, 340, 180, 180, RunTime.Land);

        // block three (under block one)
        Land landNine = new Land("Area 9", 500, 620, 180, 180, RunTime.Land); //620
        Land landTen = new Land("Area 10", 680, 620, 180, 180, RunTime.Land);
        Land landEleven = new Land("Area 11", 500, 760, 180, 180, RunTime.Land);
        Land landTwelve = new Land("Area 12", 680, 760, 180, 180, RunTime.Land);

        // block four (under block two)
        Land landThirteen = new Land("Area 13", 1040, 620, 180, 180, RunTime.Land);
        Land landFourteen = new Land("Area 14", 1220, 620, 180, 180, RunTime.Land);
        Land landFifteen = new Land("Area 15", 1040, 760, 180, 180, RunTime.Land);
        Land landSixteen = new Land("Area 16", 1220, 760, 180, 180, RunTime.Land);

        gameScreen.createLand(landOne);
        gameScreen.createLand(landTwo);
        gameScreen.createLand(landThree);
        gameScreen.createLand(landFour);
        gameScreen.createLand(landFive);
        gameScreen.createLand(landSix);
        gameScreen.createLand(landSeven);
        gameScreen.createLand(landEight);
        gameScreen.createLand(landNine);
        gameScreen.createLand(landTen);
        gameScreen.createLand(landEleven);
        gameScreen.createLand(landTwelve);
        gameScreen.createLand(landThirteen);
        gameScreen.createLand(landFourteen);
        gameScreen.createLand(landFifteen);
        gameScreen.createLand(landSixteen);
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