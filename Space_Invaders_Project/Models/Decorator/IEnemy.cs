using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models.Decorator
{
    public interface IEnemy
    {
        public void SetPosition(int x, int y);
        public Enemy_Missile shootMissile();
        public void setBodySkin(BitmapImage source);
        public void setArmSkin(BitmapImage source);
        public void setLegSkin(BitmapImage source);
        public void dealDamage(Player player);
        public void setHealth(int hp);
        public void setDamage(int dmg);
        public void setAttackVelocity(int atv);
        public void onDeath();
        public Rect Hitbox { get; }
        public bool IsDead { get; }
        //public void drawEnemy(Canvas canvas);
        //public Rectangle[] getModel();
        public Rectangle ArmModel { get; }
        public Rectangle BodyModel { get; }
        public Rectangle LegModel { get; }
        public Point Position { get; }
    }
}
