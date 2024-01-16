using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Space_Invaders_Project.Models;
using System.Windows.Input;

namespace Space_Invaders_Project.Views
{
    public class KeyEventArgsWrapper : EventArgs
    {
        public Key Key { get; }

        public KeyEventArgsWrapper(Key key)
        {
            this.Key = key;
        }
    }
    public class MapView : IMapView
    {
        private MainWindow mainWindow;
        private Canvas canvas;

        public event EventHandler<KeyEventArgsWrapper> KeyDownEvent;
        public event EventHandler<KeyEventArgsWrapper> KeyUpEvent;

        public MapView(MainWindow mainWindow, Canvas canvas) 
        {
            this.canvas = canvas;
            this.mainWindow = mainWindow;
        }
        public void AddNotification(Label label)
        {
            Canvas.SetTop(label, 10);
            Canvas.SetLeft(label, 10);
            canvas.Children.Add(label);
        }


        // Getter do Canvasu
        public Canvas getCanvas()
        {
            return canvas;
        }


        // Metoda pobierająca rozmiar Canvasu
        public Size GetWindowSize()
        {
            return new Size((int)canvas.Width,(int)canvas.Height);
        }


        // Metoda przygotowująca mapę
        public void PrepareMap(Player player)
        {
            canvas.Background = new SolidColorBrush(Colors.Black);
            Label scoreLabel = CreateLabel($"Score: {0}");
            Canvas.SetLeft(scoreLabel, 0);
            Canvas.SetTop(scoreLabel, 0);
            canvas.Children.Add(scoreLabel);

            DrawEntity(player.Model, player.Position);
            canvas.Children.Add(player.Model);
            mainWindow.KeyDown += (sender, e) =>
            {
                KeyDownEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };

            mainWindow.KeyUp += (sender, e) =>
            {
                KeyUpEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };
        }
        public void RemoveNotification()
        {
            foreach (Label label in canvas.Children.OfType<Label>())
            {
                if (label.Tag == "notification")
                {
                    canvas.Children.Remove(label);
                    break;
                }
            }
        }


            // Metoda spawnująca model pocisku na mapie
            public void SpawnMissileModel(Rectangle model, Point position)
        {
            DrawEntity(model, position);
            canvas.Children.Add(model);
        }


        // Główna metoda rysująca - wywoływana głównie podczas przesuwania obiektów (w każdym takcie zegara)
        public void DrawEntity(Rectangle model, Point position)
        {
            Canvas.SetLeft(model, position.X);
            
            Canvas.SetTop(model, position.Y);           
        }


        // Metoda tworząca label Score
        private Label CreateLabel(string content)
        {
           return new Label() 
           {
               Content = content,
               Foreground = Brushes.White,
               FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P")
           };
        }
    }
}
