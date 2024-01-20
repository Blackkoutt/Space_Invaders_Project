using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;

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
    public class PlayerNicknameEventArgs : EventArgs
    {
        public string PlayerNickname { get; }

        public PlayerNicknameEventArgs(string playerNickname)
        {
            PlayerNickname = playerNickname;
        }
    }

    public class MenuView : IMenuView
    {
        private MainWindow _mainWindow;
        private Canvas canvas;

        public event EventHandler StartGameEvent;
        public event EventHandler ShowDescriptionEvent;
        public event EventHandler ShowHighScoresEvent;
        public event EventHandler ExitGameEvent;
        public event EventHandler FullScreenModeEvent;
        public event EventHandler ReturnToMenuEvent;
        public event EventHandler ReturnToDifficultyEvent;
        public event EventHandler<PlayerNicknameEventArgs> PlayGameEvent;
        public event EventHandler<DifficultyEventArgs> ChooseDifficultyEvent;

        private double width;
        private double height;

        public MenuView(MainWindow mainWindow) 
        {
            _mainWindow = mainWindow;
            setUpCanvas(); // Inicjaizacja canvasu
            width = canvas.Width;
            height = canvas.Height;
            _mainWindow.ResizeMode = ResizeMode.NoResize;
            _mainWindow.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/background.jpg")) };
            _mainWindow.Background.Opacity = 0.5;
            
            CreateMenuButtons();    // Utworzenie przycisków Menu
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
            canvas.Width = 1000;
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
                Foreground = Brushes.LimeGreen,
                Background = Brushes.Transparent,
                FontSize = 20,
                BorderThickness = new Thickness(0),
            };
            Canvas.SetLeft(button, left - button.Width/2);
            Canvas.SetTop(button, top);
            canvas.Children.Add(button);
            return button;
        }


        // Metoda tworząca label
        private void CreateDefaultLabel(string tag, int height, int width, int fontSize, string content, int top, Brush brush)
        {
            Label label = new Label()
            {
                Tag = tag,
                Height = height,
                Width = width,
                FontSize = fontSize,
                Foreground = brush,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Content = content

            };
            Canvas.SetLeft(label, this.width / 2 - label.Width / 2);
            Canvas.SetTop(label, this.height / 2 - top);
            canvas.Children.Add(label);
        }


        // Metoda pokazująca label zawierający błąd - złe podany nick
        public void ShowNicknameError(string variant)
        {
            Label labelToRemove = canvas.Children
            .OfType<Label>()
            .FirstOrDefault(item => (item.Tag == "error"));

            if (labelToRemove != null)
            {
                canvas.Children.Remove(labelToRemove);
            }
            switch (variant)
            {
                case "min_width":
                    {
                        CreateDefaultLabel("error", 45, 500, 10, "Nickname must have min 1 character", 50, Brushes.Red);
                        break;
                    }
                case "exist":
                    {
                        CreateDefaultLabel("error", 45, 500, 10, "Such nickname already exists in Highscore table", 50, Brushes.Red);
                        break;
                    }
            }        
        }


        // Metoda ładująca widok GameOver
        public void LoadGameOverScreen(int score)
        {
            canvas.Background = new SolidColorBrush(Colors.Transparent);

            CreateDefaultLabel("game_over", 65, 500, 50, "Game Over!", 200, Brushes.YellowGreen);
            CreateDefaultLabel("score", 45, 210, 25, $"Score: {score}", 120, Brushes.YellowGreen);

            Button playAgainButton = CreateButton(width / 2, height / 2 - 10, "Play Again!");
            Button returnToMenuButton = CreateButton(width / 2, height / 2 + 180, "Return");

            // Obsługa przycisków oddelegowana do MenuController poprzez interfejs MenuView
            returnToMenuButton.Click += delegate { ReturnToMenuEvent?.Invoke(this, EventArgs.Empty); };
            playAgainButton.Click += delegate { ReturnToDifficultyEvent?.Invoke(this, EventArgs.Empty); };            
        }


        // Metoda pokazująca widok wpisywania nicku gracza
        public void ShowEnterNicknameView()
        {
            CreateDefaultLabel("nickname", 45, 500, 25, "Enter your nickname: ", 150, Brushes.YellowGreen);
            TextBox textBox = new TextBox() 
            {
                Width = 400,
                Height = 40,
                FontSize = 25,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"),
                "./Fonts/#Press Start 2P"),
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(textBox, height / 3);
            Canvas.SetLeft(textBox, width / 2 - textBox.Width/2);
            canvas.Children.Add(textBox);

            Button playButton = CreateButton(width / 2, height / 2 - 10, "Play!");
            Button returnToDifficulty = CreateButton(width / 2, height / 2 + 180, "Return");

            // Obsługa przycisków oddelegowana do MenuController poprzez interfejs MenuView
            returnToDifficulty.Click += delegate { ReturnToDifficultyEvent?.Invoke(this, EventArgs.Empty); };
            playButton.Click += delegate { PlayGameEvent?.Invoke(this, new PlayerNicknameEventArgs(textBox.Text)); };
        }


        // Metoda tworząca przyciski wyboru poziomu trudności i przypisująca eventy
        public void CreateDifficultyButtonsAndLabel()
        {
            CreateDefaultLabel("difficulty", 45, 410, 25, "Choose Difficulty", 160, Brushes.YellowGreen);

            Button easyLevelButton = CreateButton(width / 2, height / 2 - 110, "Easy");
            Button mediumLevelButton = CreateButton(width / 2, height / 2 - 45, "Medium");
            Button hardLevelButton = CreateButton(width / 2, height / 2 + 20, "Hard");
            Button returnToMenuButton = CreateButton(width / 2, height / 2 + 160, "Return");

            // Obsługa przycisków oddelegowana do MenuController poprzez interfejs MenuView
            easyLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(0)); };
            mediumLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(1)); };
            hardLevelButton.Click += delegate { ChooseDifficultyEvent?.Invoke(this, new DifficultyEventArgs(2)); };
            returnToMenuButton.Click += delegate { ReturnToMenuEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda tworząca przyciski wyboru w menu i przypisująca eventy
        public void CreateMenuButtons()
        {
            Button startGameButton = CreateButton(width / 2, height / 2 - 130, "Start");
            Button descriptionButton = CreateButton(width / 2, height / 2 - 65, "Description");
            Button highScoresButton = CreateButton(width / 2, height / 2, "High scores");
            Button fullScreenButton;
            Button exitGameButton = CreateButton(width / 2, height / 2 + 130, "Exit");

            if (_mainWindow.WindowState == WindowState.Maximized)
            {
                fullScreenButton = CreateButton(width / 2, height / 2 + 65, "Normal size");
            }
            else
            {
                fullScreenButton = CreateButton(width / 2, height / 2 + 65, "Fullscreen");
            }      

            // Obsługa przycisków oddelegowana do MenuController poprzez interfejs MenuView
            startGameButton.Click += delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            descriptionButton.Click += delegate { ShowDescriptionEvent?.Invoke(this, EventArgs.Empty); };
            highScoresButton.Click += delegate { ShowHighScoresEvent?.Invoke(this, EventArgs.Empty); };
            exitGameButton.Click += delegate { ExitGameEvent?.Invoke(this, EventArgs.Empty); };
            fullScreenButton.Click += delegate { FullScreenModeEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda wyświetlająca widok opisu gry
        public void ShowDescription()
        {
            _mainWindow.Background.Opacity = 0.3;
            CreateDescriptionLabel(8, 30);
            Button returnToMenuButton = CreateButton(width - 150, height - 150, "Return");

            // Obsługa przycisków oddelegowana do MenuController poprzez interfejs MenuView
            returnToMenuButton.Click += delegate { ReturnToMenuEvent?.Invoke(this, EventArgs.Empty); };
        }


        // Metoda usuwająca wszytskie elementy widoku (będące na canvasie)
        public void ClearMenuView()
        {
            canvas.Children.Clear();
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


        // Metoda pokazująca widok tabeli wyników 
        public void ShowHighScores(HighScores highScores)
        {
            List<string> nickList = highScores.NickList;
            List<int> scoreList = highScores.ScoresList;

            _mainWindow.Background.Opacity = 0.3;
            Label titleLabel = new Label() { Content = "Top 10 highscores:", Foreground = Brushes.YellowGreen};
            Canvas.SetTop(titleLabel, 10);
            Canvas.SetLeft(titleLabel, canvas.Width / 2 - 135);
            canvas.Children.Add(titleLabel);

            for (int i = 0; i < 10; i++)
            {
                string formatterText = String.Format("#{0} - Nick: {1} - Score: {2}", i + 1, nickList[i], scoreList[i]);
                Label newScore = new Label() { Content = formatterText, Foreground = Brushes.White, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
                Canvas.SetTop(newScore, 40 * (i + 1) + 20);
                Canvas.SetLeft(newScore, canvas.Width / 2 - 160);
                canvas.Children.Add(newScore);
            }
            Button returnToMenuButton = CreateButton(canvas.Width - 150, canvas.Height - 80, "Return");

            // Obsługa przycików oddelegowana do MenuController poprzez interfejs MenuView
            returnToMenuButton.Click += delegate { ReturnToMenuEvent?.Invoke(this, EventArgs.Empty); };
        }
    }
}
