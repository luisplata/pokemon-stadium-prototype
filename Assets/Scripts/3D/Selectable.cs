using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    public GameObject GetGO()
    {
        return gameObject;
    }
}