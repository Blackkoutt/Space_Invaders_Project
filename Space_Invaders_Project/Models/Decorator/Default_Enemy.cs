using System;

namespace Space_Invaders_Project.Models
{
    public class Default_Enemy : Enemy
    {
        public Default_Enemy() {
            this.health=setHealth();
        }
        public override void dealDamage()
        {
            throw new NotImplementedException();
        }

        public override void onDeath(Player player)
        {
            throw new NotImplementedException();
        }

        public override void setAttackSpeed()
        {
            throw new NotImplementedException();
        }

        public override void setDamage()
        {
            throw new NotImplementedException();
        }

        public override int setHealth()
        {
            return 10;
        }

        public override void setImage()
        {
            throw new NotImplementedException();
        }

        public override Enemy_Missle shotMissle()
        {
            throw new NotImplementedException();
        }
    }
}
