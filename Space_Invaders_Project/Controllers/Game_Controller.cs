using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Space_Invaders_Project.Controllers
{
    public class Game_Controller
    {
        private MainWindow _mainWindow;
        private IMapView _mapView;
        private IGame gameFacade;
        private Player player;
        private List<Player_Missile> playerMissles = new List<Player_Missile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<Barrier> barriers = new List<Barrier>();
        private List<IMissile> enemyMissiles = new List<IMissile>();
       // private List<Enemy_Missile> enemyMissles;
        private int level = 1;
        private string playerMoveDirection = "";
        private int notificationTimer = 0;
        private bool gamePaused = false;

        public Game_Controller(Player player, IMapView mapView, IGame game)
        {
            this.player = player;
            this._mapView = mapView;
            this.gameFacade = game;

            game.GameLoopTimerEvent += GameLoop;
            mapView.KeyDownEvent += HandleKeyDownEvent;
            mapView.KeyUpEvent += HandleKeyUpEvent;

        }

        public IMapView MapView { get { return _mapView; } }
        public List<Player_Missile> PlayerMissiles { get { return playerMissles; } }
        public List<IEnemy> Enemies { get {  return enemies; } set { enemies = value; } }
        public List<IMissile > EnemyMissiles { get { return enemyMissiles; } }


        // Handler puszczenia przycisku 
        private void HandleKeyUpEvent(object? sender, KeyEventArgsWrapper e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
                playerMoveDirection = "";
            if (e.Key == Key.Space && !gamePaused)
            {
                Player_Missile newMissile = player.shootMissile();
                enemyMissiles.Add(newMissile);
                _mapView.SpawnMissileModel(newMissile.model, newMissile.Position);
            }
        }


        // Handler wciśnięcia przycisku 
        private void HandleKeyDownEvent(object? sender, KeyEventArgsWrapper e)
        {
            if (e.Key == Key.Escape)
            {
                gamePaused = !gamePaused;
                if (gamePaused)
                    _mapView.drawPauseOverlay();
                else
                    _mapView.erasePauseOverlay();
            }
            if (e.Key == Key.Left)
                playerMoveDirection = "left";
            else if (e.Key == Key.Right)
                playerMoveDirection = "right";
        }


        // Główna pętla gry wykonywana przez timer z klasy Game
        private void GameLoop(object? sender, EventArgs e)
        {
            if (gamePaused)
                return;

            if (enemies.Count == 0)
                level = gameFacade.NextLevel(this, level);

            
            UpdateOverlay();
            MoveEntities();
            DrawEntities();
        }


        // Metoda rysująca wszytskie postacie na mapie
        private void DrawEntities()
        {
            _mapView.DrawEntity(player.Model, player.Position);
            foreach(IMissile m in enemyMissiles)
            {
                _mapView.DrawEntity(m.Model, m.Position);
            }
            foreach(IEnemy m in enemies)
            {
                m.drawEnemy(_mapView.getCanvas());
            }
            
        }


        // Metoda przesuwająca gracza w zależności od wciśniętego wcześniej klawisza
        private void MovePlayer()
        {
            if (playerMoveDirection == "right" && player.Position.X + 75 < _mapView.getCanvas().Width)
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
            foreach(IMissile m in enemyMissiles)
            {
                MoveMissile(m);
            }
        }


        public void CheckForCollisions (IMissile missle)
        {

        }
        public void SetEnemies(List<IEnemy> enemies)
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
        private void UpdateOverlay()
        {
            CheckIfNotificationToRemove();
            _mapView.UpdateScoreLabel(player.Score);
        }
        private void CheckIfNotificationToRemove()
        {
            if (notificationTimer == 0)
            {
                foreach (Label label in _mapView.getCanvas().Children.OfType<Label>())
                {
                    if (label.Tag.ToString() == "notification")
                    {
                        notificationTimer++;
                        break;
                    }
                }
                    
            }
            else
                if (notificationTimer++ > 50)
                {
                    _mapView.RemoveNotification();
                    notificationTimer = 0;
                }
        }
    }
}
