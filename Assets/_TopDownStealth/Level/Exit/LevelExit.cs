using Tools;
using UnityEngine;

namespace TopDownStealth
{
    [RequireComponent(typeof(Collider))]
    public class LevelExit : MonoBehaviour
    {
        private bool _triggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered)
            {
                _triggered = true;
                EventManager.Instance.Trigger(GameplayEvent.ExitReached);
            }
        }
    }
}