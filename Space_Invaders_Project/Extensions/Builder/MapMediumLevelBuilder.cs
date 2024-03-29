﻿using Space_Invaders_Project.Models;
using Space_Invaders_Project.Models.Decorator;
using System.Collections.Generic;
using System.Windows;
using System;

namespace Space_Invaders_Project.Extensions.Builder
{
    public class MapMediumLevelBuilder : MapBuilder
    {
         List<DefenceBarrier> barriers = new List<DefenceBarrier>();
         private List<IEnemy> enemies = new List<IEnemy>();
         private int enemyNumber =6;
         private int basicChances = 20;
         public List<IEnemy> CreateEnemies(int level)
        {
            Random random = new Random();
            int enemyXposition = 0;
            enemyNumber +=1;
            if(enemyNumber>20)
                enemyNumber=20;
            for(int i=0; i<enemyNumber;i++)
            {
                if(Drawing(basicChances+(level*2),random))
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
            barriers.Add(new DefenceBarrier());        
            return barriers;
        }         
         private bool Drawing(int chance, Random random)
        {
            int randomNumber = random.Next(1, 101);

            return randomNumber <= chance;
        }
    }
}
