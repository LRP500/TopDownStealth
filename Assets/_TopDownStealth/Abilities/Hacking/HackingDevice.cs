using System.Collections;
using Tools.Variables;
using UnityEngine;

namespace TopDownStealth
{
    public class HackingDevice : ToggleAbility
    {
        [SerializeField]
        private LayerMask _hackableLayers = default;

        [SerializeField]
        private TransformVariable _target = null;

        [SerializeField]
        private FloatVariable _progress = null;

        [SerializeField]
        private LineRenderer _lineRenderer = null;

        private Hackable _hackable = null;

        private bool _hacking = false;

        public override void Activate()
        {
            base.Activate();

            if (Active)
            {
                _hackable = GetHackableTarget();

                if (_hackable != null)
                {
                    _target.SetValue(_hackable.transform);
                    StartCoroutine(StartHacking());
                }
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();

            CancelHack();
        }

        protected override bool CanActivate()
        {
            return true;
        }

        private IEnumerator StartHacking()
        {
            Debug.Log($"[Player] Initiating hacking procedure on {transform.parent.name}...");

            _hacking = true;
            _lineRenderer.enabled = true;

            float elapsed = 0;
            while (elapsed < _hackable.HackingTime)
            {
                elapsed += Time.deltaTime;
                _progress.SetValue(elapsed / _hackable.HackingTime);
                RefreshLine();
                yield return null;
            }

            _hackable.Hack();
            Reset();

            Debug.Log("<color=green>[Player] Hack successful</color>");
        }

        private void RefreshLine()
        {
            Vector3 dir = (_target.Value.position - transform.position).normalized;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position + (dir / 2));
            _lineRenderer.SetPosition(1, _target.Value.position - (dir / 2));
        }

        private void Reset()
        {
            _hacking = false;
            _target.Clear();
            _progress.Clear();
            _lineRenderer.enabled = false;
        }

        public void CancelHack()
        {
            if (_hacking)
            {
                StopAllCoroutines();

                Reset();

                Debug.Log("<color=red>[Player] Hacking procedure cancelled</color>");
            }
        }

        private Hackable GetHackableTarget()
        {
            Vector3 dir = transform.forward;

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _hackableLayers))
            {
                return hit.collider.gameObject.GetComponent<Hackable>();
            }

            return null;
        }
    }
}
