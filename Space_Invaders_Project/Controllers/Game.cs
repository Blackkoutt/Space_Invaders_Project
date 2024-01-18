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
            List<IEnemy> enemies = builder.CreateEnemies(1); // 1 level
            List<Barrier> barriers = builder.GetBarrier();
            Player player = Player.getInstance();
            Size windowSize = _mapView.GetWindowSize();
            player.setPosition((int)(windowSize.Width/2 - (player.Model.Width/2)), (int)windowSize.Height - 105);
            for(int i =0; i<enemies.Count;i++)
            {

            }
            /*for(int i = 0; i<barriers.Count; i++)
            {
                barriers[i].setPosition()
            }*/

            Notification notification = new Notification(_mapView);
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
        public int NextLevel(Game_Controller gc, int level)
        {
            foreach (Player_Missile pm in gc.PlayerMissiles)
            {
                gc.MapView.getCanvas().Children.Remove(pm.model);
            }
            foreach (Enemy_Missile em in gc.EnemyMissiles)
            {
                gc.MapView.getCanvas().Children.Remove(em.model);
            }
            gc.PlayerMissiles.Clear();
            gc.EnemyMissiles.Clear();

            gc.Enemies = builder.CreateEnemies(++level);

            return level;
        }
    }
}
