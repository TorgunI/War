using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
    abstract class Soldier
    {
        protected Status Status;
        protected Random Random = new Random();

        public int Number { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int AbillityCounter { get; protected set; }
        public string Specialty { get; protected set; }

        public abstract void UseAbillity(Soldier soldier);


        public Soldier()
        {
            Status = new Status();

            Status.SetTarget(false);
            Status.SetLoadGun(false);
            Status.SetAvoid(false);

            Number = 0;
            MaxHealth = 0;
            Health = 0;
            Damage = 0;
            AbillityCounter = 0;
            Specialty = "Новобранец";
        }

        public bool GetAvoidState()
        {
            return Status.IsAvoid;
        }

        public bool GetGunLoadState()
        {
            return Status.IsGunLoaded;
        }

        public bool GetTargetState()
        {
            return Status.IsTargetEnemy;
        }

        public void TryAttack(Soldier enemy, Soldier ally)
        {
            if (Status.IsGunLoaded)
            {
                Attack(enemy);
            }
            else if (AbillityCounter == 0)
            {
                if (Status.IsTargetEnemy)
                {
                    UseAbillity(enemy);
                }
                else if (Status.IsTargetEnemy == false)
                {
                    UseAbillity(ally);
                }
            }
        }

        public void Attack(Soldier enemy)
        {
            if(enemy.Status.IsAvoid == false)
            {
                enemy.Health -= Damage;
            }

            Status.SetLoadGun(false);
        }

        public void ReloadGun()
        {
            Status.SetLoadGun(true);
        }

        public virtual void Heal(Soldier ally)
        {
            if (Health < MaxHealth)
            {
                ally.Health += Random.Next(40, 70);
            }
            else
            {
                ally.Health += Random.Next(10, 30);
            }
        }

        public void CooldownAbility()
        {
            if (AbillityCounter > 0)
                --AbillityCounter;
        }
    }
}
