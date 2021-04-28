using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
    class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();
            battle.RunMenu();
        }
    }

    class Battle
    {
        int _raund = 0;

        private Squad _squadLeft;
        private Squad _squadRight;
        private static Random _rand;

        public Battle()
        {
            _raund = 1;
            _rand = new Random();
            _squadLeft = new Squad();
            _squadRight = new Squad();
        }

        public void RunMenu()
        {
            bool isRun = true;
            while (isRun)
            {
                Console.WriteLine("[1] - Создать отряд\n[2] - Запустить бой\n[3] - Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        _squadLeft.CreateSquad();
                        _squadRight.CreateSquad();
                        break;
                    case "2":
                        StartGame();
                        break;
                    case "3":
                        isRun = false;
                        break;
                }
            }
        }

        private void StartGame()
        {
            if(IsGameReady() == false)
            {
                return;
            }

            Soldier leftSoldier;
            Soldier rightSoldier;

            while (IsGameOver() == false)
            {
                leftSoldier = _squadLeft.GetRandomSoldier();
                rightSoldier = _squadRight.GetRandomSoldier();

                Console.WriteLine("Западный отряд атакует!");
                _squadLeft.GetCommand(leftSoldier, rightSoldier);

                Console.WriteLine("Восточный отряд атакует!");
                _squadRight.GetCommand(rightSoldier, leftSoldier);

                CheckDeath(leftSoldier, rightSoldier);

                if(CheckReadySquads())
                {
                    EndRaund();
                }

                Console.ReadKey();
                Console.Clear();
            }
        }     

        private void CheckDeath(Soldier leftSoldier, Soldier rightSoldier)
        {
            _squadRight.TryDeleteDeadSoldier(rightSoldier);
            _squadLeft.TryDeleteDeadSoldier(leftSoldier);
        }

        private bool CheckReadySquads()
        {
            if (_squadLeft.IsSoldiersUnready() && _squadRight.IsSoldiersUnready())
            {
                _squadLeft.Refresh();
                _squadRight.Refresh();
                return true;
            }
            return false;
        }

        private void EndRaund()
        {
            Console.WriteLine("Обе стороны готовятся к следующей атаке");
             _raund++;
        }

        private bool IsGameOver()
        {
            if(_squadLeft.GetHealthSquad() <= 0)
            {
                Console.WriteLine("Западный отряд умер!");
                return true;
            }
            else if(_squadRight.GetHealthSquad() <= 0)
            {
                Console.WriteLine("Восточный отряд умер!");
                return true;
            }

            Console.WriteLine("Raund: " + _raund);
            Console.WriteLine("\nСилы западного отряда: " + _squadLeft.GetHealthSquad());
            Console.WriteLine("Силы восточного отряда: " + _squadRight.GetHealthSquad() + "\n");
            return false;
        }

        private bool IsGameReady()
        {
            if (_squadLeft.GetSquadSize() < 0)
            {
                Console.WriteLine("Левый отряд не создан!");
                return false;
            }
            else if(_squadRight.GetSquadSize() < 0)
            {
                Console.WriteLine("Правый отряд не создан!");
                return false;
            }
            return true;
        }
    }

    class Squad
    {
        private List<Soldier> _soldiers;
        private static Random _rand = new Random();

        public Squad()
        {
            _soldiers = new List<Soldier>(20);
        }

        public void CreateSquad()
        {
            Console.WriteLine("Создаем отряд...");

            Medic medic = new Medic();
            CreateSoldier(5, medic);

            Sniper sniper = new Sniper();
            CreateSoldier(5, sniper);

            Infantryman infantryman = new Infantryman();
            CreateSoldier(10, infantryman);

            for (int counter = 0; counter < _soldiers.Count; counter++)
            {
                int swapIndex = _rand.Next(_soldiers.Count);
                Soldier soldier = _soldiers[swapIndex];
                _soldiers[swapIndex] = _soldiers[counter];
                _soldiers[counter] = soldier;
            }
        }

        public void GetCommand(Soldier soldier, Soldier enemy)
        {
            if (soldier.IsGunReady && enemy.IsAvoid == false)
            {
                soldier.Attack(enemy);
            }
            else if (soldier.AbillityCounter == 0)
            {
                if (soldier.IsTargetEnemy)
                {
                    soldier.UseAbillity(enemy);
                }
                else if (soldier.IsTargetEnemy == false)
                {
                    for (int i = 0; i < _rand.Next(1,5); i++)
                    {
                        soldier.UseAbillity(GetRandomSoldier());
                    }
                }
            }
        }

        public int GetHealthSquad()
        {
            int healthSquad = 0;

            foreach (var soldier in _soldiers)
            {
                healthSquad += soldier.Health;
            }
            return healthSquad;
        }

        public int GetSquadSize()
        {
            return _soldiers.Count;
        }

        public Soldier GetRandomSoldier()
        {
            for (int i = 0; i < _soldiers.Count; i++)
            {
                Soldier soldier = _soldiers[_rand.Next(0, _soldiers.Count)];
                if (soldier.IsGunReady || soldier.AbillityCounter == 0)
                {
                    return soldier;
                }
            }
            return _soldiers[_rand.Next(0, _soldiers.Count)];
        }

        public bool IsSoldiersUnready()
        {
            foreach (var soldier in _soldiers)
            {
                if (soldier.AbillityCounter == 0 && soldier.IsGunReady)
                {
                    return false;
                }
            }
            return true;
        }

        public void Refresh()
        {
            foreach (var soldier in _soldiers)
            {
                soldier.ReloadGun();
                soldier.CooldownAbility();
            }
        }

        public bool TryDeleteDeadSoldier(Soldier soldier)
        {
            if (soldier.Health <= 0)
            {
                _soldiers.Remove(soldier);
                return true;
            }
            return false;
        }

        private void CreateSoldier(int soldiersCapasity, Soldier soldier)
        {
            for (int counter = 0; counter < soldiersCapasity; counter++)
            {
                var type = soldier;

                if (type as Medic != null)
                {
                    type = new Medic(counter, _rand.Next(50, 100), _rand.Next(25, 50));
                    _soldiers.Add(type);
                }
                else if (type as Sniper != null)
                {
                    type = new Sniper(counter, _rand.Next(50, 70), _rand.Next(85, 105));
                    _soldiers.Add(type);
                }
                else if (type as Infantryman != null)
                {
                    type = new Infantryman(counter, _rand.Next(70, 100), _rand.Next(50, 80));
                    _soldiers.Add(type);
                }
            }
        }
    }

    abstract class Soldier
    {
        protected Random _random = new Random();
        public bool IsTargetEnemy { get; protected set; }
        public bool IsGunReady { get; protected set; }
        public bool IsAvoid { get; protected set; }
        public int Number { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int AbillityCounter { get; protected set; }
        public string Specialty { get; protected set; }

        public abstract void UseAbillity(Soldier soldier);

        public void Attack(Soldier enemy)
        {
            IsGunReady = false;
            enemy.Health -= Damage;
        }

        public virtual void Heal(Soldier ally)
        {
            if(Health < MaxHealth)
            {
                ally.Health += _random.Next(10, 30);
            }
        }

        public void ReloadGun()
        {
            IsGunReady = true;
        }

        public void CooldownAbility()
        {
            if(AbillityCounter > 0)
                --AbillityCounter;
        }
    }

    class Medic : Soldier
    {
        public int ChargeAbility { get; private set; }
        public Medic()
        {
            Specialty = "Медик";
            IsTargetEnemy = false;
            IsGunReady = false;
            IsAvoid = false;
            Number = 0;
            MaxHealth = 0;
            Health = 0;
            Damage = 0;
            AbillityCounter = 0;
            ChargeAbility = 0;
        }

        public Medic(int number, int health, int damage)
        {
            Specialty = "Медик";
            IsTargetEnemy = false;
            IsGunReady = false;
            IsAvoid = false;
            Number = number;
            MaxHealth = health;
            Health = health;
            Damage = damage;
            AbillityCounter = 0;
            _random = new Random();
            ChargeAbility = _random.Next(1, 5);
        }

        public override void UseAbillity(Soldier ally)
        {
            Heal(ally);
            AbillityCounter = 5;
        }
    }

    class Infantryman : Soldier
    {
        public Infantryman()
        {
            Specialty = "Штурмовик";
            IsTargetEnemy = false;
            IsGunReady = false;
            IsAvoid = false;
            Number = 0;
            MaxHealth = 0;
            Health = 0;
            Damage = 0;
        }

        public Infantryman(int number, int health, int damage)
        {
            Specialty = "Штурмовик";
            IsTargetEnemy = true;
            IsGunReady = true;
            IsAvoid = false;
            Number = number;
            MaxHealth = health;
            Health = health;
            Damage = damage;
        }

        public override void UseAbillity(Soldier enemy)
        {
            Health += _random.Next(10, 20);
            AbillityCounter = _random.Next(1, 3);
        }
    }

    class Sniper : Soldier
    {
        public Sniper()
        {
            Specialty = "Снайпер";
            IsTargetEnemy = false;
            IsGunReady = false;
            IsAvoid = false;
            Number = 0;
            MaxHealth = 0;
            Health = 0;
            Damage = 0;
            AbillityCounter = 0;
        }

        public Sniper(int number, int health, int damage)
        {
            Specialty = "Снайпер";
            IsTargetEnemy = true;
            IsGunReady = true;
            IsAvoid = false; 
            Number = number;
            MaxHealth = health;
            Health = health;
            Damage = damage;
            AbillityCounter = 3;
        }

        public override void UseAbillity(Soldier soldier)
        {
            IsAvoid = true;
            AbillityCounter = 3;
        }
    }
}
