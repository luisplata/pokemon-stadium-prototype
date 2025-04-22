using Mirror;
using Unity.Cinemachine;
using UnityEngine;

namespace Multiplayer.Game
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private CameraController cameraPrefab;
        [SerializeField] private GameObject positionOfCamera;

        private PlayerInputActions inputActions;
        private Vector2 moveInput;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            // Activamos los inputs solo si somos el jugador local
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

            // Instanciamos la cámara y la asociamos al jugador
            var camera = Instantiate(cameraPrefab);
            camera.SetTarget(transform);
            camera.transform.position = positionOfCamera.transform.position;
            camera.transform.rotation = positionOfCamera.transform.rotation;
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * (moveSpeed * Time.deltaTime);
            transform.Translate(movement, Space.World);
        }

        private void OnDisable()
        {
            if (inputActions != null)
                inputActions.Player.Disable();
        }
    }
}