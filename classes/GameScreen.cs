using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class GameScreen() : Screen()
{

    override public void Display()
    {
        Camera2D camera = new Camera2D();
        camera.Zoom = 1.0f;

        BeginMode2D(camera);
        DrawRectangle(0, 0, 200, 200, Color.Red);
        EndMode2D();
    }
}