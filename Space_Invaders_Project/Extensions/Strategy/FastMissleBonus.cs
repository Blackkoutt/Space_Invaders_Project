using Space_Invaders_Project.Models;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using Space_Invaders_Project.Views.Interfaces;


namespace Space_Invaders_Project.Extensions.Strategy
{
    public class FastMissleBonus : Player_Bonus
    {
        private IMapView _mapView;
        public FastMissleBonus(Point position)  : base(position) 
        { 
            
        }    
        public override void admitBonus(Player player)
        {
            player.AttackVelocity = 12;
        }
        public override void EndBonus(Player player)
        {
           player.AttackVelocity = 6;
        }
        public override void Notify()
        {
            string message = String.Format("Przyznano bonus do szybkości pocisków");
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
