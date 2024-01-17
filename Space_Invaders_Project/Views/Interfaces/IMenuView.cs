using System;
using System.Windows.Controls;

namespace Space_Invaders_Project.Views.Interfaces
{
    public interface IMenuView
    {
        event EventHandler StartGameEvent;
        event EventHandler ShowDescriptionEvent;
        event EventHandler ShowHighScoresEvent;
        event EventHandler ExitGameEvent;
        event EventHandler FullScreenModeEvent;
        event EventHandler ReturnToMenuEvent;

        event EventHandler ReturnFromDescriptionEvent;

        event EventHandler<DifficultyEventArgs> ChooseDifficultyEvent;

        void ChangeWindowSize();
        void ClearDifficultyButtons();
        void ClearMenuButtons();
        void CreateMenuButtons();
        void ShowDescription();
        void ClearDescription();
        Canvas GetCanvas();
        MainWindow GetMainWindow();
        void CreateDifficultyButtons();
        void ShowHighScores();
    }
}
