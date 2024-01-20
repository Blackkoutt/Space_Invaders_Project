using Space_Invaders_Project.Extensions.Observer;
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
        event EventHandler<DifficultyEventArgs> ChooseDifficultyEvent;
        event EventHandler ReturnToDifficultyEvent;
        event EventHandler<PlayerNicknameEventArgs> PlayGameEvent;

        void ChangeWindowSize();
        void CreateMenuButtons();
        void ShowDescription();
        void ShowNicknameError(string variant);
        void LoadGameOverScreen(int score);
        Canvas GetCanvas();
        void ClearMenuView();
        MainWindow GetMainWindow();
        void ShowEnterNicknameView();
        void CreateDifficultyButtonsAndLabel();
        void ShowHighScores(HighScores highScores);
    }
}
