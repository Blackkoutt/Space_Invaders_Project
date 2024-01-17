using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Builder
{
    // Jeśli niezależnie od poziomu trudności właściwości playera się nie zmieniają to lepiej dać klasę abstrakcyjną
    public interface MapBuilder
    {
        List<IEnemy> CreateEnemies(int level);
        List<Barrier> GetBarrier();
    }
}
