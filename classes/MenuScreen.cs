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

            // string menuFile = File.ReadAllText("./resources/texts/menu.json");
            // LanguageFile menuJson = JsonConvert.DeserializeObject<LanguageFile>(menuFile);
            LanguageFile menuJson = RunTime.LanFile;

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
            string menuTitle = "SURVIVE!";
            String[] menuArrays = {menuTitle, language["play"], language["quit"] };
            int moveDown = 0;
            foreach (string menuText in menuArrays)
            {
                string textName = menuText;

                MenuText text = new MenuText(menuText, screenWidth, screenHeight, fontSize);
                if (menuText != menuTitle)
                {
                    text.Clickable = true;
                    if(menuText.ToLower() == "play")
                    {
                        Action goToGameScreen = () => RunTime.CurrentWindow = typeof(GameScreen);
                        text.Method = goToGameScreen;
                    }else if(menuText.ToLower() == "quit")
                    {
                        text.Method = CloseWindow;
                    }
                }

                Vector2 textPos;
                textPos.X = (text.LengthSize > 200) ? (float)Math.Round((screenWidth - text.LengthSize) / 2.0f) : (float)Math.Round((screenWidth - text.LengthSize - 50) / 2.0f);
                textPos.Y = (float)Math.Round((screenHeight / 4.0f) + moveDown);
                text.PlaceText(textPos, Color.White);
                text.HoverNClick(GetMousePosition(), Color.Black);

                moveDown = moveDown + (int)text.HeightSize;
            }

            // MenuText langaugeOne = new MenuText("en", screenWidth, screenHeight, fontSize);
            // MenuText langaugeTwo = new MenuText("ms", screenWidth, screenHeight, fontSize);

            // Vector2 enPos;
            // enPos.X = (screenWidth - langaugeOne.LengthSize - 150) / 2.0f;
            // enPos.Y = screenHeight - (screenHeight / 7.0f);

            // Vector2 msPos = enPos;
            // msPos.X = msPos.X + langaugeOne.LengthSize;

            // langaugeOne.PlaceText(enPos, Color.White);
            // langaugeTwo.PlaceText(msPos, Color.White);

            // langaugeOne.HoverNClick(GetMousePosition(), Color.Black);
            // langaugeTwo.HoverNClick(GetMousePosition(), Color.Black);
            Shown = true;
        }
    }
    
}