namespace Game;

using Raylib_cs;
using static Raylib_cs.Raylib;

public class ScreenFactory
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

}