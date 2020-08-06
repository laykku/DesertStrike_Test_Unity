using UnityEngine;
using Zenject;

namespace DesertStrike
{
    public abstract class ABullet : MonoBehaviour
    {
        public virtual void Init(Vector3 origin, Vector3 dir)
        {
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
        }
        
        public class Factory : Factory<ABullet>
        {
        }
    }
}