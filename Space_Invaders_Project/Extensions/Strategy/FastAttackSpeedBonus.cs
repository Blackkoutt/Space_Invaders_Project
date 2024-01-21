using Space_Invaders_Project.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using Space_Invaders_Project.Views.Interfaces;


namespace Space_Invaders_Project.Extensions.Strategy
{
    public class FastAttackSpeedBonus : Player_Bonus
    {
        private IMapView _mapView;
        public FastAttackSpeedBonus(Point position)  : base(position) 
        { 
            
        }    
        public override void admitBonus(Player player)
        {
            player.AttackSpeed = 4; 
        }
        public override void EndBonus(Player player)
        {
            player.AttackSpeed = 2;
        }
        public override void Notify()
        {
            string message = String.Format("Przyznano bonus do szybkości ataku");
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
