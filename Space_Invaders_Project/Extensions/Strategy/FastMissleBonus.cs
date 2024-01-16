using Space_Invaders_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders_Project.Extensions.Strategy
{
    public class FastMissleBonus : Player_Bonus
    {
        public FastMissleBonus() { }
        public override void admitBonus(Player player)
        {
            player.MissileSpeed = 1.2f; // przykładowa wartość
        }
    }
}
