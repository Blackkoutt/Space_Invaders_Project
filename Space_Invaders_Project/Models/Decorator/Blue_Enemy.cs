namespace Space_Invaders_Project.Models
{
    public class Blue_Enemy : Enemy_Decorator
    {
        public Blue_Enemy(Enemy enemy) : base(enemy)
        {
        }
        public override void dealDamage()
        {
            throw new System.NotImplementedException();
        }

        public override void onDeath(Player player)
        {
            throw new System.NotImplementedException();
        }

        public override void setAttackSpeed()
        {
            throw new System.NotImplementedException();
        }

        public override void setDamage()
        {
            throw new System.NotImplementedException();
        }

        public override int setHealth()
        {
            throw new System.NotImplementedException();
        }

        public override void setImage()
        {
            throw new System.NotImplementedException();
        }

        public override Enemy_Missle shotMissle()
        {
            throw new System.NotImplementedException();
        }
    }
}
