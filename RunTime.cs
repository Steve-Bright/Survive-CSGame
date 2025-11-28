using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public static class RunTime
    {
        public static ScreenFactory screenFactory;
        public static Texture2D MainGameBg;
        public static Texture2D conditionScreen;
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
        public static Texture2D CloseIcon;

        public static Texture2D MenuBg;
        public static Texture2D GamescreenBg;
        public static Texture2D meatIcon;
        public static Texture2D tickIcon;
        public static Texture2D crossIcon;
        public static Texture2D greenTickIcon;
        public static Texture2D snowyIcon;
        public static Texture2D stormyIcon;
        public static Texture2D zombie_down;
        public static Texture2D zombie_up;
        public static Texture2D zombie_left;
        public static Texture2D zombie_right;

        public static Texture2D zombie_downAtk;
        public static Texture2D zombie_downAtk2;
        public static Texture2D zombie_upAtk;
        public static Texture2D zombie_upAtk2;
        public static Texture2D zombie_leftAtk;
        public static Texture2D zombie_leftAtk2;
        public static Texture2D zombie_rightAtk;
        public static Texture2D zombie_rightAtk2;
        
        // public static Texture2D cannonStatic;
        public static Calendar currentCalendar;
        public static bool detailsShown = false;
        public static Sound clickSound;
        public static Sound buildSound;
        public static Sound warningSound;

        public static Sound infoSound;
        public static Sound errorSound;

        public static GameScreen gameScreen;
        public static bool isGameOver = true;

        //variable မသုံးတာက property ဆိုရင် အလွယ်တကူ set get လုပ်လို့တယ်။ 
        public static ScreenType CurrentWindow{
            get; set;
        }
    }
}
