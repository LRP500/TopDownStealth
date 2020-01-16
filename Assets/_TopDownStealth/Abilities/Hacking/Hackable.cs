using System.Collections;
using UnityEngine;

namespace TopDownStealth
{
    public class Hackable : MonoBehaviour
    {
        [SerializeField]
        private float _hackingTime = 1f;
        public float HackingTime => _hackingTime;

        [SerializeField]
        private float _effectDuration = 2f;
        public float EffectDuration => _effectDuration;

        private System.Action OnHackSuccessful = null;
        private System.Action OnHackTimedOut = null;

        public bool IsHacked { get; private set; } = false;

        public virtual void Hack()
        {
            StartCoroutine(RefreshState());
        }

        private IEnumerator RefreshState()
        {
            IsHacked = true;
            OnHackSuccessful?.Invoke();

            yield return new WaitForSeconds(_effectDuration);

            OnHackTimedOut?.Invoke();
            IsHacked = false;
        }

        public void SubscribeOnHackSuccessful(System.Action callback)
        {
            OnHackSuccessful += callback;
        }

        public void UnsubscribeOnHackSuccessful(System.Action callback)
        {
            OnHackSuccessful -= callback;
        }

        public void SubscribeOnHackTimedOut(System.Action callback)
        {
            OnHackTimedOut += callback;
        }

        public void UnsubscribeOnHackTimedOut(System.Action callback)
        {
            OnHackTimedOut -= callback;
        }
    }
}
