using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;

namespace Game
{
    public class MenuScreen():Screen()
    {

        override public void Display()
        {
            int screenWidth = GetScreenWidth();
            int screenHeight = GetScreenHeight();
                
            string menuFile = File.ReadAllText("./resources/texts/menu.json");
            LanguageFile menuJson = JsonConvert.DeserializeObject<LanguageFile>(menuFile);

            Texture2D menuBg = LoadTexture("./resources/assets/menubg.png");

            for (int y = 0; y < screenHeight; y += menuBg.Height)
            {
                for (int x = 0; x < screenWidth; x += menuBg.Width)
                {
                    DrawTexture(menuBg, x, y, Color.White);
                }
            }
            Dictionary<string, string> language = (RunTime.Language == "english") ? language = menuJson.en : language = menuJson.ms;

            int fontSize = 80;
            String[] menuArrays = { "SURVIVE!", language["play"], language["quit"] };
            int moveDown = 0;
            foreach(string menuText in menuArrays){
                string textName = menuText;

                Vector2 textSize = MeasureTextEx(GetFontDefault(), textName, fontSize, 1.0f);
                Vector2 textPos;
                textPos.X = (textSize.X > 200) ? (screenWidth - textSize.X) / 2.0f : (screenWidth - textSize.X - 50 ) / 2.0f;
                textPos.Y = (screenHeight / 4.0f) + moveDown;
                DrawTextEx(RunTime.LoadGameFont(), textName, textPos, fontSize, 2.0f, Color.White);
                moveDown = moveDown + (int) textSize.Y;
            }

            string english = "en";
            string malaysian = "ms";
            Vector2 lanTextSize = MeasureTextEx(GetFontDefault(), english, fontSize, 1.0f);

            Vector2 enPos;
            enPos.X = (screenWidth - lanTextSize.X - 150) / 2.0f;
            enPos.Y = screenHeight - (screenHeight / 7.0f);

            Vector2 msPos = enPos;
            msPos.X = msPos.X + lanTextSize.X; 

            DrawTextEx(RunTime.LoadGameFont(), english, enPos, fontSize - 10, 2.0f, Color.White);
            DrawTextEx(RunTime.LoadGameFont(), malaysian, msPos, fontSize-10, 2.0f, Color.White);
            Shown = true;
        }
    }
}