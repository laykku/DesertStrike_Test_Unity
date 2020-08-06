using UnityEngine;

namespace DesertStrike.Level
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        public Transform Transform => transform;
    }
}