using Space_Invaders_Project.Models;
using System;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class DamageDealtBonus : Player_Bonus
    {
        public DamageDealtBonus() { }
        public override void admitBonus(Player player)
        {
            player.Damage = 120; // przykładowa wartość
        }
    }
}
