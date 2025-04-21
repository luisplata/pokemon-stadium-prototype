using UnityEngine;
using UnityEngine.InputSystem;

namespace Multiplayer.Camera
{
    public class CameraInputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;
        private Vector2 lookInput;

        public Vector2 LookInput => lookInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
        }
    }
}