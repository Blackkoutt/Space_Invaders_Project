using Space_Invaders_Project.Controllers.Interfaces;
using Space_Invaders_Project.Models;
using Space_Invaders_Project.Extensions.Strategy;
using Space_Invaders_Project.Models.Decorator;
using Space_Invaders_Project.Models.Interfaces;
using Space_Invaders_Project.Views;
using Space_Invaders_Project.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;

namespace Space_Invaders_Project.Controllers
{
    public class Game_Controller
    {
        private IMapView _mapView;
        private IGame gameFacade;
        private Player player;
        private List<IEnemy> enemies;
        private List<DefenceBarrier> barriers;
        private List<IMissile> missiles;
        private List<Player_Bonus> bonuses;
        private List<Player_Bonus> activeBonuses = new List<Player_Bonus>();
        private List<Player_Bonus> bonusesToRemove = new List<Player_Bonus>();
        
        private string playerMoveDirection = "";
        private string enemyMoveDirection = "right";
        private int notificationTimer = 0;
        private bool gamePaused = false;
        private DispatcherTimer bulletTimer;
        private DispatcherTimer bonusTimer;

        public Game_Controller(Player player, IMapView mapView, List<IEnemy> enemies, List<DefenceBarrier> barriers, IGame game)
        {
            this.player = player;
            this._mapView = mapView;
            this.gameFacade = game;
            this.enemies = enemies;
            this.barriers = barriers;

            missiles = new List<IMissile>();
            bonuses = new List<Player_Bonus>();

            // Dodanie obsługi eventu timera gameTimer z Fasady
            game.GameLoopTimerEvent += GameLoop;

            // Dodanie obsługi wciśnięcia przycisków na klawiaturze
            mapView.KeyDownEvent += HandleKeyDownEvent;
            mapView.KeyUpEvent += HandleKeyUpEvent;

            // Dodanie timera do odliczania czasu między kolejnymi strzałami przeciwników
            bulletTimer = new DispatcherTimer();
            bulletTimer.Tick += RandomEnemyShotMissle;
            bulletTimer.Interval = TimeSpan.FromMilliseconds(1000);
            bulletTimer.Start();

            bonusTimer = new DispatcherTimer();
            bonusTimer.Tick +=  GenerateRandomBonus; // Generuje bonusy;
            bonusTimer.Interval = TimeSpan.FromMilliseconds(500);
            bonusTimer.Start();
        }


        // Gettery do list
        public List<IEnemy> Enemies 
        {
            get {  return enemies; }
            set { enemies = value; } 
        }
        public List<IMissile> Missiles
        {
            get { return missiles; }
            set { missiles = value; }
        }
         public List<Player_Bonus> Bonuses
        {
            get { return bonuses; }
            set { bonuses = value; }
        }
        public List<DefenceBarrier> Barriers
        {
            get { return barriers; }
            set { barriers = value; }
        }


        // Handler puszczenia przycisku 
        private void HandleKeyUpEvent(object? sender, KeyEventArgsWrapper e)
        {
            // Gracz się nie porusza
            if (e.Key == Key.Left || e.Key == Key.Right)
                playerMoveDirection = "";

            // Strzał gracza 
            if (e.Key == Key.Space && !gamePaused)
            {
                Player_Missile newMissile = player.shootMissile();
                missiles.Add(newMissile);
                _mapView.SpawnMissileModel(newMissile.model, newMissile.Position);
            }
        }


        // Handler wciśnięcia przycisku 
        private void HandleKeyDownEvent(object? sender, KeyEventArgsWrapper e)
        {
            // Pauza lub wznowienie gry
            if (e.Key == Key.Escape)
            {
                gamePaused = !gamePaused;
                if (gamePaused)
                {
                    _mapView.drawPauseOverlay();
                    bulletTimer.Stop();
                }
                else
                {
                    _mapView.erasePauseOverlay();
                    bulletTimer.Start();
                }
                    
            }

            // Ustawienie kierunku poruszania sie gracza
            if (e.Key == Key.Left)
                playerMoveDirection = "left";
            else if (e.Key == Key.Right)
                playerMoveDirection = "right";
        }


