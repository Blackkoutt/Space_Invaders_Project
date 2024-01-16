using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Invaders_Project.Views
{
    public static class MapView
    {
        public static void RemoveNotification()
        {
            foreach (Label label in MainWindow.MainCanvas.Children.OfType<Label>())
            {
                if (label.Tag == "notification")
                {
                    MainWindow.MainCanvas.Children.Remove(label);
                    break;
                }
            }
            
        }
    }
}
