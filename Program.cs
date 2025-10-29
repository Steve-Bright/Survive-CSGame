using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;

namespace Game;

internal static class Program
{
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Dictionary<string, string> settings = Util.returnJsonFile("./resources/saved/settings.json");
        
        Image gameIcon = LoadImage("./resources/assets/bonfire.png");
        Font myanmarFont = LoadFont("./resources/assets/myanmarfont.ttf");
        Font englishFont = LoadFont("./resources/assets/englishfont.ttf");

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

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public enum Language
    {
        English, Myanmar
    }
}