using UnityEngine;
using Zenject;

namespace DesertStrike.Level
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundTile : MonoBehaviour
    {
        public Transform Transform => transform;
        public BoxCollider Collider { get; private set; }
        
        private void Awake()
        {
            Collider = GetComponent<BoxCollider>();
        }
        
        public class Factory : PlaceholderFactory<GroundTile>
        {
        }
    }
}