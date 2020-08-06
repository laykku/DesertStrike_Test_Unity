namespace DesertStrike.Characters
{
    public interface ICharacterControls
    {
        float Horizontal { get; }
        float Vertical { get; }
        bool FireStart { get; }
        bool FireStop { get; }

        void SetCharacter(Character character);
    }
}