using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class Barrier
    {
        private Point position;
        private int health; // wartości 0,1,2,3, gdzie 0 to brak bariery
        private ImageBrush model;

        public Barrier(Point position)
        {
            this.position = position;
            health = 3;
            MapHealthToModel();
        }
        public void MapHealthToModel()
        {
            switch (health)
            {
                case 1:
                    {
                        model = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_red.png")) };
                        break;
                    }
                case 2:
                    {
                        model = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_orange.png")) };
                        break;
                    }
                case 3:
                    {
                        model = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/barrier_green.png")) };
                        break;
                    }
                default:
                    {
                        // wtedy trzeba usunąć bariere z listy i usunać ją z widoku
                        // można w sumie dać np czarnyImageBrush o takich samyych wymiarach ale to się zobaczy
                        break;
                    }
            }
        }
        public int Health
        {
            get {return health;}
        }
        public Point Position
        {
            get { return position; }
        }
    }
}
