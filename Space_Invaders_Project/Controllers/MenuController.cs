using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
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
            _menuView.FullScreenModeEvent += HandleFullScreenModeEvent;
            _menuView.ExitGameEvent += HandleExitGameEvent;
            _menuView.ShowHighScoresEvent += HandleShowHighScoresEvent;
            _menuView.ReturnToMenuEvent += HandleReturnToMenuEvent;
        }

        private void HandleShowHighScoresEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.ShowHighScores();
        }

        private void HandleExitGameEvent(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleFullScreenModeEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.ChangeWindowSize();
            _menuView.CreateMenuButtons();
        }
        
        private void HandleStartGameEvent(object? sender, EventArgs e)
        {

        }
        private void HandleReturnToMenuEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuButtons();
            _menuView.CreateMenuButtons();
        }

    }
}
