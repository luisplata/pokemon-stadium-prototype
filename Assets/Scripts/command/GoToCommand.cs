using Cysharp.Threading.Tasks;
using UnityEngine;

public class GoToCommand : ICommand
{
    private readonly Vector3 _position;
    private readonly GameObject gameObject;

    public GoToCommand(GameObject gameObject, Vector3 position)
    {
        this.gameObject = gameObject;
        _position = position;
    }

    public async UniTask Execute()
    {
        //move with constant speed
        Debug.Log("GoCommand: " + _position);
        Debug.Log("ToCommand: " + gameObject.transform.position);
        while (Vector3.Distance(gameObject.transform.position, _position) > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _position, 0.05f);
            await UniTask.Yield();
        }

        // Ensure the final position is set exactly
        gameObject.transform.position = _position;
        Debug.Log("GoCommand After: " + _position);
        Debug.Log("ToCommand After: " + gameObject.transform.position);
    }
}