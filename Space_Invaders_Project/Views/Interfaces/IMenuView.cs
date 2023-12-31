using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Invaders_Project.Views.Interfaces
{
    public interface IMenuView
    {
        event EventHandler StartGameEvent;
        event EventHandler ShowHighScoresEvent;
        event EventHandler ExitGameEvent;
        event EventHandler FullScreenModeEvent;

        void ChangeWindowSize();
        void ClearMenuButtons();
        void CreateMenuButtons();
    }
}
