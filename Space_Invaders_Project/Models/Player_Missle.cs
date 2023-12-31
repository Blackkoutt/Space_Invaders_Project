using Space_Invaders_Project.Models.Interfaces;
using System.Drawing;
using System.Printing;

namespace Space_Invaders_Project.Models
{
    class Player_Missle : IMissle
    {
        private Point position;
        private float speed;
        private int damage;

        public Player_Missle(Point position, float speed, int damage)
        {
            this.position = position;
            this.speed = speed;
            this.damage = damage;
        }
        public int Damage
        {
            get { return damage; }
        }
        public Point Position
        {
            get { return position; }
        }

        // Nie pamiętam dokładnie jak to miało działać ;/
        public void move()
        {
            throw new System.NotImplementedException();
        }
    }
}
