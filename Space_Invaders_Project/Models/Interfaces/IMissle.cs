using System.Drawing;

namespace Space_Invaders_Project.Models.Interfaces
{
    public interface IMissle
    {
        public Point Position { get; }
        void move();
        public int Damage { get; }
    }
}
