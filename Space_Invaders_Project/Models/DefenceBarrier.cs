﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class DefenceBarrier
    {
        private Point position;
        private int health; // wartości 0,1,2,3, gdzie 0 to brak bariery
        private ImageBrush model;

        public DefenceBarrier()
        {
            health = 3;
            MapHealthToModel();
        }


        // Metoda ustawiająca pozycję bariery
        public void setPosition(Point position)
        {
            this.position = position;
        }


        // Metoda mapująca HP bariery na model
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

                        break;
                    }
            }
        }


        // Gettery i settery
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
