using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game
{
    public abstract class Screen
    {
        private bool shown;
        abstract public void Display();
        abstract public void Unload();
        public bool Shown
        {
            get; set;
        }
        
        public void ClearScreen()
        {
            RunTime.CurrentWindow = null;
        }

    }
}