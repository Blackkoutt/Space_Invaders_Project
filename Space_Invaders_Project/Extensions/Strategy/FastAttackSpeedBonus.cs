using Space_Invaders_Project.Models;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class FastAttackSpeedBonus : Player_Bonus
    {
        public FastAttackSpeedBonus() { }
        public override void admitBonus(Player player)
        {
            player.AttackSpeed = 1.2f; // przykładowa wartość
        }
    }
}
