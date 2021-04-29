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

        public Medic() : base()
        {

        }

        public Medic(int number, int health, int damage)
        {
            Specialty = "Медик";
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
        public Infantryman() : base()
        {

        }
        public Infantryman(int number, int health, int damage)
        {
            Status.SetTarget(true);
            Status.SetLoadGun(true);
            Status.SetAvoid(false);

            Specialty = "Штурмовик";
            Number = number;
            MaxHealth = health;
            Health = health;
            Damage = damage;
        }

        public override void UseAbillity(Soldier enemy)
        {
            Health += Random.Next(10, 20);
            AbillityCounter = Random.Next(1, 3);
        }
    }

    class Sniper : Soldier
    {
        public Sniper() : base()
        {

        }
        public Sniper(int number, int health, int damage)
        {
            Status.SetTarget(true);
            Status.SetLoadGun(true);
            Status.SetAvoid(false);

            Specialty = "Снайпер";
            Number = number;
            MaxHealth = health;
            Health = health;
            Damage = damage;
            AbillityCounter = 3;
        }

        public override void UseAbillity(Soldier soldier)
        {
            Status.SetAvoid(true);
            AbillityCounter = 3;
        }
    }
}
