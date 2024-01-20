using Space_Invaders_Project.Extensions.Observer;
using System;

namespace Space_Invaders_Project.Controllers.Interfaces
{
    public interface IGame
    {
        event EventHandler GameLoopTimerEvent;
        HighScores HighScores { get; }
        void NextLevel();
        void GameOver();
    }
}
