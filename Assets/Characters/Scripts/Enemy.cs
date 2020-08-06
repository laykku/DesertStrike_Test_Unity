using DesertStrike.Level;
using Zenject;

namespace DesertStrike.Characters
{
    public class Enemy : Character
    {
        [Inject]
        public void Construct(EnemyAIControls controls)
        {
            Init(controls);
            controls.SetCharacter(this);
        }
        
        public class Factory : Factory<int, Enemy>
        {
        }
    }
}