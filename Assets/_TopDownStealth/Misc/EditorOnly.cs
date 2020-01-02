using UnityEngine;

namespace Tools.Misc
{
    public class EditorOnly : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer = null;

        private void Start()
        {
            _renderer.enabled = false;
        }
    }
}
