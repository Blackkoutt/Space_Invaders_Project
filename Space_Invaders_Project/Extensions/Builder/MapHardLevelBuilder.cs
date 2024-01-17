using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapHardLevelBuilder : MapBuilder
    {
         private Player player;
         private List<Enemy> enemies = new List<Enemy>();
         List<Barrier> barriers = new List<Barrier>();
         private int enemyNumber =19;
         private int basicChances = 40;
        public List<IEnemy> CreateEnemies(int level)
        {
            Random random = new Random();
            enemyNumber+=level*5;
            if(enemyNumber>50)
                enemyNumber=50;
            for(int i=0; i<enemyNumber;i++)
            {
                Enemy enemy = new Default_Enemy();
                if(Drawing(basicChances+(level*3),random))
                {
                    int randomEnemy = random.Next(1,4);
                    switch (randomEnemy)
                    {
                        case 1:
                        enemies.Add(new Red_Enemy(enemy) );
                            break;
                        case 2:
                         enemies.Add(new Blue_Enemy(enemy) );
                            break;
                        case 3:
                         enemies.Add(new Yellow_Enemy(enemy) );
                            break;
                        default:
                        enemies.Add(enemy);
                            break;
                    }
                }else
                {
                    enemies.Add(enemy);
                }
            }
            return enemies;
        }

        public List<Barrier> GetBarrier()
        {
           return barriers;
        }      
         private bool Drawing(int chance, Random random)
        {
            int randomNumber = random.Next(1, 101);

            return randomNumber <= chance;
        }
    }
}
