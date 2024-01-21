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

        private string name; 
        public FastMissleBonus(Point position)  : base(position) 
        { 
            name="Szybkosc atakow";
        }    
        public override void admitBonus(Player player)
        {
            player.AttackVelocity = 12;
            Console.WriteLine("szybkie ataki");
        }
        public override void EndBonus(Player player)
        {
           player.AttackVelocity = 6;
        }
        public override string Name ()
        {
            return name;
        }  

    }
}
