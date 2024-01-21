using System;
using System.Collections.Generic;
using System.Windows;
using Space_Invaders_Project.Models;
using System.Windows.Controls;
using System.Windows.Shapes;
using Space_Invaders_Project.Models.Decorator;

namespace Space_Invaders_Project.Views.Interfaces
{
    public interface IMapView
    {
        event EventHandler<KeyEventArgsWrapper> KeyDownEvent;
        event EventHandler<KeyEventArgsWrapper> KeyUpEvent;
        Canvas getCanvas();
        void DrawEntity(Rectangle model, Point position);
        Size GetWindowSize();
        void PrepareMap(Player player, List<IEnemy> enemies);
        void PrepareMap(Player player, List<IEnemy> enemies, List<DefenceBarrier> barriers);
        void DrawEnemy(IEnemy enemy);
        void DrawBarrier(DefenceBarrier barrier);
        void RemoveEntity(Rectangle model);
        void PrepareNextLevel(List<IEnemy> enemies);
        void SpawnMissileModel(Rectangle model, Point position);
        void SpawnBonusModel(Rectangle model, Point position);
        void AddNotification(Label label);
        void RemoveNotification();
        void RemoveAllEntitiesAndEvents();
        void drawPauseOverlay();
        void erasePauseOverlay();
        void UpdateScoreLabel(int score);
        void UpdateHealthLabel(int health);
        void UpdateLevelLabel(int level);
    }
}
