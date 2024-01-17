using Space_Invaders_Project.Models;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapEasyLevelBuilder : MapBuilder
    {
        private Player player;
        private List<Barrier> barriers = new List<Barrier>();
        private List<Enemy> enemies = new List<Enemy>();
        private int enemyNumber =9;
        private int basicChances = 10;
        public List<Enemy> CreateEnemies(int level)
        {
            Random random = new Random();
             enemyNumber+=level*2;
            if(enemyNumber>35)
                enemyNumber=35;
            for(int i=0; i<enemyNumber;i++)
            {
                Enemy enemy = new Default_Enemy();
                if(Drawing(basicChances+level,random))
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
                    enemies.Add(enemy);

            }
            return enemies;
        }

        // Utworzenie danej ilości barier i ustawienie ich pozycji
        public List<Barrier> GetBarrier()
        {
            for(int i=0; i<3; i++)
            {
                barriers.Add(new Barrier());  
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
