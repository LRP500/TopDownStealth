using Sirenix.OdinInspector;
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

        [SerializeField]
        [LabelText("Critical Treshold (%)")]
        private float _criticalTreshold = 0f;

        [SerializeField]
        private Color _criticalColor = Color.white;

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
                _cooldownText.text = $"<mspace=0.3>{cooldown.ToString("00.00")}</mspace>";
                _cooldownText.enabled = true;

                float ratio = 1 / (_hackable.EffectDuration / cooldown);
                _cooldownText.color = ratio < (_criticalTreshold / 100) ? _criticalColor : Color.white;

                /// [TODO] make text red when cooldown under critical treshold
            }
            else
            {
                _cooldownText.enabled = false;
            }
        }
    }
}
