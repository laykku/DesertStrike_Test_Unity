using System.Collections;
using DesertStrike.Characters;
using UnityEngine;

namespace DesertStrike
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : ABullet
    {
        private const float Damage = 1f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            Remove();
        }
        
        public override void Init(Vector3 origin, Vector3 dir)
        {
            transform.position = origin;
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(dir.normalized * 100f, ForceMode.Impulse);
        }
        
        protected override void OnCollisionEnter(Collision other)
        {
            IDamageReceiver damageReceiver = other.gameObject.GetComponent<IDamageReceiver>();
            
            if (damageReceiver != null)
            {
                damageReceiver.Hit(Damage);
            }
            
            Remove();
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
    }
}