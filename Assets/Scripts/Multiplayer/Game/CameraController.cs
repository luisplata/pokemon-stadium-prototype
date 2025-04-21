using Unity.Cinemachine;
using UnityEngine;

namespace Multiplayer.Game
{
    public class CameraController : MonoBehaviour
    {
        public void SetTarget(Transform target)
        {
            GetComponent<CinemachineCamera>().Follow = target;
            GetComponent<CinemachineCamera>().LookAt = target;
        }
    }
}