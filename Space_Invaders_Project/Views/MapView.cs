using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Invaders_Project.Views
{
    public class MapView
    {
        private MainWindow _mainWindow;

        public MapView(MainWindow mainWindow) 
        {
            _mainWindow = mainWindow;
        }

        public void AddNotification(Label label)
        {
            Canvas.SetTop(label, 10);
            Canvas.SetLeft(label, 10);
            _mainWindow.MainCanvas.Children.Add(label);
        }
        public void RemoveNotification()
        {
            foreach (Label label in _mainWindow.MainCanvas.Children.OfType<Label>())
            {
                if (label.Tag == "notification")
                {
                    _mainWindow.MainCanvas.Children.Remove(label);
                    break;
                }
            }
            
        }
    }
}
