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
        Dictionary<string, string> settings = Util.returnJsonFile("./resources/saved/settings.json");
        RunTime.Language = settings["language"];
                
        string menuFile = File.ReadAllText("./resources/texts/menu.json");
        LanguageFile menuJson = JsonConvert.DeserializeObject<LanguageFile>(menuFile);

        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        RunTime.GameFont = "./resources/assets/englishfont.ttf";
        RunTime.LanFile = menuJson;
        RunTime.CurrentWindow = typeof(MenuScreen);

        InitWindow(800, 480, "Survive!");
        SetWindowIcon(gameIcon);
        SetWindowState(ConfigFlags.ResizableWindow);
        SetWindowState(ConfigFlags.MaximizedWindow);
        SetTraceLogLevel(TraceLogLevel.None);
        SetTargetFPS(60);

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.White);

            ScreenFactory screen = new ScreenFactory();
            screen.RunScreen(RunTime.CurrentWindow);

            EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public enum Language
    {
        English, Myanmar
    }
}