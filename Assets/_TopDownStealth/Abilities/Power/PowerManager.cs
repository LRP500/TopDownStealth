using Tools.Variables;
using UnityEngine;

namespace TopDownStealth
{
    public class PowerManager : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable _maximumValue = null;

        [SerializeField]
        private FloatVariable _currentValue = null;
        public float Current => _currentValue;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _currentValue.SetValue(_maximumValue);
        }

        public void Substract(float amount)
        {
            float newValue = Mathf.Clamp(_currentValue - amount, 0, _maximumValue);
            _currentValue.SetValue(newValue);
        }

        public void Add(float amount)
        {
            float newValue = Mathf.Clamp(_currentValue + amount, 0, _maximumValue);
            _currentValue.SetValue(newValue);
        }
    }
}