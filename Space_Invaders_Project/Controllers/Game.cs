﻿using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Extensions.Builder;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Space_Invaders_Project.Controllers
{
    public class Game : IGame
    {
        private MapBuilder builder;
        private IMapView _mapView;
        private DispatcherTimer gameTimer;
        public event EventHandler GameLoopTimerEvent;

        public Game(IMapView mapView) 
        {
            _mapView = mapView;
            gameTimer = new DispatcherTimer();
        }


        // Metoda przygotowująca grę
        public void StartGame()
        {
           // List<Enemy> enemies = builder.CreateEnemies(1); // 1 level
           // List<Barrier> barriers = builder.GetBarrier();
            Player player = Player.getInstance();
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width/2 - (player.Model.Width/2)), (int)windowSize.Height - 105);

            Notification notification = new Notification(mapView);
            HighScores.AddSubscriber(notification);
            ScoreBoard scoreBoard = new ScoreBoard();
            HighScores.AddSubscriber(scoreBoard);

            // Przygotowanie mapy
            Game_Controller controller = new Game_Controller(player, _mapView, this);
            _mapView.PrepareMap(player);

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
        public void NextLevel(int level)
        {
            // wyczyszczenie planszy i utworzenie nowych przeciwników
        }
    }
}
