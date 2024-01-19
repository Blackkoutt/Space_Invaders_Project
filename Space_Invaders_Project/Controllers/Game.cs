using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Extensions.Builder;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Space_Invaders_Project.Controllers
{
    public class Game : IGame
    {
        private MapBuilder builder;
        private IMapView _mapView;
        private DispatcherTimer gameTimer;
        private DispatcherTimer moveToNextLevelTimer;
        public event EventHandler GameLoopTimerEvent;
        private Player player;
        private Game_Controller gc;
        private int level = 1;

        public Game(IMapView mapView) 
        {
            _mapView = mapView;
            gameTimer = new DispatcherTimer();
        }


        // Metoda przygotowująca grę
        public void StartGame()
        {
            // Builder
            List<IEnemy> enemies = builder.CreateEnemies(1); // 1 level
            List<DefenceBarrier> barriers = builder.GetBarrier();
            player = Player.getInstance();

            // Ustawienie początkowej pozycji gracza
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width/2 - (player.Model.Width/2)), (int)windowSize.Height - 105);

           
            Notification notification = new Notification(_mapView);
            HighScores.AddSubscriber(notification);
            ScoreBoard scoreBoard = new ScoreBoard();
            HighScores.AddSubscriber(scoreBoard);

            // Przygotowanie mapy
            gc = new Game_Controller(player, _mapView, enemies, barriers, this);
            _mapView.PrepareMap(player, enemies);

            // Uruchomienie Timera który co 20 milisekund wykonuje główną pętlę gry
            gameTimer.Tick += delegate { GameLoopTimerEvent?.Invoke(this, EventArgs.Empty); };
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }
        public void GameOver()
        {
            HighScores.RemoveAllSubscribers();
        }


        // Metoda przypisująca buider w zależności od wybranego przez użytkownika poziomu trudności
        public void ChooseMapBuilder(byte difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    {
                        builder = new MapEasyLevelBuilder();
                        break;
                    }
                case 1:
                    {
                        builder = new MapMediumLevelBuilder();
                        break;
                    }
                case 2:
                    {
                        builder = new MapHardLevelBuilder();
                        break;
                    }
            }
        }
        public void NextLevel()
        {
            gameTimer.Stop();
            moveToNextLevelTimer = new DispatcherTimer();
            moveToNextLevelTimer.Tick += MovePlayerToNextLevel;
            moveToNextLevelTimer.Interval = TimeSpan.FromMilliseconds(15);
            moveToNextLevelTimer.Start();
        }
        private void SetNextLevel()
        {
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width / 2 - (player.Model.Width / 2)), (int)windowSize.Height - 105);

            foreach (IMissile missile in gc.Missiles)
            {
                _mapView.RemoveEntity(missile.Model);
            }
            gc.Missiles.Clear();


            List<IEnemy> enemies = builder.CreateEnemies(++level);
            gc.Enemies = enemies;
            _mapView.PrepareNextLevel(enemies);
            gameTimer.Start();
        }
        private void MovePlayerToNextLevel(object? sender, EventArgs e)
        {
            if (player.Position.Y <= 0 - player.Model.Height)
            {
                moveToNextLevelTimer.Stop();
                SetNextLevel();
                return;
            }

            player.setPosition((int)player.Position.X, (int)player.Position.Y - 10);
            _mapView.DrawEntity(player.Model, player.Position);
        }
    }
}
