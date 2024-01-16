using Space_Invaders_Project.Extensions.Strategy;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Space_Invaders_Project.Controllers
{
    public class Game_Controller
    {
        private List<Player_Missle> playerMissles;
        private List<Enemy> enemies;
        private Player player;
        private List<Barrier> barriers;
        private List<Enemy_Missle> enemyMissles;
        private int level;
        private static DispatcherTimer gameTimer = new DispatcherTimer();
        private int notificationTimer = 0;
        //private Game game;
        public Game_Controller()
        {
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

        }

        private void GameLoop(object sender, EventArgs e)
        {
            CheckIfNotificationToRemove();
        }

        // move enemies and missles
        public void Move()
        {

        }
        public void CheckForCollisions (IMissle missle)
        {

        }
        /*public void setGame(Game game)
        {

        }*/
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
                foreach (Label label in MainWindow.MainCanvas.Children.OfType<Label>())
                    if (label.Tag.ToString() == "notification")
                    {
                        notificationTimer++;
                        break;
                    }
            }
            else
                if (notificationTimer++ > 20)
                {
                    MapView.RemoveNotification();
                    notificationTimer = 0;
                }
        }
    }
}
