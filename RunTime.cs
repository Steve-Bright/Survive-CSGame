using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public static class RunTime
    {
        public static Texture2D MainGameBg;

        //variable မသုံးတာက property ဆိုရင် အလွယ်တကူ set get လုပ်လို့တယ်။ 
        public static ScreenType CurrentWindow{
            get; set;
        }
    }
}
