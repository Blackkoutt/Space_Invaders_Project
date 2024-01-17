using Space_Invaders_Project.Models.Decorator;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class HealthEnemyDecorator : Enemy_Decorator
    {
        public HealthEnemyDecorator(IEnemy enemy) : base(enemy)
        {
            enemy.setHealth(20);
            enemy.setBodySkin(new BitmapImage(new Uri("pack://application:,,,/Assets/healthEnemyBody.png")));
        }
    }
}