namespace Game;

using Raylib_cs;
using static Raylib_cs.Raylib;

class ScreenFactory
{
    List<Screen> windows;

    public ScreenFactory()
    {
        windows = new List<Screen>();
    }

    public void AddScreen(Screen oneScreen)
    {
        windows.Add(oneScreen);
    }

    public void RunScreen(ScreenType currentScreen)
    {   
        foreach(Screen eachScreen in windows)
        {
            if(eachScreen.TypeName == currentScreen)
            {
                eachScreen.Display();
            }
        }
    }

    // public void UnloadScreen(Type CurrentScreen)
    // {
    //     if (CurrentScreen == typeof(MenuScreen))
    //     {
    //         MenuScreen menu = new MenuScreen();
    //         menu.Unload();
    //     }
    //     // else if (RunTime.CurrentWindow == typeof(GameScreen))
    //     else
    //     {
    //         GameScreen mainGame = new GameScreen();
    //         mainGame.Unload();
    //     }
    // }
}