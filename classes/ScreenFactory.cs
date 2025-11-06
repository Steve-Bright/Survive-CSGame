namespace Game;

using Raylib_cs;
using static Raylib_cs.Raylib;

class ScreenFactory
{
    public void RunScreen(Type CurrentScreen)
    {

        if(CurrentScreen == typeof(MenuScreen))
        {
            MenuScreen menu = new MenuScreen();
            menu.Display();
        }else if(RunTime.CurrentWindow == typeof(GameScreen))
        {
            GameScreen mainGame = new GameScreen();
            mainGame.Display();
        }else
        {
            DrawText("Nothing at the moment", 0, 0, 10, Color.Black);
        }
    }
}