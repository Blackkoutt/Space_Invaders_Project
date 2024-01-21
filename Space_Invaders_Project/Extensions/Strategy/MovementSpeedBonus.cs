using Space_Invaders_Project.Models;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using Space_Invaders_Project.Views.Interfaces;


namespace Space_Invaders_Project.Extensions.Strategy
{
    public class MovementSpeedBonus : Player_Bonus
    {

        private string name; 
        public MovementSpeedBonus(Point position) : base(position) 
        { 
            name="Predkosc ruchow";
        }    
        public override void admitBonus(Player player)
        {
            player.MovementSpeed = 15;
        }

        public override void EndBonus(Player player)
        {
            player.MovementSpeed = 10;
        }
        public override string Name ()
        {
            return name;
        }

    }
}
