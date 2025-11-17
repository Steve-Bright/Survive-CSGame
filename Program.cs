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
        //backgroound asset initialization
        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        Texture2D menuBg = LoadTexture("./resources/assets/menubg.png");
        Texture2D gameScreenBg = LoadTexture("./resources/assets/gameplaybg.jpg");

        allTextures.Add(menuBg);
        allTextures.Add(gameScreenBg);
        
        //mainWindow initialization
        int totalScreens = 2;
        MenuScreen menu = new MenuScreen(menuBg);
        GameScreen mainGame = new GameScreen(gameScreenBg);
        
        ScreenFactory screen = new ScreenFactory();
        screen.AddScreen(menu);
        screen.AddScreen(mainGame);

        RunTime.CurrentWindow = ScreenType.Menu;
        Screen currentScreen;

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

    public static void UnloadAssets(List<Texture2D> allAssets)
    {
        foreach(Texture2D eachAsset in allAssets)
        {
            UnloadTexture(eachAsset);
        }
    }

}