using Space_Invaders_Project.Models;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using Space_Invaders_Project.Views.Interfaces;


namespace Space_Invaders_Project.Extensions.Strategy
{
    public class ScoreMultiplier : Player_Bonus
    {
        private IMapView _mapView;
        public ScoreMultiplier(Point position) : base(position) 
        { 

        }    
        public override void admitBonus(Player player)
        {
            player.ScoreMultiplier = 2; 
        }
        public override void EndBonus(Player player)
        {
           player.ScoreMultiplier = 1; 
        }
        public override void Notify()
        {
            string message = String.Format("Przyznano bonus do mnożnika punktów");
             Label label = new Label()
                {
                    Tag = "notification",
                    Content = message,
                    Background = Brushes.DarkGray,
                    Opacity = 0.4
                };
                _mapView.AddNotification(label);
            
        }

    }
}
