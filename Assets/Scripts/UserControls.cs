using DesertStrike.Characters;
using UnityEngine;

namespace DesertStrike
{
    public class UserControls : ICharacterControls
    {
        public float Horizontal => Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");
        public bool FireStart => Input.GetButtonDown("Fire3");
        public bool FireStop => Input.GetButtonUp("Fire3");
        
        public void SetCharacter(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}