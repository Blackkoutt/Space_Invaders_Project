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
        private float speed;
        private TimeSpan duration; 
        private DateTime startTime;


        public Player_Bonus(Point position)
        {
            speed =5;
            this.position=position;
            skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/star.png")) };
            model = new Rectangle { Tag = "bonus", Fill = this.skin, Height = 50, Width = 50, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
            duration = new TimeSpan(50000000);

        }

        public abstract void admitBonus(Player player);

        public abstract void EndBonus(Player player);
        public abstract string Name ();

         public bool IsActive()
        {
            TimeSpan diffrence =DateTime.Now -startTime;
            if(diffrence.TotalSeconds > duration.TotalSeconds)
                return true;
            else
                return false;
        }

        public void setPosition()
        {
            position = new Point(position.X +speed, position.Y);
            hitbox.X = position.X;
        }  
        public void setTime()
        {
            startTime = DateTime.Now;
        }  

        public DateTime StartTime
        {
            get{return this.startTime;}
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
