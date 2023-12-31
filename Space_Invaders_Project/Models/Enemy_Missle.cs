using Space_Invaders_Project.Models.Interfaces;
using System.Drawing;

namespace Space_Invaders_Project.Models
{
    public class Enemy_Missle : IMissle
    {
        private Point position;
        private float speed;
        private int damage;

        public Enemy_Missle(Point position, float speed, int damage)
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
            throw new System.NotImplementedException();
        }
    }
}
