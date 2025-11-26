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
        InitAudioDevice();
        SetWindowState(ConfigFlags.ResizableWindow);
        SetWindowState(ConfigFlags.MaximizedWindow);
        SetTraceLogLevel(TraceLogLevel.All);
        SetTargetFPS(60);

        List<Texture2D> allTextures = new List<Texture2D>();
        List<Sound> allSounds = new List<Sound>();
        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        allTextures = LoadAsssets();
        allSounds = LoadSounds();
        
        //mainWindow initialization
        Calendar mainCalendar = new Calendar();
        RunTime.currentCalendar = mainCalendar;
        MenuScreen menu = new MenuScreen(RunTime.MenuBg);
        GameScreen mainGame = new GameScreen(RunTime.GamescreenBg);
        RunTime.gameScreen = mainGame;
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
        CloseWindow();

    }

    public static void CreateInitialEntites(GameScreen gameScreen){
        Inventory foodInventory = new Inventory(ResourceType.FOOD, 10);
        Inventory woodInventory = new Inventory(ResourceType.WOOD, 1000);
        Inventory stoneInventory = new Inventory(ResourceType.STONE, 1000);
        Inventory rawMeatInventory = new Inventory(ResourceType.MEAT, 10);
        gameScreen.AddInventory(foodInventory);
        gameScreen.AddInventory(woodInventory);
        gameScreen.AddInventory(stoneInventory);
        gameScreen.AddInventory(rawMeatInventory);

        int[] xs = { 500, 680, 1040, 1220 };
        int[] ys = { 200, 340, 620, 760 };
        int landW = 180, landH = 180;

        for (int r = 0; r < ys.Length; r++)
        {
            for (int c = 0; c < xs.Length; c++)
            {
                int id = r * xs.Length + c + 1;
                string name = $"Area {id}";
                Land land = new Land(name, xs[c], ys[r], landW, landH, RunTime.Land, gameScreen);
                gameScreen.AddBaseObj(land);
            }
        }

        Random rng = new Random();
        int entityWidth = 55; 
        int entityHeight = 55;
        string[] names = new[]
        {
            "Alice","Bob","Carol","Dave","Eve","Frank","Grace","Heidi","Ivan",
            "Judy","Mallory","Niaj","Olivia","Peggy","Rupert","Sybil","Trent","Uma","Victor", "Zac"
        };

        foreach (var name in names)
        {
            int x = rng.Next(0, GetScreenWidth() - entityWidth);
            int y = rng.Next(0, GetScreenHeight() - entityHeight);
            Person p = new Person(name, x, y, entityWidth, entityHeight, 100, RunTime.PersonDown, RunTime.currentCalendar, gameScreen.Inventories);
            gameScreen.AddBaseObj(p);
        }

        
        ResourceArea forest = new ResourceArea("Forest 1",  0, 150, 200, 200, RunTime.Forest, ResourceType.WOOD);
        ResourceArea animalArea  = new ResourceArea("Animal Area",  50, 800, 200, 200, RunTime.AnimalArea, ResourceType.MEAT);
        ResourceArea stoneArea = new ResourceArea("Stone Area 1 ",  50, forest.Width + 300, 150, 150, RunTime.StoneArea, ResourceType.STONE);
        ResourceArea stoneArea2 = new ResourceArea("Stone Area 2 ",  GetScreenWidth()-200, 200, 150, 150, RunTime.StoneArea, ResourceType.STONE);
        ResourceArea forest2 = new ResourceArea("Forest 2", GetScreenWidth()-200, GetScreenHeight()-400, 200, 200, RunTime.Forest, ResourceType.WOOD);
        gameScreen.AddBaseObj(forest);
        gameScreen.AddBaseObj(animalArea);
        gameScreen.AddBaseObj(stoneArea);
        gameScreen.AddBaseObj(stoneArea2);
        gameScreen.AddBaseObj(forest2);
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
        Texture2D cannonIcon = LoadTexture("./resources/assets/cannon2.png");
        Texture2D towerIcon = LoadTexture("./resources/assets/tower2.png");
        Texture2D clinicIcon = LoadTexture("./resources/assets/clinic.png");
        Texture2D cookeryIcon  = LoadTexture("./resources/assets/cookery.png");
        Texture2D land  = LoadTexture("./resources/assets/land.png");
        Texture2D wallV  = LoadTexture("./resources/assets/wallV.png");
        Texture2D wallH  = LoadTexture("./resources/assets/wallH.png");
        Texture2D closeIcon = LoadTexture("./resources/assets/close.png");
        Texture2D meatIcon = LoadTexture("./resources/assets/meat.png");
        Texture2D tickIcon = LoadTexture("./resources/assets/tick.png");
        Texture2D crossIcon = LoadTexture("./resources/assets/cross.png");
        Texture2D greenTickIcon = LoadTexture("./resources/assets/greentick.png");
        // Texture2D cannonStatic = LoadTexture("./resources/assets/cannonStatic.png");

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
        allTextures.Add(closeIcon);
        allTextures.Add(meatIcon);
        allTextures.Add(tickIcon);
        allTextures.Add(crossIcon);
        allTextures.Add(greenTickIcon);
        // allTextures.Add(cannonStatic);


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
        RunTime.CloseIcon = closeIcon;
        RunTime.meatIcon = meatIcon;
        RunTime.tickIcon = tickIcon;
        RunTime.crossIcon = crossIcon;
        RunTime.greenTickIcon = greenTickIcon;
        // RunTime.cannonStatic = cannonStatic;

        return allTextures;     
    }

    public static List<Sound> LoadSounds(){
        List<Sound> allSounds = new List<Sound>();

        Sound warningSound = LoadSound("./resources/assets/warning.mp3");
        Sound clickSound = LoadSound("./resources/assets/click.mp3");
        Sound buildSound = LoadSound("./resources/assets/build.mp3");
        Sound errorSound = LoadSound("./resources/assets/error.mp3");
        Sound infoSound = LoadSound("./resources/assets/info.mp3");

        RunTime.warningSound = warningSound;
        RunTime.clickSound = clickSound;
        RunTime.buildSound = buildSound;
        RunTime.errorSound = errorSound;
        RunTime.infoSound = infoSound;

        allSounds.Add(warningSound);
        allSounds.Add(clickSound);
        allSounds.Add(buildSound);
        allSounds.Add(errorSound);
        allSounds.Add(infoSound);

        return allSounds;
    }

    public static void UnloadSounds(List<Sound> allSounds)
    {
        foreach(Sound eachSound in allSounds)
        {
            UnloadSound(eachSound);
        }
    }

    public static void UnloadAssets(List<Texture2D> allAssets)
    {
        foreach(Texture2D eachAsset in allAssets)
        {
            UnloadTexture(eachAsset);
        }
    }

}