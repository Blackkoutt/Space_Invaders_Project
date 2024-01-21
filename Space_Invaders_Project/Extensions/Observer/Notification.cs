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
        private List<string> nicks;
        private List<int> scores;

        public Notification(IMapView mapView, List<string> nicks, List<int> scores) 
        {
            _mapView = mapView;
            this.nicks = nicks;
            this.scores = scores;
        }

        
        // Metoda tworząca powiadomienie o pobiciu wyniku danego gracza
        public void Update(string name, int score,int position)
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

        public void UpdateRealHighScores(ref List<string> nicks, ref List<int> scores)
        {
            
        }
    }
}