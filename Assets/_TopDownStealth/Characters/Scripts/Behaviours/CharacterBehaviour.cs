using UnityEngine;

namespace TopDownShooter
{
    public abstract class CharacterBehaviour : ScriptableObject
    {
        public abstract void Initialize(Character character);
        public abstract void Run(Character character);
    }
}
