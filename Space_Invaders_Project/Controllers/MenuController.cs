using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;

namespace Space_Invaders_Project.Controllers
{
    public class MenuController
    {
        private IMenuView _menuView;
        public MenuController(IMenuView menuView) 
        {
            _menuView = menuView;
            _menuView.StartGameEvent += HandleStartGameEvent;
            _menuView.ShowDescriptionEvent += HandleShowDescriptionEvent;
            _menuView.FullScreenModeEvent += HandleFullScreenModeEvent;
            _menuView.ExitGameEvent += HandleExitGameEvent;
            _menuView.ShowHighScoresEvent += HandleShowHighScoresEvent;
            _menuView.ReturnFromDescriptionEvent += HandleReturnFromDescriptionEvent;

            _menuView.ChooseDifficultyEvent += HandleChooseDifficultyEvent;
        }


        // Metoda obsługująca event kliknięcia przycisków wyboru trudności
        private void HandleChooseDifficultyEvent(object? sender, DifficultyEventArgs e)
        {
            _menuView.ClearDifficultyButtons();
            MapView mapView = new MapView(_menuView.GetMainWindow(),_menuView.GetCanvas());
            Game game = new Game(mapView);
            game.ChooseMapBuilder(e.DifficultyLevel);
            game.StartGame();
        }


        // Metoda obsługująca event kliknięcia przycisku wróć w widoku opisu gry
        private void HandleReturnFromDescriptionEvent(object? sender, EventArgs e)
        {
            _menuView.ClearDescription();
            _menuView.CreateMenuButtons();
        }


        // Metoda obsługująca event kliknięcia przycisku "Description" w głównym menu
        private void HandleShowDescriptionEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.ShowDescription();
        }


        // Metoda obsługująca event kliknięcia przycisku "Show Highscores" w głównym menu
        private void HandleShowHighScoresEvent(object? sender, EventArgs e)
        {

        }


        // Metoda obsługująca event kliknięcia przycisku "Exit" w głównym menu
        private void HandleExitGameEvent(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        // Metoda obsługująca event kliknięcia przycisku "Fullscreen" w głównym menu
        private void HandleFullScreenModeEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.ChangeWindowSize();
            _menuView.CreateMenuButtons();
        }


        // Metoda obsługująca event kliknięcia przycisku "Start" w głównym menu
        private void HandleStartGameEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.CreateDifficultyButtons();
        }
    }
}
