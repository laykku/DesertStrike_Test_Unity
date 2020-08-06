using UnityEngine;

namespace DesertStrike.Characters
{
    public interface IWeapon
    {
        void Shoot(Vector3 origin, Vector3 dir);
    }
}