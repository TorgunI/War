using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
    class Status
    {
        public bool IsTargetEnemy { get; private set; }
        public bool IsGunLoaded { get; private set; }
        public bool IsAvoid { get; private set; }

        public Status()
        {
            IsTargetEnemy = false;
            IsGunLoaded = false;
            IsAvoid = false;
        }

        public void SetTarget(bool state)
        {
            IsTargetEnemy = state;
        }

        public void SetLoadGun(bool state)
        {
            IsGunLoaded = state;
        }

        public void SetAvoid(bool state)
        {
            IsAvoid = state;
        }

        
    }
}