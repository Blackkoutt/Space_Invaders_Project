using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Space_Invaders_Project.Controllers
{
    public class Game_Controller
    {
       // private List<Player_Missile> playerMissiles;
        private List<Enemy> enemies;
        private Player player;
        private List<Barrier> barriers;
        private List<IMissile> missiles;
       // private List<Enemy_Missile> enemyMissles;
        private int level;
        private IMapView mapView;
        private string playerMoveDirection;
        private int notificationTimer = 0;
        public Game_Controller(Player player, IMapView mapView, IGame game)
        {
            this.player = player;
            this.mapView = mapView;

            playerMoveDirection = "";

            game.GameLoopTimerEvent += GameLoop;
            mapView.KeyDownEvent += HandleKeyDownEvent;
            mapView.KeyUpEvent += HandleKeyUpEvent;
            missiles = new List<IMissile>();

        }


        // Handler puszczenia przycisku 
        private void HandleKeyUpEvent(object? sender, KeyEventArgsWrapper e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
                playerMoveDirection = "";
            if (e.Key == Key.Space)
            {
                Player_Missile newMissile = player.shootMissile();
                missiles.Add(newMissile);
                mapView.SpawnMissileModel(newMissile.model, newMissile.Position);
            }
        }


        // Handler wciśnięcia przycisku 
        private void HandleKeyDownEvent(object? sender, KeyEventArgsWrapper e)
        {
            if (e.Key == Key.Left)
                playerMoveDirection = "left";
            else if (e.Key == Key.Right)
                playerMoveDirection = "right";
        }


        // Główna pętla gry wykonywana przez timer z klasy Game
        private void GameLoop(object? sender, EventArgs e)
        {
            CheckIfNotificationToRemove();
            MoveEntities();
            DrawEntities();
            
        }


        // Metoda rysująca wszytskie postacie na mapie
        private void DrawEntities()
        {
            mapView.DrawEntity(player.Model, player.Position);
            foreach(IMissile m in missiles)
            {
                mapView.DrawEntity(m.Model, m.Position);
            }
            
        }


        // Metoda przesuwająca gracza w zależności od wciśniętego wcześniej klawisza
        private void MovePlayer()
        {
            if (playerMoveDirection == "right" && player.Position.X + 75 < mapView.getCanvas().Width)
            {
                player.setPosition((int)player.Position.X + 10, (int)player.Position.Y);
            }
            else if (playerMoveDirection == "left" && player.Position.X > 0)
            {
                player.setPosition((int)player.Position.X - 10, (int)player.Position.Y);
            }
        }


        // Metoda przesuwająca pocisk
        private void MoveMissile(IMissile missile)
        {
            missile.setPosition(missile.Speed);
        }


        // Metoda przesuwająca wszytskie obiekty znajdujące się na mapie
        private void MoveEntities()
        {
            MovePlayer();
            foreach(IMissile m in missiles)
            {
                MoveMissile(m);
            }
        }


        public void CheckForCollisions (IMissile missle)
        {

        }

        public void SetEnemies(List<Enemy> enemies)
        {

        }
        public void SetPlayer(Player player)
        {

        }
        public void SetBarrier(List<Barrier> barrier) 
        {
        }
        public void MissleControl()
        {

        }
        private void CheckIfNotificationToRemove()
        {
            if (notificationTimer == 0)
            {
                foreach (Label label in mapView.getCanvas().Children.OfType<Label>())
                {
                    if (label.Tag.ToString() == "notification")
                    {
                        notificationTimer++;
                        break;
                    }
                }
                    
            }
            else
                if (notificationTimer++ > 20)
                {
                    mapView.RemoveNotification();
                    notificationTimer = 0;
                }
        }
    }
}
