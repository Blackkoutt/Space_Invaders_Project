using Space_Invaders_Project.Extensions.Observer;
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
        private float attackVelocity;
        private float movementSpeed;
        private ImageBrush skin;
        private Rectangle model;
        private Rect hitbox;
        private bool isDead;
        private Player_Bonus bonus;
        private int score;
        private static Player? playerInstance = null;

        private Player()
        {
            // tu defaultowe wartości

            // x i y zależne raczej od szerokości okna 
            // powinny być ustawiane w controlerze w zależności od canvasu zaraz po pobraniu instancji
            /*position = new Point(x,y)*/

            // Przykładowe początkowe wartości
            health = 3;
            damage = 1;
            attackSpeed = 2;
            missileSpeed = 6;
            scoreMultiplier = 1;
            attackSpeed = 1.0f;
            attackVelocity = 1.0f;

            skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/player.png")) };
            model = new Rectangle { Tag = "player", Fill = this.skin, Height = 65, Width = 55, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            hitbox = new Rect(position.X, position.Y, model.Width, model.Height);
            isDead = false;
            score = 0;  // nie wiem czy tutaj powinnismy trzymać score
        }
        public static Player getInstance()
        {
            if (playerInstance == null)
            {
                playerInstance = new Player();
            }
            return playerInstance;
        }
        // Nie wiem co to dokładnie miało na celu
        /*public Player_Missle shootMissle()
        {
            return new Player_Missle();
        }*/

        public void setPosition(int x, int y)
        {
            position = new Point(x, y);
            hitbox.X = x;
            hitbox.Y = y;
        }
        public void addBonus()
        {
            bonus.admitBonus(this);
        }
        public Player_Missile shootMissile()
        {
            return new Player_Missile(new Point((position.X + model.Width / 2) - 2, position.Y), missileSpeed, damage);
        }
        public void setBonusStrategy(Player_Bonus strategy)
        {
            bonus = strategy;
        }
        public Rect Hitbox { get { return hitbox; } }
        public Rectangle Model { get { return model; } }
        public Point Position { get { return position; } }
        public ImageBrush Skin { get { return skin; } }
        public void addScore()
        {
            score += scoreMultiplier;
            HighScores.Notification(score);
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
        public float MovementSpeed
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
        // to możliwe że nie potrzebne tutaj
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }

}
