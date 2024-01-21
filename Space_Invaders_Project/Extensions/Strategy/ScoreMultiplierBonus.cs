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
  
        private string name;  
        public ScoreMultiplier(Point position) : base(position) 
        { 
            name="Mnoznik punktow";
        }    
        public override void admitBonus(Player player)
        {
            player.ScoreMultiplier = 2; 
        }
        public override void EndBonus(Player player)
        {
           player.ScoreMultiplier = 1; 
        }  
        public override string Name ()
        {
            return name;
        }

    }
}
