using Space_Invaders_Project.Models;
using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Builder
{
    // Jeśli niezależnie od poziomu trudności właściwości playera się nie zmieniają to lepiej dać klasę abstrakcyjną
    public interface MapBuilder
    {
        List<Enemy> CreateEnemies(int level);
        Player GetPlayer();
        List<Barrier> GetBarrier();
    }
}
