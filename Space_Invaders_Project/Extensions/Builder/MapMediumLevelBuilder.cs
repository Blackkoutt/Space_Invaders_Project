using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;
using System.Windows;
using System;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapMediumLevelBuilder : MapBuilder
    {
         private Player player;
         List<Barrier> barriers = new List<Barrier>();
         private List<IEnemy> enemies = new List<IEnemy>();
         private int enemyNumber =14;
         private int basicChances = 20;
         public List<IEnemy> CreateEnemies(int level)
        {
            Random random = new Random();
            enemyNumber+=level*3;
            if(enemyNumber>40)
                enemyNumber=40;
            for(int i=0; i<enemyNumber;i++)
            {
                IEnemy enemy = new Default_Enemy(new Point(0 ,0));
                if(Drawing(basicChances+(level*2),random))
                {
                    int numberOfDecoratorsToUse = random.Next(1, 4);
                    enemies.Add(Default_Enemy.enemyGenerator(new Point(0, 0), numberOfDecoratorsToUse));
                }
                else
                    enemies.Add(enemy);

            }
            return enemies;
        }

       public List<Barrier> GetBarrier()
       {
            barriers.Add(new Barrier());        
            return barriers;
        }         
         private bool Drawing(int chance, Random random)
        {
            int randomNumber = random.Next(1, 101);

            return randomNumber <= chance;
        }
    }
}
