using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapHardLevelBuilder : MapBuilder
    {
        public List<IEnemy> CreateEnemies(int level)
        {
            throw new System.NotImplementedException();
        }

        public List<Barrier> GetBarrier()
        {
            throw new System.NotImplementedException();
        }

        public Player GetPlayer()
        {
            throw new System.NotImplementedException();
        }
    }
}
