using Space_Invaders_Project.Models;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class MovementSpeedBonus : Player_Bonus
    {
        public MovementSpeedBonus() { }
        public override void admitBonus(Player player)
        {
            player.MovementSpeed = 1.3f;
        }
    }
}
