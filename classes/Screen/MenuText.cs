using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game
{
    public class MenuText
    {
        private string text;
        private bool clickable;
        private int x;
        private int y;
        private int pxSize;

        private Action method = () => Console.WriteLine("button clicked ");

        public MenuText(string menuString,  int fontSize)
        {
            text = menuString;
            clickable = false;
            pxSize = fontSize;
        }

        public void Draw(int posx, int posy, Color color)
        {
            x = posx;
            y = posy;
            DrawText(text, x, y, pxSize, color);
        }

        public void HoverNClick(Vector2 mousePosition, Color hoverColor)
        {
            if(clickable == true)
            {
                bool xCondition =  mousePosition.X > x && mousePosition.X < MeasureText(text, pxSize) + x ;
                bool yCondition = mousePosition.Y > y && mousePosition.Y < y/3 + y;
                
                    if(xCondition && yCondition)
                    {

                        DrawText(text, x, y, pxSize, hoverColor);
                        if (IsMouseButtonPressed(MouseButton.Left))
                        {
                            method.Invoke();
                        }

                    }
                       
            }         
        }

        public bool Clickable
        {
            set { clickable = value; }
        }


        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public int PxSize
        {
            get { return pxSize; }
            set { pxSize = value; }
        }

        public Action Method
        {
            set{ method = value; }
        }
    }
}