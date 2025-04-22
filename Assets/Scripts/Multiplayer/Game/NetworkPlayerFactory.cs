using Mirror;
using UnityEngine;

namespace Multiplayer.Game
{
    public class NetworkPlayerFactory : NetworkManager, IPlayerFactory
    {
        private void Awake()
        {
            ServiceLocator.Instance.RegisterService<IPlayerFactory>(this);
        }

        public GameObject GetLocalPlayer()
        {
            if (NetworkClient.isConnected)
            {
                // Asegúrate de que el cliente esté conectado antes de acceder al jugador local
                if (NetworkClient.localPlayer == null)
                {
                    Debug.LogWarning("El jugador local no está disponible.");
                    return null;
                }

                // Devuelve el objeto del jugador local
                return NetworkClient.localPlayer.gameObject;
            }
            else
            {
                Debug.LogWarning("El cliente no está conectado al servidor.");
            }

            return NetworkClient.localPlayer?.gameObject;
        }
    }
}