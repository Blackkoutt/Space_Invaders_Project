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
        private Button returnToMenuButton;

        public event EventHandler StartGameEvent;
        public event EventHandler ShowHighScoresEvent;
        public event EventHandler ExitGameEvent;
        public event EventHandler FullScreenModeEvent;
        public event EventHandler ReturnToMenuEvent;

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
        public static void TestEnemy(MenuView mw)
        {
            IEnemy enemy = new Default_Enemy(new System.Drawing.Point(20, 20));
            //IEnemy enemy2 = new Default_Enemy(new System.Drawing.Point(40, 10));
            //IEnemy enemy3 = new Default_Enemy(new System.Drawing.Point(80, 10));

            IEnemy enemyDmg = new DamageEnemyDecorator(enemy);
            IEnemy enemyDmgHp = new HealthEnemyDecorator(enemyDmg);
            foreach (Rectangle r in enemyDmg.getModel())
            {
                mw.canvas.Children.Add(r);
            }
            enemyDmg.drawEnemy(mw.canvas);
            //enemy2.drawEnemy(mw.canvas);
            //enemy3.drawEnemy(mw.canvas);
        }
    }
}
