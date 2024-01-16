using Space_Invaders_Project.Models.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models
{
    public class Enemy_Missile : IMissile
    {
        private Point position;
        private float speed;
        private int damage;
        public Rectangle model;
        public Rect hitbox;

        public Enemy_Missile(Point position, float speed, int damage)
        {
            this.position = position;
            this.speed = speed;
            this.damage = damage;
            this.model = new Rectangle { Tag = "enemyMissile", Height = 20, Width = 5, Fill = Brushes.Purple };
            this.hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
        }
        public float Speed
        {
            get { return speed; }
        }
        public Point Position
        {
            get { return this.position; }
        }
        public Rectangle Model
        {
            get { return this.model; }
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

        public void setPosition(float y)
        {
            throw new System.NotImplementedException();
        }
    }
}
