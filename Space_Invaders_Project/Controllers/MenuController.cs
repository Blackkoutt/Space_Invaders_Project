using Space_Invaders_Project.Extensions.Exceptions;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;

namespace Space_Invaders_Project.Controllers
{
    public class MenuController
    {
        private IMenuView _menuView;
        private HighScores highScores;
        private byte chosenDifficulty;
        public MenuController(IMenuView menuView) 
        {
            _menuView = menuView;

            // Dodanie obsługi eventów
            _menuView.StartGameEvent += HandleStartGameEvent;
            _menuView.ShowDescriptionEvent += HandleShowDescriptionEvent;
            _menuView.FullScreenModeEvent += HandleFullScreenModeEvent;
            _menuView.ExitGameEvent += HandleExitGameEvent;
            _menuView.ShowHighScoresEvent += HandleShowHighScoresEvent;
            _menuView.ChooseDifficultyEvent += HandleChooseDifficultyEvent;
            _menuView.ReturnToMenuEvent += HandleReturnToMenuEvent;
            _menuView.ReturnToDifficultyEvent += HandleStartGameEvent;
            _menuView.PlayGameEvent += HandlePlayGameEvent;

            // Odczytanie tablicy wyników z pliku
            highScores = new HighScores();
            try
            {
                highScores.ReadFromFile();
                
            }
            catch (CannotFindFileException CFFE)
            {
                MessageBox.Show(CFFE.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            catch (UnknownException UE)
            {
                MessageBox.Show(UE.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            catch (FileSyntaxException FSE)
            {
                MessageBox.Show(FSE.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            catch (CannotConvertException CCE)
            {
                MessageBox.Show(CCE.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }


        // Metoda wywoływana w momencie kliknięcia przycisku Play w widoku podawania nicku
        private void HandlePlayGameEvent(object? sender, PlayerNicknameEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.PlayerNickname) || e.PlayerNickname.Length < 1)
            {
                _menuView.ShowNicknameError("min_width");
            }
            else if (highScores.NickList.Contains(e.PlayerNickname))
            {
                _menuView.ShowNicknameError("exist");
            }
            else
            {
                // Tworzenie nowej gry
                _menuView.ClearMenuView();
                MapView mapView = new MapView(_menuView.GetMainWindow(),_menuView.GetCanvas());
                highScores.Nick = e.PlayerNickname;
                Game game = new Game(mapView, _menuView, highScores);
                game.ChooseMapBuilder(chosenDifficulty);
                game.StartGame();
            }
        }


        // Metoda obsługująca event wciśnięcia przycisku return powodującego powrót do menu głównego
        private void HandleReturnToMenuEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.CreateMenuButtons();
        }


        // Metoda obsługująca event kliknięcia przycisków wyboru trudności
        private void HandleChooseDifficultyEvent(object? sender, DifficultyEventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.ShowEnterNicknameView();
            chosenDifficulty = e.DifficultyLevel;
        }


        // Metoda obsługująca event kliknięcia przycisku "Description" w głównym menu
        private void HandleShowDescriptionEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.ShowDescription();
        }


        // Metoda obsługująca event kliknięcia przycisku "Show Highscores" w głównym menu
        private void HandleShowHighScoresEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.ShowHighScores(highScores);
        }


        // Metoda obsługująca event kliknięcia przycisku "Exit" w głównym menu
        private void HandleExitGameEvent(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        // Metoda obsługująca event kliknięcia przycisku "Fullscreen" w głównym menu
        private void HandleFullScreenModeEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.ChangeWindowSize();
            _menuView.CreateMenuButtons();
        }


        // Metoda obsługująca event kliknięcia przycisku "Start" w głównym menu
        private void HandleStartGameEvent(object? sender, EventArgs e)
        {
            _menuView.ClearMenuView();
            _menuView.CreateDifficultyButtonsAndLabel();
        }
    }
}
