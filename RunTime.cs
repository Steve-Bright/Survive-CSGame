using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public static class RunTime
    {
        public static Texture2D MainGameBg;
        public static Texture2D PersonUp;
        public static Texture2D PersonDown;
        public static Texture2D PersonRight;
        public static Texture2D PersonLeft;
        public static Texture2D Sun;
        public static Texture2D Food;
        public static Texture2D Wood;
        public static Texture2D Stone;
        public static Texture2D Forest;
        public static Texture2D StoneArea;
        public static Texture2D AnimalArea;
        public static Texture2D Hut;
        public static Texture2D Cannon;
        public static Texture2D Tower;
        public static Texture2D Clinic;
        public static Texture2D Cookery;
        public static Texture2D Land;
        public static Texture2D WallV;
        public static Texture2D WallH;

        public static Texture2D MenuBg;
        public static Texture2D GamescreenBg;
        public static Calendar currentCalendar;

        //variable မသုံးတာက property ဆိုရင် အလွယ်တကူ set get လုပ်လို့တယ်။ 
        public static ScreenType CurrentWindow{
            get; set;
        }
    }
}
