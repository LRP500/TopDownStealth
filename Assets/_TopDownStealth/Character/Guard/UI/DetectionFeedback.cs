using Tools.UI;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class DetectionFeedback : SliderValueSetter
    {
        [SerializeField]
        private Guard _guard = null;

        private void Awake()
        {
            SetMin(0);
            SetMax(1);
        }

        private void Update()
        {
            SetValue(_guard.DetectionLevel);
        }
    }
}
