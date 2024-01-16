﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space_Invaders_Project.Models;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Views.Interfaces
{
    public interface IMapView
    {
        event EventHandler<KeyEventArgsWrapper> KeyDownEvent;
        event EventHandler<KeyEventArgsWrapper> KeyUpEvent;
        Canvas getCanvas();
        void DrawEntity(Rectangle model, Point position);
        Size GetWindowSize();
        void PrepareMap(Player player);
        void SpawnMissileModel(Rectangle model, Point position);
        void AddNotification(Label label);
        void RemoveNotification();
    }
}
