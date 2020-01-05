using Cinemachine;
using UnityEngine;

namespace TopDownStealth
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _camera = null;

        public void SetFollowTarget(Transform target)
        {
            _camera.m_Follow = target;
        }
    }
}