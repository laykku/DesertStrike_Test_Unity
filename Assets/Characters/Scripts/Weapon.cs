using DesertStrike.Characters;
using UnityEngine;

namespace DesertStrike
{
    public class Weapon : IWeapon
    {
        private readonly ABullet.Factory _bulletFactory;

        public Weapon(ABullet.Factory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        private float _shootTs;
        
        public void Shoot(Vector3 origin, Vector3 dir)
        {
            if (Time.time - _shootTs > 0.1f)
            {
                _shootTs = Time.time;
                
                ABullet bullet = _bulletFactory.Create();
                bullet.Init(origin, dir);
            }
        }
    }
}