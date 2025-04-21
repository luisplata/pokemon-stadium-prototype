using UnityEngine;

namespace Multiplayer.Game
{
    public interface IPlayerFactory
    {
        GameObject GetLocalPlayer();
    }
}