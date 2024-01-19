using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Models.Decorator;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Space_Invaders_Project.Views
{
    public class DifficultyEventArgs : EventArgs
    {
        public byte DifficultyLevel { get; }

        public DifficultyEventArgs(byte difficultyLevel)
        {
            DifficultyLevel = difficultyLevel;
        }
    }

    public class MenuView : IMenuView
    {
        private MainWindow _mainWindow;
        private Canvas canvas;
        private Button startGameButton;
        private Button descriptionButton;
        private Button highScoresButton;
        private Button exitGameButton;
        private Button fullScreenButton;
        private Button returnFromDescriptionButton;

        private Button easyLevelButton;
        private Button mediumLevelButton;
        private Button hardLevelButton;

        private Button returnToMenuButton;

        public event EventHandler StartGameEvent;
        public event EventHandler ShowDescriptionEvent;
        public event EventHandler ShowHighScoresEvent;
        public event EventHandler ExitGameEvent;
        public event EventHandler FullScreenModeEvent;
        public event EventHandler ReturnToMenuEvent;

        public event EventHandler<DifficultyEventArgs> ChooseDifficultyEvent;

        private double width;
        private double height;

        public event EventHandler ReturnFromDescriptionEvent;

        public MenuView(MainWindow mainWindow) 
        {
            _mainWindow = mainWindow;
            setUpCanvas();
            width = canvas.Width;
            height = canvas.Height;
            _mainWindow.ResizeMode = ResizeMode.NoResize;
            _mainWindow.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/background.jpg")) };
            _mainWindow.Background.Opacity = 0.5;
            
            CreateMenuButtons();
        }


        // Getter do głównego okna
        public MainWindow GetMainWindow()
        {
            return _mainWindow;
        }


        // Getter do Canvasu
        public Canvas GetCanvas()
        {
            return canvas;
        }


        // Inicjalizacja Canvasu
        private void setUpCanvas()
        {
            canvas = new Canvas();
            canvas.Height = 600;
            canvas.Width = 1000; //800
            canvas.Background = new SolidColorBrush(Colors.Transparent);
            _mainWindow.Content = canvas;
        }


        // Metoda tworząca przyciski
        private Button CreateButton(double left, double top, string content)
        {
            Button button = new Button {
                Height = 45,
                Width = 220,
                Content = content,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Foreground = Brushes.Green,
                Background = Brushes.Transparent,
                FontSize = 20,
                BorderThickness = new Thickness(0),
            };
            Canvas.SetLeft(button, left - button.Width/2);
            Canvas.SetTop(button, top);
            canvas.Children.Add(button);
            return button;
        }


        

        // Metoda tworząca przyciski wyboru poziomu trudności i przypisująca eventy
        public void CreateDifficultyButtons()
        {
            easyLevelButton = CreateButton(width / 2, height / 2 - 130, "Easy");
            mediumLevelButton = CreateButton(width / 2, height / 2 - 65, "Medium");
            hardLevelButton = CreateButton(width / 2, height / 2, "Hard");

            easyLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(0)); };
            mediumLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(1)); };
            hardLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(2)); };

        }


        // Metoda usuwająca przyciski wyboru poziomu trudności
        public void ClearDifficultyButtons()
        {
            canvas.Children.Clear();
            easyLevelButton.Click -= delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(0)); };
            mediumLevelButton.Click -= delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(1)); };
            hardLevelButton.Click -= delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(2)); };
        }


        // Metoda tworząca przyciski wyboru w menu i przypisująca eventy
        public void CreateMenuButtons()
        {

            startGameButton = CreateButton(width / 2, height / 2 - 130, "Start");
            descriptionButton = CreateButton(width / 2, height / 2 - 65, "Description");
            highScoresButton = CreateButton(width / 2, height / 2, "High scores");
            if (_mainWindow.WindowState == WindowState.Maximized)
            {
                fullScreenButton = CreateButton(width / 2, height / 2 + 65, "Normal size");
            }
            else
            {
                fullScreenButton = CreateButton(width / 2, height / 2 + 65, "Fullscreen");
            }
            exitGameButton = CreateButton(width / 2, height / 2 + 130, "Exit");

            // obsługa eventów oddelegowana do MenuController
            startGameButton.Click += delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            descriptionButton.Click += delegate { ShowDescriptionEvent?.Invoke(this, EventArgs.Empty); };
            highScoresButton.Click += delegate { ShowHighScoresEvent?.Invoke(this, EventArgs.Empty); };
            exitGameButton.Click += delegate { ExitGameEvent?.Invoke(this, EventArgs.Empty); };
            fullScreenButton.Click += delegate { FullScreenModeEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda usuwająca z widoku przyciski głównego menu
        public void ClearMenuButtons()
        {
            canvas.Children.Clear();
            startGameButton.Click -= delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            descriptionButton.Click -= delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            highScoresButton.Click -= delegate { ShowHighScoresEvent?.Invoke(this, EventArgs.Empty); };
            exitGameButton.Click -= delegate { ExitGameEvent?.Invoke(this, EventArgs.Empty); };
            fullScreenButton.Click -= delegate { FullScreenModeEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda wyświetlająca widok opisu gry
        public void ShowDescription()
        {
            _mainWindow.Background.Opacity = 0.3;
            CreateDescriptionLabel(8, 30);
            returnFromDescriptionButton = CreateButton(width - 80, height - 150, "Return");
            returnFromDescriptionButton.Click += delegate { ReturnFromDescriptionEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda usuwająca z widoku opis gry
        public void ClearDescription()
        {
            canvas.Children.Clear();
            returnFromDescriptionButton.Click -= delegate { ReturnFromDescriptionEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda tworząca opis gry (label)
        private void CreateDescriptionLabel(int left, int top)
        {   
            Label descriptionLabel = new Label()
            {
                Foreground = Brushes.YellowGreen,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Content = "Goal of the game is to beat aliens attacking player.\n\n" +
                "Game can't be beaten, aim is to gain as many points as possible.\n\n" +
                "Every defeated alien gives 1 point, every wave extra 10.\n\n" +
                "\n\n" +
                "Player moves using arrow keys nad shoots with spacebar.\n\n" +
                "\n\n" +
                "Aliens aslo shoot purple missiles, which should be avoided\n\n" +
                "as getting hit decreases players health points.\n\n" +
                "The game end when playrs healf goes down to 0,\n\n" +
                "or the invaders reach his ship."

            };
            Canvas.SetLeft(descriptionLabel, left);
            Canvas.SetTop(descriptionLabel, top);
            canvas.Children.Add(descriptionLabel);
        }


        // Metoda zmieniająca rozmiar okna
        public void ChangeWindowSize()
        {
            if(_mainWindow.WindowState == WindowState.Maximized)
            {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                _mainWindow.WindowState = WindowState.Maximized;
                _mainWindow.WindowStyle = WindowStyle.None;
            }
        }
        public void ShowHighScores()
        {
            _mainWindow.Background.Opacity = 0.3;
            Label titleLabel = new Label() { Content = "Top 10 highscores:", Foreground = Brushes.YellowGreen};
            Canvas.SetTop(titleLabel, 10);
            Canvas.SetLeft(titleLabel, canvas.Width / 2 - 135);
            canvas.Children.Add(titleLabel);

            for (int i = 0; i < 10; i++)
            {
                string formatterText = String.Format("#{0} - Nick: {1} - Score: {2}", i + 1, HighScores.Nicks[i], HighScores.Scores[i]);
                Label newScore = new Label() { Content = formatterText, Foreground = Brushes.White, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
                Canvas.SetTop(newScore, 40 * (i + 1) + 20);
                Canvas.SetLeft(newScore, canvas.Width / 2 - 160);
                canvas.Children.Add(newScore);
            }
            returnToMenuButton = CreateButton(canvas.Width - 150, canvas.Height - 80, "Return");
            returnToMenuButton.Click += delegate { ReturnToMenuEvent?.Invoke(this, EventArgs.Empty); };
        }
    }
}
