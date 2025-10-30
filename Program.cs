using Raylib_cs;
using static Raylib_cs.Raylib;
using System.IO;

namespace Game;

internal static class Program
{
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Dictionary<string, string> settings = Util.returnJsonFile("./resources/saved/settings.json");
        RunTime.Language = settings["language"];

        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        RunTime.GameFont = "./resources/assets/englishfont.ttf";

        InitWindow(800, 480, "Survive!");
        SetWindowIcon(gameIcon);
        SetWindowState(ConfigFlags.ResizableWindow);
        SetWindowState(ConfigFlags.MaximizedWindow);
        

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