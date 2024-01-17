using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapEasyLevelBuilder : MapBuilder
    {
        public List<IEnemy> CreateEnemies(int level)
        {
            throw new System.NotImplementedException();
        }

        // Utworzenie danej ilości barier i ustawienie ich pozycji
        public List<Barrier> GetBarrier()
        {
            throw new System.NotImplementedException();
        }

        public Player GetPlayer()
        {
            // pobranie instancji playera ustawienie pozycji i zwrócenie 
            throw new System.NotImplementedException();
        }
    }
}
