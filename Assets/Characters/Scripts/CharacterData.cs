using UnityEngine;

namespace Characters.Scripts
{
    [CreateAssetMenu(menuName = "Character Data", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public float MovementSpeed = 200f;
        public float TurnSpeed = 360f;
    }
}