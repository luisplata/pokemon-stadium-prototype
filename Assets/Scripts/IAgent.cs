using UnityEngine;

public interface IAgent
{
    void AddCommand(ICommand command);
    GameObject GetGameObject();
}