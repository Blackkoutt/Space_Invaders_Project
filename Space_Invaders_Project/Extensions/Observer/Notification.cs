using Space_Invaders_Project.Views;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class Notification : ISubscriber
    {
        public Notification() { 

        }

        public void Update(string name, int score,int position)
        {
            TextBox textBox = new TextBox()
            {
                Tag = "notification",
                Text = String.Format("Pokonano gracza {0}, z wynikiem {1}", HighScores.nicks[position], HighScores.scores[position]),
                Background = Brushes.DarkGray,
                Opacity = 0.4
            };
            Canvas.SetTop(textBox, 10);
            Canvas.SetLeft(textBox, 10);
            MainWindow.MainCanvas.Children.Add(textBox);
            //MainWindow.MainCanvas.Children.Remove(textBox);
        }
    }
}