        // Główna pętla gry wykonywana przez timer z klasy Game
        private void GameLoop(object? sender, EventArgs e)
        {
            if (gamePaused)
                return;

            if (enemies.Count == 0)
                gameFacade.NextLevel(); // Następny poziom
            
            MoveEntities(); // Przesunięcie elementów na mapie
            CheckForCollisions();   // Wykrycie kolizji

            DrawEntities(); // Rysowanie elementów na mapie (po przesunięciu)
            UpdateOverlay();    // Zaktualizowanie paska z wynikiem
            IsBonuActive(); // Sprawdzanie czy bonusy sa dalej aktywne

            // Jeśli gracz zginął to oznacza to koniec gry
            if (player.IsDeath)
            {
                bulletTimer.Stop();
                bulletTimer.Tick -= RandomEnemyShotMissle;
                bonusTimer.Stop();
                bonusTimer.Tick -=  GenerateRandomBonus;
                gameFacade.GameOver();
            }
        }


        // Metoda generująca losową ilość pocisków przeciwników w każdym takcie zegara bulletTimer
        private void RandomEnemyShotMissle(object? sender, EventArgs e)
        {
            if (enemies.Any())
            {
                Random random = new Random();
                int howManyEnemiesShot = random.Next(1, enemies.Count + 1); // Ile przeciwników ma strzelić
                for (int i = 0; i < howManyEnemiesShot; i++)
                {
                    // Który przeciwnik ma strzelić
                    int wichEnemyWillShot = random.Next(0, enemies.Count);
                    Enemy_Missile newMissile = enemies[wichEnemyWillShot].shootMissile();
                    missiles.Add(newMissile);
                    _mapView.SpawnMissileModel(newMissile.model, newMissile.Position);
                }
            }  
        }


        // Metoda rysująca wszytskie postacie na mapie
        private void DrawEntities()
        {
            // Rysowanie gracza
            _mapView.DrawEntity(player.Model, player.Position);

            // Rysowanie pocisków
            foreach(IMissile m in missiles)
            {
                _mapView.DrawEntity(m.Model, m.Position);
            }

            // Rysowanie przeciwników
            foreach(IEnemy enemy in enemies)
            {
                _mapView.DrawEnemy(enemy);
            } 
            //Rysowanie bonusów
            foreach(Player_Bonus bonus in bonuses)
            {
                _mapView.DrawEntity(bonus.Model, bonus.Position);
            }
              
        }


        // Metoda przesuwająca gracza w zależności od wciśniętego wcześniej klawisza
        private void MovePlayer()
        {
            if (playerMoveDirection == "right" && player.Position.X + 75 < _mapView.getCanvas().Width)
            {
                player.setPosition((int)player.Position.X + 10, (int)player.Position.Y);
            }
            else if (playerMoveDirection == "left" && player.Position.X > 0)
            {
                player.setPosition((int)player.Position.X - 10, (int)player.Position.Y);
            }
        }


        // Metoda przesuwająca pocisk
        private void MoveMissile(IMissile missile)
        {
            missile.setPosition(missile.Speed);
        }


        // Metoda przesuwająca wszytskie obiekty znajdujące się na mapie
        private void MoveEntities()
        {
            MovePlayer();
            foreach(IMissile m in missiles)
            {
                MoveMissile(m);
            }
            if (enemies.Any())
            {
                CheckIfEnemyOnEdge();
                foreach (IEnemy e in enemies)
                {
                    MoveEnemy(e);
                }
            }
            
            if ( bonuses?.Any() == true)
            {
                foreach(Player_Bonus b in bonuses)
                {
                    b.setPosition();
                }
            }           
        }


        // Metoda przesuwająca danego przeciwnika
        private void MoveEnemy(IEnemy enemy)
        {
            if(enemyMoveDirection == "right")
            {
                enemy.SetPosition((int)enemy.Position.X + 4, (int)enemy.Position.Y);
            }
            else
            {
                enemy.SetPosition((int)enemy.Position.X - 4, (int)enemy.Position.Y);
            }
        }

