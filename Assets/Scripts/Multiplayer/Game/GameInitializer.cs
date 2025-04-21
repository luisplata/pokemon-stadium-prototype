using Multiplayer.Game;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private IPlayerFactory playerFactory;
    private PlayerSetupHandler setupHandler;

    private void Start()
    {
        playerFactory = ServiceLocator.Instance.GetService<IPlayerFactory>();
        setupHandler = ServiceLocator.Instance.GetService<PlayerSetupHandler>();
    }

    private void OnEnable()
    {
        ConnectionHandler.OnLocalPlayerReady += OnPlayerReady;
    }

    private void OnDisable()
    {
        ConnectionHandler.OnLocalPlayerReady -= OnPlayerReady;
    }

    private void OnPlayerReady()
    {
        var player = playerFactory.GetLocalPlayer();

        if (player != null)
        {
            Debug.Log("[GameInitializer] Jugador listo. Inicializando.");
            setupHandler.SetupPlayer(player);  // Configuramos la cámara y demás
        }
        else
        {
            Debug.LogWarning("[GameInitializer] Jugador local no disponible.");
        }
    }
}