
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
public class ConditionScreen : Screen
{

    public ConditionScreen(Texture2D bg) : base(ScreenType.ConditionScreen, bg)
    {
    }
    public override void Display()
    {
        DrawTexture(MainBackground, 0, 0, Color.White);

        string conditionText = RunTime.isGameOver ? "You Win!" : "Game Over!";
        Color textColor = RunTime.isGameOver ? Color.Green : Color.Red;

        DrawText(conditionText, (GetScreenWidth()  - MeasureText(conditionText, 100)) / 2, GetScreenHeight() / 2 - 40, 100, textColor);

        // Rectangle goToMenuButton = new Rectangle((GetScreenWidth() / 2) - 100, (GetScreenHeight() / 2) + 100, 200, 60);
        // DrawRectangleRec(goToMenuButton, Color.DarkGray);

    }
}