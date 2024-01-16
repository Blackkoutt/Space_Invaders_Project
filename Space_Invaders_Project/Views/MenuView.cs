using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Invaders_Project.Views
{
    public class MenuView : IMenuView
    {
        private MainWindow _mainWindow;
        private Canvas canvas;
        private Button startGameButton;
        private Button highScoresButton;
        private Button exitGameButton;
        private Button fullScreenButton;

        public event EventHandler StartGameEvent;
        public event EventHandler ShowHighScoresEvent;
        public event EventHandler ExitGameEvent;
        public event EventHandler FullScreenModeEvent;

        public MenuView(MainWindow mainWindow) 
        {
            _mainWindow = mainWindow;
            canvas = _mainWindow.MainCanvas;
            _mainWindow.ResizeMode = ResizeMode.NoResize;
            _mainWindow.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/background.jpg")) };
            setUpCanvas();
            CreateMenuButtons();
        }
        private void setUpCanvas()
        {
            canvas = new Canvas();
            canvas.Height = 500;
            canvas.Width = 800;
            canvas.Background = new SolidColorBrush(Colors.Transparent);
            _mainWindow.Content = canvas;
        }
        private Button CreateButton(double left, double top, string content)
        {
            Button button = new Button {
                Height = 40,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Content = content
            };
            Canvas.SetLeft(button, left);
            Canvas.SetTop(button, top);
            canvas.Children.Add(button);
            return button;
        }
        public void CreateMenuButtons()
        {
            double height = canvas.Height;
            double width = canvas.Width;

            startGameButton = CreateButton(width / 2 - 50, height / 2 - 65, "Start");
            highScoresButton = CreateButton(width / 2 - 50, height / 2, "High scores");
            if (_mainWindow.WindowState == WindowState.Maximized)
            {
                fullScreenButton = CreateButton(width / 2 - 50, height / 2 + 65, "Normal size");
            }
            else
            {
                fullScreenButton = CreateButton(width / 2 - 50, height / 2 + 65, "Fullscreen");
            }
            exitGameButton = CreateButton(width / 2 - 50, height / 2 + 130, "Exit");

            // obsługa eventów oddelegowana do MenuController
            startGameButton.Click += delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            highScoresButton.Click += delegate { ShowHighScoresEvent?.Invoke(this, EventArgs.Empty); };
            exitGameButton.Click += delegate { ExitGameEvent?.Invoke(this, EventArgs.Empty); };
            fullScreenButton.Click += delegate { FullScreenModeEvent?.Invoke(this, EventArgs.Empty); };
        }
        public void ClearMenuButtons()
        {
            canvas.Children.Clear();
            startGameButton.Click -= delegate { StartGameEvent?.Invoke(this, EventArgs.Empty); };
            highScoresButton.Click -= delegate { ShowHighScoresEvent?.Invoke(this, EventArgs.Empty); };
            exitGameButton.Click -= delegate { ExitGameEvent?.Invoke(this, EventArgs.Empty); };
            fullScreenButton.Click -= delegate { FullScreenModeEvent?.Invoke(this, EventArgs.Empty); };
        }
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
    }
}
