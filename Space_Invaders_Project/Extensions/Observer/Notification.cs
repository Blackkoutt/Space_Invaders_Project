using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
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
        private IMapView _mapView;

        public Notification(IMapView mapView) 
        {
            _mapView = mapView;
        }

        public void Update(string name, int score,int position)
        {
            Label label = new Label()
            {
                Tag = "notification",
                Content = String.Format("Pokonano gracza {0}, z wynikiem {1}", HighScores.Nicks[position], HighScores.Scores[position]),
                Background = Brushes.DarkGray,
                Opacity = 0.4
            };
            _mapView.AddNotification(label);
        }
    }
}