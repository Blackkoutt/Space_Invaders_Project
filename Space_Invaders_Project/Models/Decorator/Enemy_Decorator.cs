namespace Space_Invaders_Project.Models
{
    public abstract class Enemy_Decorator : Enemy
    {
        private Enemy enemy;

        public Enemy_Decorator(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void dealDamage()
        {
            throw new System.NotImplementedException();
        }

        public override int onDeath()
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
            return enemy.setHealth();
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
