using Space_Invaders_Project.Models.Decorator;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class SpeedEnemyDecorator : Enemy_Decorator
    {
        public SpeedEnemyDecorator(IEnemy enemy) : base(enemy)
        {
            ((Default_Enemy)enemy).setAttackVelocity(12);
            enemy.setLegSkin(new BitmapImage(new Uri("pack://application:,,,/Assets/speedEnemyLeg.png")));
        }
    }
}