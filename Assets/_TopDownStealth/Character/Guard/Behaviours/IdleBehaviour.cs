using System.Collections;
using UnityEngine;

namespace TopDownStealth.Characters.Behaviours
{
    [CreateAssetMenu(menuName = "Top Down Stealth/Characters/Behaviours/Idle")]
    public class IdleBehaviour : CharacterBehaviour
    {
        public override void Initialize(Character character)
        {
        }

        public override IEnumerator Run(Character character)
        {
            yield return null;
        }
    }
}
