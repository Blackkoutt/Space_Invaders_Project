using Space_Invaders_Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Label label = new Label()
            {
                Tag = "notification",
                Content = String.Format("Pokonano gracza {0}, z wynikiem {1}", HighScores.nicks[position], HighScores.scores[position]),
                Background = Brushes.DarkGray,
                Opacity = 0.4
            };
            Canvas.SetTop(label, 10);
            Canvas.SetLeft(label, 10);
            MainWindow.MainCanvas.Children.Add(label);
        }
    }
}
