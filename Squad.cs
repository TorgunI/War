using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
    class Squad
    {
        private List<Soldier> _soldiers;

        private static Random _rand = new Random(DateTime.Now.Millisecond);

        public int SoldiersCount => _soldiers.Count;
        public Soldier Frontman { get; private set; }

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

        public void TakeAction(Soldier enemyFrontman)
        {
            Frontman.TryAttack(enemyFrontman, GetRandomSoldier(_soldiers));
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

        public Soldier ChooseRandomFrontmen()
        {
            List<Soldier> readySoldiers = new List<Soldier>();

            foreach (var soldier in _soldiers)
            {
                if(soldier.GetGunLoadState() == true || soldier.AbillityCounter == 0)
                {
                    readySoldiers.Add(soldier);
                }
            }

            if(readySoldiers.Count == 0)
            {
                Frontman = GetRandomSoldier(_soldiers);
            }
            else
            {
                Frontman = GetRandomSoldier(readySoldiers);
            }
            return Frontman;
        }

        public Soldier GetRandomSoldier(List<Soldier> soldiers)
        {
            return soldiers[_rand.Next(0, soldiers.Count)];
        }

        public bool IsSoldiersUnready()
        {
            foreach (var soldier in _soldiers)
            {
                if (soldier.AbillityCounter == 0 && soldier.GetGunLoadState() == true)
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

        public bool TryDeleteDeadSoldier()
        {
            if (Frontman.Health <= 0)
            {
                _soldiers.Remove(Frontman);
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
}
