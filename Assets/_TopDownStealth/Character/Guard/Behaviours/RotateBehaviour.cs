using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownStealth.Characters.Behaviours
{
    [CreateAssetMenu(menuName = "Top Down Stealth/Characters/Behaviours/Rotate")]
    public class RotateBehaviour : CharacterBehaviour
    {
        [System.Flags]
        public enum Direction
        {
            North = 1 << 1,
            East = 1 << 2,
            South = 1 << 3,
            West = 1 << 4
        }


        private Dictionary<Direction, Vector3> _directions = new Dictionary<Direction, Vector3>
        {
            { Direction.North, Vector3.forward },
            { Direction.East, Vector3.right },
            { Direction.South, -Vector3.forward },
            { Direction.West, -Vector3.right }
        };

        [EnumToggleButtons]
        public Direction _direction = default;

        [SerializeField]
        private float _idleTime = 2f;

        [SerializeField]
        private float _rotationSpeed = 2f;

        public override void Initialize(Character character)
        {
        }

        public override IEnumerator Run(Character character)
        {
            while (character.enabled)
            {
                foreach (Direction value in Direction.GetValues(_direction.GetType()))
                {
                    if (_direction.HasFlag(value))
                    {
                        Vector3 dir = character.transform.position + _directions[value];
                        yield return new WaitForSeconds(_idleTime);
                        yield return character.StartCoroutine(character.LookAt(dir, _rotationSpeed));
                    }
                }
            }
        }
    }
}