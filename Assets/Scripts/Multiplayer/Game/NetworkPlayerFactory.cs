using Mirror;
using UnityEngine;

namespace Multiplayer.Game
{
    public class NetworkPlayerFactory : MonoBehaviour, IPlayerFactory
    {
        private void Awake()
        {
            ServiceLocator.Instance.RegisterService<IPlayerFactory>(this);
        }

        public GameObject GetLocalPlayer()
        {
            return NetworkClient.localPlayer?.gameObject;
        }
    }
}