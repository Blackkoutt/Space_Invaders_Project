using Space_Invaders_Project.Models.Decorator;
using System;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class DamageEnemyDecorator : Enemy_Decorator
    {
        public DamageEnemyDecorator(IEnemy enemy) : base(enemy)
        {
            enemy.setDamage(20);
            enemy.setArmSkin(new BitmapImage(new Uri("pack://application:,,,/Assets/damageEnemyArm.png")));
        }
    }
}