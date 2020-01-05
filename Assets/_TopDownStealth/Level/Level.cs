using UnityEngine;

namespace TopDownStealth
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private Transform _start = null;
        public Transform Start => _start;
    }
}
