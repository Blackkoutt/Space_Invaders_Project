using Space_Invaders_Project.Models.Decorator;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models
{

    public class Default_Enemy : IEnemy
    {
        protected System.Drawing.Point position;
        protected ImageBrush armSkin, bodySkin, legSkin;
        protected Rectangle armModel, bodyModel, legModel;
        protected Rect hitbox;
        protected int health;
        protected int damage;
        protected int attackVelocity;
        protected bool isDead;


        public Default_Enemy(System.Drawing.Point position)
        {
            this.position = position;
            setBodySkin(new BitmapImage(new Uri("pack://application:,,,/Assets/baseEnemyBody.png")));
            setArmSkin(new BitmapImage(new Uri("pack://application:,,,/Assets/baseEnemyArm.png")));
            setLegSkin(new BitmapImage(new Uri("pack://application:,,,/Assets/baseEnemyLeg.png")));
            this.armModel = new Rectangle() { Fill = this.armSkin, Height = 45, Width = 45 };
            this.bodyModel = new Rectangle() { Fill = this.bodySkin, Height = 45, Width = 45 };
            this.legModel = new Rectangle() { Fill = this.legSkin, Height = 45, Width = 45 };
            this.hitbox = new Rect(position.X, position.Y, bodyModel.Width, bodyModel.Height);
            setHealth(10);
            setDamage(10);
            setAttackVelocity(6);
        }

        public void SetPosition(int x, int y)
        {
            position = new System.Drawing.Point(x, y);
        }
        public void dealDamage(Player player)
        {
            health -= 1;
            if (health == 0)
                onDeath(player);
        }

        public void onDeath(Player player)
        {
            player.addScore();
        }

        public void setAttackVelocity(int atv)
        {
            attackVelocity = atv;
        }

        public void setDamage(int dmg)
        {
            damage = dmg;
        }

        public void setHealth(int hp)
        {
            health = hp;
        }

        public void setBodySkin(BitmapImage source)
        {
            bodySkin = new ImageBrush() { ImageSource = source };
            if (bodyModel == null)
                bodyModel = new Rectangle() { Fill = bodySkin };
            else
                bodyModel.Fill = bodySkin;
        }
        public void setArmSkin(BitmapImage source)
        {
            armSkin = new ImageBrush() { ImageSource = source };
            if (armModel == null)
                armModel = new Rectangle() { Fill = armSkin };
            else
                armModel.Fill = armSkin;
        }
        public void setLegSkin(BitmapImage source)
        {
            legSkin = new ImageBrush() { ImageSource = source };
            if (legModel == null)
                legModel = new Rectangle() { Fill = legSkin };
            else
                legModel.Fill = legSkin;
        }

        public Enemy_Missle shootMissle(int dmg, int speed)
        {
            return new Enemy_Missle(position, dmg, speed);
        }

        public void drawEnemy(Canvas canvas)
        {
            Canvas.SetTop(this.armModel, position.X);
            Canvas.SetLeft(this.armModel, position.Y);
            Canvas.SetTop(this.bodyModel, position.X);
            Canvas.SetLeft(this.bodyModel, position.Y);
            Canvas.SetTop(this.legModel, position.X);
            Canvas.SetLeft(this.legModel, position.Y);
        }

        public void addEnemyToCanvas(Canvas canvas)
        {
            foreach (Rectangle r in getModel())
            {
                canvas.Children.Add(r);
            }
        }

        public Rectangle[] getModel()
        {
            return new Rectangle[] { armModel, bodyModel, legModel };
        }

        public static IEnemy enemyGenerator(System.Drawing.Point position, int numberOfDecorators)
        {
            if (numberOfDecorators > 3)
                numberOfDecorators = 3;
            if (numberOfDecorators == 0)
                return new Default_Enemy(position);

            IEnemy enemy = new Default_Enemy(position);
            Random rnd = new Random();
            List<Func<IEnemy, IEnemy>> decoratorFactories = new List<Func<IEnemy, IEnemy>>
            {
                (e) => new HealthEnemyDecorator(e),
                (e) => new SpeedEnemyDecorator(e),
                (e) => new DamageEnemyDecorator(e)
            };

            for (int i = 0; i < numberOfDecorators; i++)
            {
                int randomIndex = rnd.Next(decoratorFactories.Count);
                enemy = decoratorFactories[randomIndex](enemy);
                decoratorFactories.RemoveAt(randomIndex);
            }

            return enemy;
        }
    }
}
