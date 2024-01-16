using System.Drawing;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Space_Invaders_Project.Models
{
    public abstract class Enemy
    {
        protected Point position;
        protected int health;
        protected bool isDead;
        protected ImageBrush model;

        public void SetPosition(int x, int y)
        {
            position = new Point(x, y);
        }
        public abstract Enemy_Missile shotMissle();
        public abstract void setImage();
        public abstract void dealDamage();
        public abstract int setHealth();
        public abstract void setDamage();   
        public abstract void setAttackSpeed();
        public abstract void onDeath(Player player);
    }
}
