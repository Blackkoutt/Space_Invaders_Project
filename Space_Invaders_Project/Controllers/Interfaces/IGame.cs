using System;

namespace Space_Invaders_Project.Controllers.Interfaces
{
    public interface IGame
    {
        event EventHandler GameLoopTimerEvent;

        void NextLevel();
        void GameOver();
    }
}
