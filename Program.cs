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

        InitWindow(800, 480, "Survive!");
        SetWindowIcon(gameIcon);
        SetWindowState(ConfigFlags.ResizableWindow);
        SetWindowState(ConfigFlags.MaximizedWindow);
        SetTraceLogLevel(TraceLogLevel.None);
        SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            MenuScreen menu = new MenuScreen();
            menu.Display();

            // Vector2 testPos;
            // testPos.X = 100;
            // testPos.Y = 100;
            // DrawTextEx(englishFont, "Hello", testPos, 32, 2, Color.White);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public enum Language
    {
        English, Myanmar
    }
}