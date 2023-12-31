﻿using Space_Invaders_Project.Extensions.Strategy;
using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Models
{
    public class Player
    {
        private Point position;
        private int health;
        private int damage;
        private float attackSpeed;
        private float missleSpeed;
        private float scoreMultiplier;
        private float movementSpeed;
        private ImageBrush model;
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
            health = 100;
            damage = 50;
            attackSpeed = 1.0f;
            missleSpeed = 1.0f;
            scoreMultiplier = 1.0f;
            this.model = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/player.png")) };

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
        /*public Player_Missle shotMissle()
        {
            return new Player_Missle();
        }*/

        public void setPosition(int x, int y)
        {
            position = new Point(x, y);
        }
        public void addBonus()
        {
            bonus.admitBonus(this);
        }
        public void setBonusStrategy(Player_Bonus strategy)
        {
            bonus = strategy;
        }
        public ImageBrush Model { get { return model; } }
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
        public float MissleSpeed
        {
            get { return missleSpeed; }
            set { missleSpeed = value; }
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
        public float ScoreMultiplier
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
