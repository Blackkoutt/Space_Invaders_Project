namespace Space_Invaders_Project.Models
{
    public class Yellow_Enemy : Enemy_Decorator
    {
        public Yellow_Enemy(Enemy enemy) : base(enemy)
        {
            this.health = setHealth();
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
            //this.
        }

        public override void setDamage()
        {
            throw new System.NotImplementedException();
        }

        public override int setHealth()
        {
            return base.setHealth()+20;
            //this.health =// base.setHealth() + 20;
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
