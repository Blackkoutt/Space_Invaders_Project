using Space_Invaders_Project.Extensions.Strategy;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Interfaces;
using System.Collections.Generic;

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
        //private Game game;
        public Game_Controller()
        {

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

    }
}
