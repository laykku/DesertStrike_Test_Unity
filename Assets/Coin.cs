using System;
using System.Collections;
using DesertStrike.Characters;
using UnityEngine;
using Zenject;

namespace DesertStrike
{
    public class Coin : MonoBehaviour
    {
        public event Action<Coin> Collected;

        private const float DestroyDelay = 10f;
        private const float RotationSpeed = 360f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(DestroyDelay);
            Remove();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                Collected?.Invoke(this);
                StartCoroutine(Collect());
            }
        }

        private IEnumerator Collect()
        {
            Vector3 start = transform.position;
            Vector3 target = transform.position + Vector3.up * 5f;
            for (float t = 0f; t <= 1f; t += Time.deltaTime * 5f)
            {
                transform.position = Vector3.Lerp(start, target, t);
                yield return null;
            }

            Remove();
        }

        private void Remove()
        {
            Destroy(gameObject);
        }

        public class Factory : Factory<Coin>
        {
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }
    }
}