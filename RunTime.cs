using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public static class RunTime
    {
        static private string gameFont;

        //variable မသုံးတာက property ဆိုရင် အလွယ်တကူ set get လုပ်လို့တယ်။ 
        public static string Language { get; set; }
        public static string GameFont
        {
            get { return gameFont; }
            set { gameFont = value;  }
        }

        public static Font LoadGameFont()
        {
            Font actualFont = LoadFont(gameFont);
            return actualFont;
        }
    }
}
