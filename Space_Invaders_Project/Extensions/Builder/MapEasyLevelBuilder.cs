using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;
using System.Windows;
using System;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapEasyLevelBuilder : MapBuilder
    {
        private List<DefenceBarrier> barriers ;
        private List<IEnemy> enemies;
        private int enemyNumber ;
        private int basicChances ;
        public List<IEnemy> CreateEnemies(int level)
        {
            enemyNumber = 5;
            basicChances = 10;
            enemies = new List<IEnemy>();
            Random random = new Random();
            int enemyXposition = 0;
            if(level %2 == 0)
                enemyNumber+=1;
            if(enemyNumber>10)
                enemyNumber=15;
            for(int i=0; i<enemyNumber;i++)
            {
                if(Drawing(basicChances+level,random))
                {
                    int numberOfDecoratorsToUse = random.Next(1, 4);
                    enemies.Add(Default_Enemy.enemyGenerator(new Point(enemyXposition, 30), numberOfDecoratorsToUse));
                }
                else
                    enemies.Add(Default_Enemy.enemyGenerator(new Point(enemyXposition, 30), 0));
                enemyXposition -= 60;
            }
            return enemies;
        }

        public List<DefenceBarrier> GetBarrier()
        {
            barriers = new List<DefenceBarrier>();
            for(int i=0; i<3; i++)
            {
                barriers.Add(new DefenceBarrier());  
            }     
            return barriers;
        }

        private bool Drawing(int chance, Random random)
        {
            int randomNumber = random.Next(1, 101);

            return randomNumber <= chance;
        }
    }
}
