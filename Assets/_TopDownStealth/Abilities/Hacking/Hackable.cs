using System.Collections;
using UnityEngine;

namespace TopDownStealth
{
    public class Hackable : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _mainRenderer = null;

        [SerializeField]
        private MeshRenderer _fovRenderer = null;

        [SerializeField]
        private float _hackingTime = 1f;
        public float HackingTime => _hackingTime;

        [SerializeField]
        private float _effectDuration = 2f;
        public float EffectDuration => _effectDuration;

        private System.Action OnHackSuccessful = null;
        private System.Action OnHackTimedOut = null;

        public bool IsHacked { get; private set; } = false;
        public float Cooldown { get; private set; } = 0;

        private MaterialPropertyBlock _mainRendererPropertyBlock = null;

        private void Awake()
        {
            _mainRendererPropertyBlock = new MaterialPropertyBlock();
            _mainRenderer.GetPropertyBlock(_mainRendererPropertyBlock);

            SetMaterialProperties();
        }

        private void SetMaterialProperties()
        {
            _mainRendererPropertyBlock.SetFloat("_Disabled", IsHacked ? 1 : 0);
            _mainRenderer.SetPropertyBlock(_mainRendererPropertyBlock);
        }

        public virtual void Hack()
        {
            StartCoroutine(RefreshState());
        }

        private IEnumerator RefreshState()
        {
            IsHacked = true;
            SetMaterialProperties();
            _fovRenderer.enabled = false;
            OnHackSuccessful?.Invoke();

            Cooldown = _effectDuration;

            while (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
                yield return null;
            }

            Cooldown = 0;

            IsHacked = false;
            SetMaterialProperties();
            _fovRenderer.enabled = true;
            OnHackTimedOut?.Invoke();
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
