using Space_Invaders_Project.Extensions.Builder;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Views;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Space_Invaders_Project.Controllers
{
    public class Game
    {
        private MapBuilder builder;

        public Game() { }

        public void StartGame()
        {
            List<Enemy> enemies = builder.CreateEnemies(1); // 1 level
            List<Barrier> barriers = builder.GetBarrier();
            // Player player 
            Notification notification = new Notification();
            HighScores.AddSubscriber(notification);
            ScoreBoard scoreBoard = new ScoreBoard();
            HighScores.AddSubscriber(scoreBoard);
            new Game_Controller();
        }
        public void GameOver()
        {
            HighScores.RemoveAllSubscribers();
        }
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
