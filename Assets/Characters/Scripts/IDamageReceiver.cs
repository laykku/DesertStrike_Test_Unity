namespace DesertStrike.Characters
{
    public interface IDamageReceiver
    {
        float Health { get; }

        void Hit(float damage);
    }
}