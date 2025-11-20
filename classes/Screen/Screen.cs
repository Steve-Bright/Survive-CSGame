using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public abstract class Screen
    {
        private ScreenType _typename;
        private Texture2D _mainBackground;
        abstract public void Display();
        public bool Shown
        {
            get; set;
        }

        public Screen(ScreenType screenTypeValue, Texture2D bg)
        {
            _typename = screenTypeValue;
            _mainBackground = bg;
        }

        public ScreenType TypeName
        {
            get{return _typename;}
        }

        public Texture2D MainBackground
        {
            get {return _mainBackground;}
        }
        
        public void ClearScreen()
        {
            RunTime.CurrentWindow = ScreenType.None;
        }

    }
}