using UnityEngine;

namespace DesertStrike
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
        [SerializeField] private float _smoothSpeed = 1f;

        private Transform _target = null;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
        private void FixedUpdate()
        {
            if (!_target) return;
            
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(_target);
        }
    }
}