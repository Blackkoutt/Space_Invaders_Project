using Space_Invaders_Project.Models;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class ScoreMultiplier : Player_Bonus
    {
        public ScoreMultiplier() { }    
        public override void admitBonus(Player player)
        {
            player.ScoreMultiplier = 2; // przykładowa wartość mnożnika
        }
    }
}
