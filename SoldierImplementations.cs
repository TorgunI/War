using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
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
