using System;
using System.ComponentModel.DataAnnotations;

namespace Game
{
    public abstract class Screen
    {
        private bool shown;
        abstract public void Display();

        public bool Shown{
            get; set;
        }

    }
}