using UnityEngine;

namespace Multiplayer.Camera
{
    public class CameraManager : MonoBehaviour
    {
        private ICameraController currentController;

        public void SetCameraController(ICameraController controller, Transform target)
        {
            currentController = controller;
            currentController.SetTarget(target);
            currentController.Initialize();
        }

        private void LateUpdate()
        {
            currentController?.UpdateCamera();
        }
    }
}