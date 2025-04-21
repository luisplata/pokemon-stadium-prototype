using Mirror;
using UnityEngine;

namespace Multiplayer.Game
{
    public class PlayerController : NetworkBehaviour
    {
        public override void OnStartLocalPlayer()
        {
            Debug.Log("[PlayerController] Jugador local instanciado.");
            ConnectionHandler.NotifyPlayerReady();  // Notificamos que el jugador está listo
        }
    }
}