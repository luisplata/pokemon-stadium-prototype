using UnityEngine;

public class Agent : MonoBehaviour, IAgent
{
    private readonly CommandQueue _commandQueue = new();

    public void AddCommand(ICommand command)
    {
        _commandQueue.AddCommand(command);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}