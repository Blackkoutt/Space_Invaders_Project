using Space_Invaders_Project.Models.Decorator;
using System.Windows;
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
        public Point Position
        {
            get { return decoratedEnemy.Position; }
        }
        public bool IsDead
        {
            get { return decoratedEnemy.IsDead; }
        }

        public Rectangle ArmModel
        {
            get { return decoratedEnemy.ArmModel; }
        }
        public Rectangle BodyModel
        {
            get { return decoratedEnemy.BodyModel; }
        }
        public Rectangle LegModel
        {
            get { return decoratedEnemy.LegModel; }
        }
        public Rect Hitbox
        {
            get { return decoratedEnemy.Hitbox; }
        }

        public virtual void SetPosition(int x, int y)
        {
            decoratedEnemy.SetPosition(x, y);
        }

        public virtual Enemy_Missile shootMissile()
        {
            return decoratedEnemy.shootMissile();
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

        /*public virtual void drawEnemy(Canvas canvas)
        {
            decoratedEnemy.drawEnemy(canvas);
        }*/

        /*public virtual Rectangle[] getModel()
        {
            return decoratedEnemy.getModel();
        }*/
    }
}