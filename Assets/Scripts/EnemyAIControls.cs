using System.Collections.Generic;
using DesertStrike.Characters;
using DesertStrike.Level;
using UnityEngine;
using Zenject;

namespace DesertStrike
{
    public class EnemyAIControls : ITickable, ICharacterControls
    {
        public class CharacterState
        {
            public readonly List<Vector3> Path = new List<Vector3>();
            public int CurrentPathIndex;
            public Vector3 CurrentTarget;
        }
        
        public float Horizontal => 0f;
        public float Vertical { get; private set; }
        public bool FireStart => false;
        public bool FireStop => false;

        private LevelGenerator _level;
        
        private readonly Dictionary<Character, CharacterState> _characters =
            new Dictionary<Character, CharacterState>();

        public EnemyAIControls(LevelGenerator level)
        {
            _level = level;
        }
        
        public void SetCharacter(Character character)
        {
            _characters.Add(character, new CharacterState());
            character.Killed += OnCharacterKilled;
        }

        private void OnCharacterKilled(Character character)
        {
            _characters.Remove(character);
        }

        public void Tick()
        {
            foreach (var character in _characters)
            {
                if (Vector3.Distance(character.Value.CurrentTarget, character.Key.transform.position) < 1f)
                {
                    if (character.Value.CurrentPathIndex < character.Value.Path.Count - 1)
                    {
                        character.Value.CurrentPathIndex++;
                    }
                    else
                    {
                        Vector3 target = _level.GetRandomPoint();
                        character.Value.Path.Clear();
                        character.Value.Path.AddRange(_level.FindPath(
                            character.Key.transform.position, target));
                        character.Value.CurrentPathIndex = 0;
                    }

                    character.Value.CurrentTarget = character.Value.Path[character.Value.CurrentPathIndex];
                    character.Value.CurrentTarget.y = 0f;
                }

                character.Key.transform.LookAt(character.Value.CurrentTarget);
            }
            
            Vertical = 1f;
        }
    }
}