using Space_Invaders_Project.Models;
using System.Windows;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class Default : Player_Bonus
    {
        public Default(Point position)  : base(position) 
        { 
            
        }        
        public override void admitBonus(Player player)
        {
            player.SetDefaultStats();
        }

        public override void EndBonus(Player player)
        {
            throw new System.NotImplementedException();
        }
        public override void Notify()
        {
            
        }
    }
}
