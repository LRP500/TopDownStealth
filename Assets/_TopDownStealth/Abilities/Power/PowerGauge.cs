using System.Collections;
using Tools.Variables;
using UnityEngine;

namespace TopDownStealth
{
    public class PowerGauge : Tools.UI.ResourceGauge
    {
        [Space]
        [SerializeField]
        private FloatVariable _maximumPower = null;

        [SerializeField]
        private FloatVariable _currentPower = null;

        [SerializeField]
        private float _criticaltextBlinkInterval = 0.2f;

        private Coroutine _criticalFeedbackRoutine = null;

        private void Awake()
        {
            _maximumPower.Subscribe(RefreshGauge);
            _currentPower.Subscribe(RefreshGauge);
        }

        private void OnDestroy()
        {
            _maximumPower.Unsubscribe(RefreshGauge);
            _currentPower.Unsubscribe(RefreshGauge);
        }

        protected override void RefreshText()
        {
            if (IsCritical() && _criticalFeedbackRoutine == null)
            {
                _criticalFeedbackRoutine =  StartCoroutine(CriticalFeedback());
            }
        }

        private void RefreshGauge()
        {
            SetMax(_maximumPower);
            SetCurrent(_currentPower, false);
        }

        private IEnumerator CriticalFeedback()
        {
            while (IsCritical())
            {
                ValueText.enabled = !ValueText.enabled;
                yield return new WaitForSeconds(_criticaltextBlinkInterval);
            }

            ValueText.enabled = false;
            _criticalFeedbackRoutine = null;
        }
    }
}
