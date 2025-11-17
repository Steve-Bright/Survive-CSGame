using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class GameScreen() : Screen()
{
    Texture2D _gameBg;

    override public void Display()
    {

        _gameBg = LoadTexture("./resources/assets/gameplaybg.jpg");
        DrawTexture(_gameBg, 0, 0, Color.White);

        Indicator indicatorOne = new Indicator(0, 0, 580, 100);
        indicatorOne.Draw();

    }

    public override void Unload()
    {
        if(_gameBg.Id != 0)
        {
            UnloadTexture(_gameBg);
        }
    }
}