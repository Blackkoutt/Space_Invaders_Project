using Space_Invaders_Project.Models;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Space_Invaders_Project.Extensions.Strategy
{
    public abstract class Player_Bonus
    {
        private Point position;
        private ImageBrush skin;
        private Rectangle model;
        private Rect hitbox;
        private float speed =5;
        private TimeSpan duration; 
        private DateTime startTime;


        public Player_Bonus(Point position)
        {
            this.position=position;
            skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/speedEnemyLeg.png")) };
            model = new Rectangle { Tag = "bonus", Fill = this.skin, Height = 50, Width = 50, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
            duration = new TimeSpan(7);
            startTime = DateTime.Now;

        }

        public abstract void admitBonus(Player player);

        public abstract void EndBonus(Player player);

        public abstract void Notify();

         public bool IsActive()
        {
            return DateTime.Now - startTime < duration;
        }

        public void setPosition()
        {
            position = new Point(position.X +speed, position.Y);
            hitbox.X = position.X;
        }       

        public Point Position
        {
            get { return this.position; }
        }
        public Rectangle Model
        {
            get { return this.model; }
        }
        public Rect Hitbox 
        {
            get { return hitbox; }
        }
    }
}
