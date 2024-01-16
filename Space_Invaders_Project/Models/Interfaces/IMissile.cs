using System.Windows;
using System.Windows.Shapes;

namespace Space_Invaders_Project.Models.Interfaces
{
    public interface IMissile
    {
        Point Position { get; }
        int Damage { get; }
        void setPosition(float y);
        float Speed { get; }
        Rectangle Model { get; }
    }
}
