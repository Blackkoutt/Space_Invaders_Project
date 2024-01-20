using Space_Invaders_Project.Controllers;
using System;
using System.Windows;

namespace Space_Invaders_Project.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HighScores.ReadFromFile();
            MenuView menuView = new MenuView(this);
            new MenuController(menuView);          
        }
    }
}
