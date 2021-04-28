using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
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

        public void TryAttack(Soldier enemy, Soldier ally)
        {
            if (IsGunReady)
            {
                Attack(enemy);
            }
            else if (AbillityCounter == 0)
            {
                if (IsTargetEnemy)
                {
                    UseAbillity(enemy);
                }
                else if (IsTargetEnemy == false)
                {
                    UseAbillity(ally);
                }
            }
        }

        public void Attack(Soldier enemy)
        {
            if(enemy.IsAvoid == false)
            {
                enemy.Health -= Damage;
            }

            IsGunReady = false;
        }

        public virtual void Heal(Soldier ally)
        {
            if (Health < MaxHealth)
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
            if (AbillityCounter > 0)
                --AbillityCounter;
        }
    }
}
