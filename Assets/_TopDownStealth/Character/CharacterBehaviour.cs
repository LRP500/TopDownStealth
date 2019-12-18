using UnityEngine;

namespace TopDownStealth
{
    public abstract class CharacterBehaviour : ScriptableObject
    {
        public abstract void Initialize(Character character);
        public abstract void Run(Character character);
    }
}
