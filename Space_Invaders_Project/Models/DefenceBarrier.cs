using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class DefenceBarrier
    {
        private Point position;
        private int health; // wartości 0,1,2,3, gdzie 0 to brak bariery
        private ImageBrush skin;
        private Rectangle model;
        private Rect hitbox;

        public DefenceBarrier()
        {
            health = 2;
            MapHealthToModel();
        }


        // Metoda ustawiająca pozycję bariery
        public void setPosition(int x, int y)
        {
            position = new Point(x,y);
            hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
        }
        public void setHealth()
        {
            health-=1;
        }

        public bool isDead()
        {
            if(health==0)
                return true;
            return false;
        }


        // Metoda mapująca HP bariery na model
        public void MapHealthToModel()
        {
            switch (health)
            {
                case 1:
                    {
                        skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_red.png")) };
                        model = new Rectangle { Tag = "barrier", Fill = this.skin, Height = 35, Width = 150, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        break;
                    }
                case 2:
                    {
                        skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_orange.png")) };
                        model = new Rectangle { Tag = "barrier", Fill = this.skin, Height = 35, Width = 150, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        break;
                    }
                case 3:
                    {
                        skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_green.png")) };
                        model = new Rectangle { Tag = "barrier", Fill = this.skin, Height = 35, Width = 150, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }


        // Gettery i settery
        public int Health
        {
            get {return health;}
        }
        public Rect Hitbox 
        { 
            get { return hitbox; }
        }
        public Rectangle Model 
        { 
            get { return model; }
        }
        public Point Position 
        { 
            get { return position; }
        }
        public ImageBrush Skin 
        { 
            get { return skin; }
        }       
    }
}
