using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;

namespace Game
{
    public class MenuScreen:Screen
    {

        // static string menuFile = File.ReadAllText("../resources/texts/menu.json");
        // LanguageFile menuJson = JsonConvert.DeserializeObject<LanguageFile>(menuFile);
        override public void Display()
        {
            int screenWidth = GetScreenWidth();
            int screenHeight = GetScreenHeight();

            Texture2D menuBg = LoadTexture("./resources/assets/menubg.png");

            for (int y = 0; y < screenHeight; y += menuBg.Height)
            {
                for (int x = 0; x < screenWidth; x += menuBg.Width)
                {
                    DrawTexture(menuBg, x, y, Color.White);
                }
            }
            
            
        }
    }
}