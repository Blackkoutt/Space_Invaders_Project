using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Extensions.Builder;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
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
        private HighScores highScores;
        private IMenuView menuView;

        public Game(IMapView mapView, IMenuView menuView, HighScores highScores) 
        {
            _mapView = mapView;
            this.menuView = menuView;
            this.highScores = highScores;
            gameTimer = new DispatcherTimer();
        }

        
        // Getter do Highscores
        public HighScores HighScores 
        { 
            get { return highScores; } 
        }


        // Metoda przygotowująca grę
        public void StartGame()
        {
            // Builder
            List<IEnemy> enemies = builder.CreateEnemies(1); // 1 level
            List<DefenceBarrier> barriers = builder.GetBarrier();

            // Pobranie singletona
            player = Player.getInstance();
            player.SetDefaultStats();

            // Ustawienie początkowej pozycji gracza
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width/2 - (player.Model.Width/2)), (int)windowSize.Height - 105);

            // Dodanie konkretnych subskrybentów do Highscore
            Notification notification = new Notification(_mapView);
            highScores.AddSubscriber(notification);
            ScoreBoard scoreBoard = new ScoreBoard();
            highScores.AddSubscriber(scoreBoard);

            // Przygotowanie mapy
            gc = new Game_Controller(player, _mapView, enemies, barriers, this);
            _mapView.PrepareMap(player, enemies);

            // Uruchomienie Timera który co 20 milisekund wykonuje główną pętlę gry
            gameTimer.Tick += delegate { GameLoopTimerEvent?.Invoke(this, EventArgs.Empty); };
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }


        // Metoda wywoływana w momencie gdy gracz ginie
        public void GameOver()
        {
            // Zatrzymanie timera wykonującego główną pętlę gry
            gameTimer.Stop();
            gameTimer.Tick -= delegate { GameLoopTimerEvent?.Invoke(this, EventArgs.Empty); };
            _mapView.RemoveAllEntitiesAndEvents();

            // Usunięcie subskrybentów i zapis tablicy wyników do pliku
            highScores.RemoveAllSubscribers();
            highScores.SaveToFile();

            // Wyświetlenie ekranu Game Over
            menuView.LoadGameOverScreen(player.Score);
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

        
        // Metoda przenosząca gracza do kolejnego levelu
        public void NextLevel()
        {
            gameTimer.Stop();

            // Usunięcie wszytskich pocisków z mapy 
            foreach (IMissile missile in gc.Missiles)
            {
                _mapView.RemoveEntity(missile.Model);
            }
            gc.Missiles.Clear();

            moveToNextLevelTimer = new DispatcherTimer();
            moveToNextLevelTimer.Tick += MovePlayerToNextLevel;
            moveToNextLevelTimer.Interval = TimeSpan.FromMilliseconds(15);
            moveToNextLevelTimer.Start();
        }


        // Metoda przygotowująca kolejny level
        private void SetNextLevel()
        {
            // Ustawienie gracza w środkowej pozycji na dole canvasu
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width / 2 - (player.Model.Width / 2)), (int)windowSize.Height - 105);

            // Pobranie listy przeciwników dla kolejnego poziomu
            List<IEnemy> enemies = builder.CreateEnemies(++level);
            gc.Enemies = enemies;

            // Przygotowanie kolejnego poziomu 
            _mapView.PrepareNextLevel(enemies);

            // Wznowienie pętli gry
            gameTimer.Start();
        }


        // Metoda automatycznie przesuwająca gracza do góry (animacja przejścia do kolejnego poziomu)
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