        // Metoda sprawdzająca czy przecinwik znajduje się na krawędzi canvasu (jeśli tak to wszytskim przeciwnikom zmienia się kierunek
        // i są oni przesuwani o jeden "wiersz" w dół mapy)
        private void CheckIfEnemyOnEdge()
        {
            if (enemyMoveDirection == "right")
            {
                if (enemies[0].Position.X + 55 >= _mapView.GetWindowSize().Width)
                {
                    enemyMoveDirection = "left";
                    foreach (IEnemy e in enemies)
                    {
                        e.SetPosition((int)e.Position.X, (int)e.Position.Y + 20);
                    }
                }
            }
            else
            {
                if (enemies[enemies.Count-1].Position.X - 10 < 0)
                {
                    enemyMoveDirection = "right";
                    foreach (IEnemy e in enemies)
                    {
                        e.SetPosition((int)e.Position.X, (int)e.Position.Y + 20);
                    }
                }
            }
        }


        // Metoda wykrywająca kolizje 
        private void CheckForCollisions()
        {
            // Wykrycie kolizji pocisku gracza z przeciwnikiem
            foreach(IEnemy enemy in enemies)
            {
                foreach(IMissile missile in missiles)
                {
                    if(missile is Player_Missile && enemy.Hitbox.IntersectsWith(missile.Hitbox))
                    {
                        enemy.dealDamage(player.Damage);    // Przeciwnik otrzymuje damage                  
                        missiles.Remove(missile);   // Pocisk gracza jest usuwany
                        _mapView.RemoveEntity(missile.Model);
                        break;
                    }
                }

                // Wykrycie kolizji przeciwnika z graczem
                if (enemy.Hitbox.IntersectsWith(player.Hitbox))
                {
                    player.IsDeath = true; // Gracz od razy ginie 
                }


                // Przeciwnik zginął
                if (enemy.IsDead)
                {
                    player.addScore();  // Gracz dostaje punkt 
                    gameFacade.HighScores.Notification(player.Score);   // Powiadomienie subskrybentów o zmianie stanu

                    // Usunięcie przeciwnika
                    enemies.Remove(enemy);
                    _mapView.RemoveEntity(enemy.ArmModel);
                    _mapView.RemoveEntity(enemy.BodyModel);
                    _mapView.RemoveEntity(enemy.LegModel);
                    break;
                }
            }

            for (int i = 0; i < missiles.Count; i++)
            {
                IMissile? missile = missiles[i];
                // Wykrycie kolizji pocisku gracza z górną granicą canvasu
                if (missile is Player_Missile && missile.Position.Y < 0)
                {
                    missiles.Remove(missile);
                    _mapView.RemoveEntity(missile.Model);
                    break;
                }

                // Wykrycie kolizji pocisku przeciwika z dolną granicą canvasu
                if (missile is Enemy_Missile && missile.Position.Y > _mapView.GetWindowSize().Height)
                {
                    missiles.Remove(missile);
                    _mapView.RemoveEntity(missile.Model);
                    break;
                }

                // Wykrycie kolizji pocisku przeciwnika z graczem
                if (missile is Enemy_Missile && missile.Hitbox.IntersectsWith(player.Hitbox))
                {
                    player.dealDamage(missile.Damage); // Gracz dostaje damage zależny od damage posicku przeciwnika
                    
                    // Pocisk przeciwnika zostaje usunięty
                    missiles.Remove(missile);
                    _mapView.RemoveEntity(missile.Model);
                    break;
                } 
                foreach(Player_Bonus bonus in bonuses)
                {
                //Wykrycie kolizji pocisku z bonusem
                    if (missile is Player_Missile && missile.Hitbox.IntersectsWith(bonus.Hitbox))
                    {
                        player.addBonus(bonus);
                        bonus.Notify();
                        activeBonuses.Add(bonus);
                        missiles.Remove(missile);
                        _mapView.RemoveEntity(missile.Model);
                        bonuses.Remove(bonus);
                        _mapView.RemoveEntity(bonus.Model);
                        break;
                    }
                }                      
            }
                
            foreach(Player_Bonus bonus in bonuses)
            {
                foreach (IMissile missile in missiles){
                if (missile is Player_Missile && missile.Hitbox.IntersectsWith(bonus.Hitbox))
                    {
                        player.addBonus(bonus);
                        activeBonuses.Add(bonus);
                        missiles.Remove(missile);
                        _mapView.RemoveEntity(missile.Model);
                        bonusesToRemove.Add(bonus);
                        break;
                    }
                }
                //Bonus wychodzi poza mape
                if (bonus.Position.X < 0|| bonus.Position.X >_mapView.GetWindowSize().Width)
                {
                    bonuses.Remove(bonus);
                    _mapView.RemoveEntity(bonus.Model);
                    break;
                }      

            } 
            foreach (Player_Bonus bonusToRemove in bonusesToRemove)
            {
                bonuses.Remove(bonusToRemove);
                _mapView.RemoveEntity(bonusToRemove.Model);
                break;
            }        
                
            foreach(DefenceBarrier barrier in barriers)
            {    
                foreach (IMissile missile in missiles)
                {
                 //Wykrycie zderzenia pocisku przeciwnika z bariera
                    if (missile is Enemy_Missile && missile.Hitbox.IntersectsWith(barrier.Hitbox))
                    {
                        missiles.Remove(missile);
                        _mapView.RemoveEntity(missile.Model);
                        //barrier.setHealth();  
                        break;                     
                    }
                    //Wykrycie zderzenia pocisku gracza z bariera
                    if (missile is Player_Missile && missile.Hitbox.IntersectsWith(barrier.Hitbox))
                    {
                        missiles.Remove(missile);
                        _mapView.RemoveEntity(missile.Model); 
                        break; 
                    }     
                }                
            //Wykrycie zderzenia pocisku przeciwnika z bariera
                /*if(barrier.isDead())
                {
                    barriers.Remove(barrier);
                    _mapView.RemoveEntity(barrier.Model);
                    break;
                }else
                {
                    barrier.MapHealthToModel();
                    break;
                }*/
            }
                                                                                           
        }
        //Losowy generator do bonusów
        private void GenerateRandomBonus(object? sender, EventArgs e)
        {
            Random random = new Random();

            int chance = random.Next(0, 100); 

            if (chance < 5)
            {
            int x = random.Next((int)_mapView.getCanvas().Width);
            int minY = 250; 
            int maxY = (int)_mapView.getCanvas().Height;
            int y = random.Next(minY, maxY);

            Point position = new Point(x, y);
            int bonusIndex = random.Next(1, 5);
            Player_Bonus bonus ;

            switch (bonusIndex)
            {
                case 1:
                    bonus = player.setBonusStrategy(new DamageDealtBonus(position));
                    bonuses.Add(bonus);
                    _mapView.SpawnBonusModel(bonus.Model, bonus.Position);
                    break;
                case 2:
                    bonus = player.setBonusStrategy(new FastAttackSpeedBonus(position));
                    bonuses.Add(bonus);
                    _mapView.SpawnBonusModel(bonus.Model, bonus.Position);
                    break;
                case 3:
                    bonus = player.setBonusStrategy(new FastMissleBonus(position));
                    bonuses.Add(bonus);
                    _mapView.SpawnBonusModel(bonus.Model, bonus.Position);
                    break;
               /* case 4:
                    bonus = player.setBonusStrategy(new MovementSpeedBonus(position));
                    bonuses.Add(bonus);
                     _mapView.SpawnBonusModel(bonus.Model, bonus.Position);
                    break; */
                case 4:
                    bonus = player.setBonusStrategy(new ScoreMultiplier(position));
                    bonuses.Add(bonus);
                    _mapView.SpawnBonusModel(bonus.Model, bonus.Position);
                    break;
                default:
                    break;
            }
            }
        }
        private void IsBonuActive(){
            foreach (Player_Bonus bonus in activeBonuses.ToList())
        {
            if (!(bonus.IsActive()))
            {
                player.stopBonus(bonus);
                activeBonuses.Remove(bonus);
            }
        }
        }
            

        // Metoda aktualizująca górny pasek z wynikiem
        private void UpdateOverlay()
        {
            CheckIfNotificationToRemove();
            _mapView.UpdateScoreLabel(player.Score);
            _mapView.UpdateHealthLabel(player.Health);
        }


        // Metoda usuwająca powiadomienie o pobiciu wyniku danego gracza
        private void CheckIfNotificationToRemove()
        {
            if (notificationTimer == 0)
            {
                foreach (Label label in _mapView.getCanvas().Children.OfType<Label>())
                {
                    if (label.Tag.ToString() == "notification")
                    {
                        notificationTimer++;
                        break;
                    }
                }
                    
            }
            else
            {
                if (notificationTimer++ > 50)
                {
                    _mapView.RemoveNotification();
                    notificationTimer = 0;
                }
            }
        }
    }
}
