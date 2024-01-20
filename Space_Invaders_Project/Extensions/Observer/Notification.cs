using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class Notification : ISubscriber
    {
        private IMapView _mapView;
        private string lastNotification = "";

        public Notification(IMapView mapView) 
        {
            _mapView = mapView;
        }

        
        // Metoda tworząca powiadomienie o pobiciu wyniku danego gracza
        public void Update(string name, int score,int position, ref List<string> nicks, ref List<int> scores)
        {
            if(name != nicks[position])
            {
                string message = String.Format("Pokonano gracza {0}, z wynikiem {1}", nicks[position], scores[position]);
                if (message == lastNotification)
                    return;

                Label label = new Label()
                {
                    Tag = "notification",
                    Content = message,
                    Background = Brushes.DarkGray,
                    Opacity = 0.4
                };
                _mapView.AddNotification(label);
                lastNotification = message;
            } 
        }
    }
}