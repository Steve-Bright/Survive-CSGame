using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;

namespace Game
{
    public class MenuScreen : Screen
    {
        int screenWidth = GetScreenWidth();
        int screenHeight = GetScreenHeight();

        public MenuScreen(Texture2D background) : base(ScreenType.Menu, background)
        {
            
        }

        override public void Display()
        {
            Texture2D menuBg = MainBackground;

            for (int y = 0; y < screenHeight; y += menuBg.Height)
            {
                for (int x = 0; x < screenWidth; x += menuBg.Width)
                {
                    DrawTexture(menuBg, x, y, Color.White);
                }
            }

            int fontSize = 80;
            string menuTitle = "SURVIVE!";
            String[] menuArrays = {"SURVIVE!", "Play", "Quit" };
            int moveDown = 0;
            foreach (string menuText in menuArrays)
            {
                string textName = menuText;

                MenuText text = new MenuText(menuText, fontSize);
                if (menuText != menuTitle)
                {
                    text.Clickable = true;
                    if (menuText.ToLower() == "play")
                    {
                        Action goToGameScreen = () => RunTime.CurrentWindow = ScreenType.Game;
                        text.Method = goToGameScreen;
                    }
                    else if (menuText.ToLower() == "quit")
                    {
                        text.Method = CloseWindow;
                    }
                }

                // Vector2 textPos;
                // textPos.X = (text.LengthSize > 200) ? (float)Math.Round((screenWidth - text.LengthSize) / 2.0f) : (float)Math.Round((screenWidth - text.LengthSize - 50) / 2.0f);
                // textPos.Y = (float)Math.Round((screenHeight / 4.0f) + moveDown);
                
                int textWidth = MeasureText(menuText, fontSize);
                int textPosX = (screenWidth - textWidth) / 2;
                int textPosY = screenHeight/4 + moveDown;
                text.Draw(textPosX, textPosY, Color.White);
                
                // text.PlaceText(textPos, Color.White);
                text.HoverNClick(GetMousePosition(), Color.Black);

                moveDown = moveDown + (textPosY / 3);
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