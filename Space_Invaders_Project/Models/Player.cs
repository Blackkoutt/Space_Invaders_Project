using Space_Invaders_Project.Extensions.Strategy;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models
{
    public class Player
    {
        private Point position;
        private int health;
        private int damage;
        private float attackSpeed;
        private int scoreMultiplier;
        private float attackVelocity;
        private int movementSpeed;
        private ImageBrush skin;
        private Rectangle model;
        private Rect hitbox;
        private bool isDead;
        private Player_Bonus bonus;
        private int score;
        private static Player? playerInstance = null;

        private Player()
        {
            SetDefaultStats();  
            skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/player.png")) };
            model = new Rectangle { Tag = "player", Fill = this.skin, Height = 65, Width = 55, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
        }


        // Metoda ustawiająca defaultowe wartości gracza
        public void SetDefaultStats()
        {
            health = 30;
            damage = 10;
            movementSpeed = 10;
            attackSpeed = 2;
            scoreMultiplier = 1;
            attackSpeed = 1.0f;
            attackVelocity = 6;
            isDead = false;
            score = 0;
        }
        

        // Metoda pobierająca instancję gracza
        public static Player getInstance()
        {
            if (playerInstance == null)
            {
                playerInstance = new Player();
            }
            return playerInstance;
        }

        
        // Metoda ustawiająca pozycję gracza
        public void setPosition(int x, int y)
        {
            position = new Point(x, y);
            hitbox.X = x;
            hitbox.Y = y;
        }


        // Metoda dodająca bonus graczowi
        public void addBonus(Player_Bonus bonus)
        {
            bonus.admitBonus(this);
            bonus.setTime();         
        }

        //Metoda konczaca bonus
         public void stopBonus(Player_Bonus endBonus)
        {
            endBonus.EndBonus(this);
        }


        // Metoda tworząca pocisk gracza
        public Player_Missile shootMissile()
        {
            return new Player_Missile(new Point((position.X + model.Width / 2) - 2, position.Y), attackVelocity, damage);
        }
        // Metoda ustawijąca bonus
        public Player_Bonus setBonusStrategy(Player_Bonus strategy)
        {
            bonus = strategy;
            return bonus;
        }


        // Metoda dodająca score 
        public void addScore()
        {
            score += scoreMultiplier;
        }


        // Metoda aktualizująca HP gracza
        public void dealDamage(int enemyDamage)
        {
            health -= enemyDamage;
            if (health <= 0)
                isDead = true;
        }


        // Gettery i settery
        public Rect Hitbox 
        { 
            get { return hitbox; }
        }
        public Rectangle Model 
        { 
            get { return model; }
        }
        public Point Position 
        { 
            get { return position; }
        }
        public ImageBrush Skin 
        { 
            get { return skin; }
        }       
        public bool IsDeath
        {
            get { return isDead; }
            set { isDead = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public float AttackVelocity
        {
            get { return attackVelocity; }
            set { attackVelocity = value; }
        }
        public int MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }
        public float AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int ScoreMultiplier
        {
            get { return scoreMultiplier; }
            set { scoreMultiplier = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }

}
