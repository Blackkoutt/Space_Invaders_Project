using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Space_Invaders_Project.Models;
using System.Windows.Input;
using System.Linq;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

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


        // Metoda dodająca w wiodoku mapy powiadomienie o pokonaniu danego gracza
        public void AddNotification(Label label)
        {
            Canvas.SetTop(label, 10);
            Canvas.SetLeft(label, 10);
            canvas.Children.Add(label);
        }


        // Metoda aktualizująca wynik gracza na mapie
        public void UpdateScoreLabel(int score)
        {
            foreach (Label label in canvas.Children.OfType<Label>()) 
            {
                if (label.Tag.ToString() == "Score")
                {
                    label.Content = "Score: " + score.ToString();
                    return;
                }
            }
        }
        // Metoda aktualizująca level gracza
         public void UpdateLevelLabel(int level)
        {
            foreach (Label label in canvas.Children.OfType<Label>()) 
            {
                if (label.Tag.ToString() == "Level")
                {
                    label.Content = "Level: " + level.ToString();
                    return;
                }
            }
        }
        // Metoda aktualizująca zycie gracza
         public void UpdateHealthLabel(int health)
        {
            foreach (Label label in canvas.Children.OfType<Label>()) 
            {
                if (label.Tag.ToString() == "Health")
                {
                    label.Content = "Health: " + health.ToString();
                    return;
                }
            }
        }
        public void UpdateBonusLabel(string bonusName)
        {
            foreach (Label label in canvas.Children.OfType<Label>()) 
            {
                if (label.Tag.ToString() == "Bonus")
                {
                    label.Content = "Bonus: " + bonusName;
                    return;
                }
            }
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


        // Metoda przygotowująca mapę na kolejny poziom
        public void PrepareNextLevel(List<IEnemy> enemies)
        {
            foreach (IEnemy enemy in enemies)
            {
                DrawEnemy(enemy);
                canvas.Children.Add(enemy.ArmModel);
                canvas.Children.Add(enemy.BodyModel);
                canvas.Children.Add(enemy.LegModel);
            }
        }


        // Metoda przygotowująca mapę przy inicjalizacji gry
        public void PrepareMap(Player player, List<IEnemy> enemies)
        {
            canvas.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/background_game.jpg")) };
            
            // Narysuj label z wynikiem
            Label scoreLabel = CreateLabel($"Score: {0}", "Score");
            Canvas.SetLeft(scoreLabel, 0);
            Canvas.SetTop(scoreLabel, 0);
            canvas.Children.Add(scoreLabel);

            Label healthLabel = CreateLabel($"Health: {3}", "Health");
            Canvas.SetRight(healthLabel, 0);
            Canvas.SetTop(healthLabel, 0);
            canvas.Children.Add(healthLabel);

            Label levelLabel = CreateLabel($"Level: {1}", "Level");
            Canvas.SetLeft(levelLabel, 450);
            Canvas.SetTop(levelLabel, 0);
            canvas.Children.Add(levelLabel);

            Label bonusLabel = CreateLabel($"Bonus: ", "Bonus");
            Canvas.SetLeft(bonusLabel, 0);
            Canvas.SetTop(bonusLabel, 30);
            canvas.Children.Add(bonusLabel);

            // Narysuj gracza
            DrawEntity(player.Model, player.Position);
            canvas.Children.Add(player.Model);

            // Narysuj przeciwników
            foreach (IEnemy enemy in enemies)
            {
                DrawEnemy(enemy);
                canvas.Children.Add(enemy.ArmModel);
                canvas.Children.Add(enemy.BodyModel);
                canvas.Children.Add(enemy.LegModel);
            }

            // Podepnij zdarzenia do mainWindow (przycisk wciśnięty i przycisk puszczony) - obsługa w Game_Controller
            mainWindow.KeyDown += (sender, e) =>
            {
                KeyDownEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };

            mainWindow.KeyUp += (sender, e) =>
            {
                KeyUpEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };
        }
         public void PrepareMap(Player player, List<IEnemy> enemies, List<DefenceBarrier> barriers)
        {
            canvas.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/background_game.jpg")) };
            
            // Narysuj label z wynikiem
            Label scoreLabel = CreateLabel($"Score: {0}", "Score");
            Canvas.SetLeft(scoreLabel, 0);
            Canvas.SetTop(scoreLabel, 0);
            canvas.Children.Add(scoreLabel);

            //Narysuj label z życiem
            Label healthLabel = CreateLabel($"Health: {3}", "Health");
            Canvas.SetRight(healthLabel, 20);
            Canvas.SetTop(healthLabel, 0);
            canvas.Children.Add(healthLabel);

            //Narysuj labale z levelem

            Label levelLabel = CreateLabel($"Level: {1}", "Level");
            Canvas.SetLeft(levelLabel, 450);
            Canvas.SetTop(levelLabel, 0);
            canvas.Children.Add(levelLabel);

            Label bonusLabel = CreateLabel($"Bonus:  ", "Bonus");
            Canvas.SetLeft(bonusLabel, 0);
            Canvas.SetTop(bonusLabel, 30);
            canvas.Children.Add(bonusLabel);

            // Narysuj gracza
            DrawEntity(player.Model, player.Position);
            canvas.Children.Add(player.Model);

            //Narysuj bariery
            foreach(DefenceBarrier barrier in barriers)
            {
            DrawEntity(barrier.Model, barrier.Position);
            canvas.Children.Add(barrier.Model);
            }
            // Narysuj przeciwników
            foreach (IEnemy enemy in enemies)
            {
                DrawEnemy(enemy);
                canvas.Children.Add(enemy.ArmModel);
                canvas.Children.Add(enemy.BodyModel);
                canvas.Children.Add(enemy.LegModel);
            }

            // Podepnij zdarzenia do mainWindow (przycisk wciśnięty i przycisk puszczony) - obsługa w Game_Controller
            mainWindow.KeyDown += (sender, e) =>
            {
                KeyDownEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };

            mainWindow.KeyUp += (sender, e) =>
            {
                KeyUpEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };
        }


        // Metoda rysująca pojedynczego przeciwnika na mapie
        public void DrawEnemy(IEnemy enemy)
        {
            SetEnemyVisibility(enemy);
            DrawEntity(enemy.ArmModel, enemy.Position);
            DrawEntity(enemy.BodyModel, enemy.Position);
            DrawEntity(enemy.LegModel, enemy.Position);
        }

        public void DrawBarrier(DefenceBarrier barrier)
        {
            DrawEntity(barrier.Model, barrier.Position);
            canvas.Children.Add(barrier.Model);

        }


        // Metoda ustawiająca widoczność przeciwnika
        private void SetEnemyVisibility(IEnemy enemy)
        {
            if (enemy.Position.X < 0)
            {
                enemy.ArmModel.Visibility = Visibility.Hidden;
                enemy.BodyModel.Visibility = Visibility.Hidden;
                enemy.LegModel.Visibility = Visibility.Hidden;
            }
            else
            {
                enemy.ArmModel.Visibility = Visibility.Visible;
                enemy.BodyModel.Visibility = Visibility.Visible;
                enemy.LegModel.Visibility = Visibility.Visible;
            }
        }


        // Metoda usuwająca wszystkie elementy mapy i eventy poruszania się gracza
        public void RemoveAllEntitiesAndEvents()
        {
            // Odepnij zdarzenia od mainWindow
            mainWindow.KeyDown -= (sender, e) =>
            {
                KeyDownEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };

            mainWindow.KeyUp -= (sender, e) =>
            {
                KeyUpEvent?.Invoke(this, new KeyEventArgsWrapper(e.Key));
            };

            // Wyczyść widok
            canvas.Children.Clear();
        }


        // Metoda usuwająca z widoku powiadomienie o pobiciu wyniku danego gracza
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

        public void SpawnBonusModel(Rectangle model, Point position)
        {
            DrawEntity(model, position);
            canvas.Children.Add(model);
        }



        // Główna metoda rysująca - wywoływana głównie podczas przesuwania obiektów (w każdym takcie zegara gameTimer w Game_Controller)
        public void DrawEntity(Rectangle model, Point position)
        {
            Canvas.SetLeft(model, position.X);
            
            Canvas.SetTop(model, position.Y);           
        }


        // Metoda usuwająca pojedynczy element mapy 
        public void RemoveEntity(Rectangle model)
        {
            canvas.Children.Remove(model);
        }


        // Metoda tworząca label Score
        private Label CreateLabel(string content, string tag)
        {
           return new Label() 
           {
               Content = content,
               Foreground = Brushes.White,
               FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
               Tag = tag
           };
        }


        // Metoda rysująca widok pauzy gry
        public void drawPauseOverlay()
        {
            TextBlock bcgk = new TextBlock()
            {
                Tag = "pause",
                Width = canvas.Width,
                Height = canvas.Height,
                Text = "     Game paused",
                Opacity = 0.7,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Foreground = Brushes.Green,
                Background = Brushes.Black,
                FontSize = 50,
            };
            Canvas.SetTop(bcgk, 0);
            Canvas.SetLeft(bcgk, 0);
            canvas.Children.Add(bcgk);
        }


        // Metoda wznawiająca grę po zapauzowaniu
        public void erasePauseOverlay()
        {
            foreach (TextBlock item in canvas.Children.OfType<TextBlock>())
            {
                if (item.Tag == "pause")
                {
                    canvas.Children.Remove(item);
                    return;
                }
            }
        }
    }
}
