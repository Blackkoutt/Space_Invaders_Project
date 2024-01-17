using Space_Invaders_Project.Models.Interfaces;
using System.Drawing;

namespace Space_Invaders_Project.Models
{
    public class Enemy_Missle : IMissle
    {
        private Point position;
        private int speed;
        private int damage;

        public Enemy_Missle(Point position, int damage, int speed)
        {
            this.position = position;
            this.speed = speed;
            this.damage = damage;
        }

        public Point Position
        {
            get { return this.position; }
        }

        public int Damage
        {
            get { return  damage; }
        }

        // Nie pamiętam jak to dokładnie miało działać ;/
        public void move()
        {
            position.Y += speed;
        }
    }
}
