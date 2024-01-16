using Space_Invaders_Project.Models.Interfaces;
using System.Windows;
using System.Printing;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Space_Invaders_Project.Models
{
    public class Player_Missile : IMissile
    {
        private Point position;
        private float speed;
        private int damage;
        public Rectangle model;
        public Rect hitbox;

        public Player_Missile(Point position, float speed, int damage)
        {
            this.position = position;
            this.speed = speed;
            this.damage = damage;
            this.model = new Rectangle { Tag = "playerMissile", Height = 20, Width = 5, Fill = Brushes.White, Stroke = Brushes.Red };
            this.hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
        }
        public int Damage
        {
            get { return damage; }
        }
        public Point Position
        {
            get { return position; }
        }
        public float Speed
        {
            get { return speed; }
        }
        public Rectangle Model
        {
            get { return this.model; }
        }
        public void setPosition(float y)
        {
            position = new Point(position.X, position.Y-y);
            hitbox.Y = y;
        }
    }
}
