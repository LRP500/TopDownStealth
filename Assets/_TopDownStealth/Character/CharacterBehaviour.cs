using System.Collections;
using UnityEngine;

namespace TopDownStealth.Characters
{
    public abstract class CharacterBehaviour : ScriptableObject
    {
        public abstract void Initialize(Character character);
        public abstract IEnumerator Run(Character character);

        public virtual void Reset(Character character) { }
    }
}
