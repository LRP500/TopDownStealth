using TMPro;
using UnityEngine;

namespace TopDownStealth
{
    public class HackCooldown : MonoBehaviour
    {
        [SerializeField]
        private Hackable _hackable = null;

        [SerializeField]
        private TextMeshProUGUI _cooldownText = null;

        private void Awake()
        {
            _cooldownText.enabled = false;
        }

        private void Update()
        {
            if (_hackable.IsHacked)
            {
                transform.eulerAngles = new Vector3(90, 0, 0);

                float cooldown = Mathf.Clamp(_hackable.Cooldown, 0, _hackable.EffectDuration);
                _cooldownText.text = cooldown.ToString("00.00");
                _cooldownText.enabled = true;

                /// [TODO] make text red when cooldown under critical treshold
            }
            else
            {
                _cooldownText.enabled = false;
            }
        }
    }
}
