using System;
using Mirror;
using UnityEngine;

namespace Multiplayer.Game
{
    public class ConnectionHandler : MonoBehaviour
    {
        public static event Action OnLocalPlayerReady;

        private void OnEnable()
        {
            // Registramos el evento cuando el cliente se conecta
            NetworkClient.OnConnectedEvent += OnClientConnected;
            NetworkClient.OnDisconnectedEvent += OnClientDisconnected;
        }

        private void OnDisable()
        {
            // Desregistramos los eventos
            NetworkClient.OnConnectedEvent -= OnClientConnected;
            NetworkClient.OnDisconnectedEvent -= OnClientDisconnected;
        }

        private void OnClientConnected()
        {
            Debug.Log("[ConnectionHandler] Cliente conectado al servidor.");
        }

        private void OnClientDisconnected()
        {
            Debug.Log("[ConnectionHandler] Cliente desconectado del servidor.");
        }

        public static void NotifyPlayerReady()
        {
            OnLocalPlayerReady?.Invoke();
        }
    }
}