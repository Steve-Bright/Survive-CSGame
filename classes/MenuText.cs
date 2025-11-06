using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game
{
    public class MenuText
    {
        private string text;
        private bool clickable;
        private float lengthSize;
        private float heightSize;
        private float x;
        private float y;
        private int pxSize;
        private float spacing;

        private int screenWidth;
        private int screenHeight;

        private Action method = () => Console.WriteLine("button clicked ");

        public MenuText(string menuString, int screenW, int screenH, int fontSize, float txtSpacing = 1.0f)
        {
            text = menuString;
            clickable = false;
            screenWidth = screenW;
            screenHeight = screenH;
            pxSize = fontSize;
            Vector2 textSize = MeasureTextEx(GetFontDefault(), text, pxSize, txtSpacing);
            lengthSize = textSize.X;
            heightSize = textSize.Y;
            spacing = txtSpacing;
        }

        public void PlaceText(Vector2 textPosition, Color textColor)
        {
            x = textPosition.X;
            y = textPosition.Y;
            DrawTextEx(RunTime.LoadGameFont(), text, textPosition, pxSize, spacing, textColor);
        }

        public void HoverNClick(Vector2 mousePosition, Color hoverColor)
        {
            if(clickable == true)
            {
                if (mousePosition.X > x && mousePosition.X < lengthSize + x )
                {
                    if(mousePosition.Y > y && mousePosition.Y < heightSize + y)
                    {
                        SetMouseCursor(MouseCursor.PointingHand);
                        Vector2 position;
                        position.X = x;
                        position.Y = y;
                        DrawTextEx(RunTime.LoadGameFont(), text, position, pxSize, spacing, hoverColor);
                        if (IsMouseButtonPressed(MouseButton.Left))
                        {
                            method.Invoke();
                        }

                    }
                }                   
            }         
        }

        public bool Clickable
        {
            set { clickable = value; }
        }

        public float LengthSize
        {
            get { return lengthSize; }
        }

        public float HeightSize
        {
            get { return heightSize; }
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

        public float Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }

        public Action Method
        {
            set{ method = value; }
        }
    }
}