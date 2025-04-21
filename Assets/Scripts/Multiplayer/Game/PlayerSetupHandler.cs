using Mirror;
using UnityEngine;

namespace Multiplayer.Game
{
    public class PlayerSetupHandler : MonoBehaviour
    {
        [SerializeField] private GameObject cameraPrefab;
        
        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void SetupPlayer(GameObject player)
        {
            if (player == null) return;

            var isLocal = player.GetComponent<NetworkIdentity>().isLocalPlayer;
            if (!isLocal) return;

            // Instancia cámara y asocia al jugador
            GameObject cam = Instantiate(cameraPrefab);
            cam.GetComponent<CameraController>().SetTarget(player.transform);

            // También puedes inicializar Input, HUD, etc.
        }
    }
}