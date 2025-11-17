using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class GameScreen : Screen
{

    public GameScreen(Texture2D background) : base(ScreenType.Game, background)
    {
        
    }

    override public void Display()
    {
        Texture2D displayBg = MainBackground;
        DrawTexture(displayBg, 0, 0, Color.White);

        Indicator indicatorOne = new Indicator(0, 0, 580, 100);
        indicatorOne.Draw();

    }
}