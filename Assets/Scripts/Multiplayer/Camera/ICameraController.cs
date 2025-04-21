using UnityEngine;

namespace Multiplayer.Camera
{
    public interface ICameraController
    {
        void Initialize();
        void UpdateCamera();
        void SetTarget(Transform target);
    }
}