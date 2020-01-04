using Cinemachine;
using UnityEngine;

namespace TopDownStealth
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _camera = null;

        [SerializeField]
        private CharacterVariable _player = null;

        private void Start()
        {
            _camera.m_Follow = _player.Value.transform;
        }
    }
}