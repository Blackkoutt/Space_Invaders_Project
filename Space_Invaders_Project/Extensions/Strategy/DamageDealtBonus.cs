using Space_Invaders_Project.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;

using Space_Invaders_Project.Views.Interfaces;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class DamageDealtBonus : Player_Bonus
    {
        private IMapView _mapView;
        public DamageDealtBonus(Point position) : base(position) 
        { 
            
        }    
        public override void admitBonus(Player player)
        {
            player.Damage = 25;
        }
        public override void EndBonus(Player player)
        {
            player.Damage = 10;
        }
        public override void Notify()
        {
            string message =  String.Format("Przyznano bonus do obrazeń");
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
