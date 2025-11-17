namespace Game;

using Raylib_cs;
using static Raylib_cs.Raylib;

class ScreenFactory
{
    public Screen RunScreen(Type CurrentScreen)
    {

        if (CurrentScreen == typeof(MenuScreen))
        {
            MenuScreen menu = new MenuScreen();
            menu.Display();
            return menu;
        }
        // else if (RunTime.CurrentWindow == typeof(GameScreen))
        else
        {
            GameScreen mainGame = new GameScreen();
            mainGame.Display();
            return mainGame;
        }
        // else
        // {
        //     DrawText("Nothing at the moment", 0, 0, 10, Color.Black);
        // }
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