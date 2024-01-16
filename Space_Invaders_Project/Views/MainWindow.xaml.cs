using Space_Invaders_Project.Controllers;
using Space_Invaders_Project.Extensions.Observer;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Canvas mainCanvas;
        public MainWindow()
        {
            InitializeComponent();
            MenuView menuView = new MenuView(this);
            new MenuController(menuView);
            //setUpCanvas();
        }
        public static Canvas MainCanvas
        {
            get { return mainCanvas; }
            set { mainCanvas = value; }
        }
    }
}
