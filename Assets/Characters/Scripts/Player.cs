using Zenject;

namespace DesertStrike.Characters
{
    public class Player : Character
    {
        [Inject]
        public void Construct(UserControls controls)
        {
            Init(controls);
        }

        public class Factory : Factory<Player>
        {
        }
    }
}