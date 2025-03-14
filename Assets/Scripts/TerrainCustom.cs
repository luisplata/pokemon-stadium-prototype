using System.Collections.Generic;
using UnityEngine;

public class TerrainCustom : MonoBehaviour
{
    public List<TerrainCustom> neighbors = new();
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject[] exits;

    public GameObject Point => point;
    public GameObject[] Exits => exits;

    // Connection points to other TerrainCustom objects
    public List<ConnectionPoint> connectionPoints = new();
}

[System.Serializable]
public class ConnectionPoint
{
    public TerrainCustom connectedTerrain;
    public Transform connectionTransform;
}