using Space_Invaders_Project.Models.Decorator;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models
{
    public abstract class Enemy_Decorator : IEnemy
    {
        protected IEnemy decoratedEnemy;

        public Enemy_Decorator(IEnemy enemy)
        {
            decoratedEnemy = enemy;
        }
        public virtual void SetPosition(int x, int y)
        {
            decoratedEnemy.SetPosition(x, y);
        }

        public virtual Enemy_Missle shootMissle(int dmg, int speed)
        {
            return decoratedEnemy.shootMissle(dmg, speed);
        }

        public virtual void dealDamage(Player player)
        {
            decoratedEnemy.dealDamage(player);
        }

        public virtual void onDeath(Player player)
        {
            decoratedEnemy.onDeath(player);
        }

        public virtual void setAttackVelocity(int atv)
        {
            decoratedEnemy.setAttackVelocity(atv);
        }

        public virtual void setDamage(int dmg)
        {
            decoratedEnemy.setDamage(dmg);
        }

        public virtual void setHealth(int hp)
        {
            decoratedEnemy.setHealth(hp);
        }

        public virtual void setBodySkin(BitmapImage source)
        {
            decoratedEnemy.setBodySkin(source);
        }
        public virtual void setArmSkin(BitmapImage source)
        {
            decoratedEnemy.setArmSkin(source);
        }
        public virtual void setLegSkin(BitmapImage source)
        {
            decoratedEnemy.setLegSkin(source);
        }

        public virtual void drawEnemy(Canvas canvas)
        {
            decoratedEnemy.drawEnemy(canvas);
        }

        public virtual Rectangle[] getModel()
        {
            return decoratedEnemy.getModel();
        }
    }
}