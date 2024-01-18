using System;

namespace Space_Invaders_Project.Controllers.Interfaces
{
    public interface IGame
    {
        event EventHandler GameLoopTimerEvent;

        int NextLevel(Game_Controller gc, int level);
        void GameOver();
    }
}
