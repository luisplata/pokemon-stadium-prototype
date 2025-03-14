using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerate : MonoBehaviour
{
    [SerializeField] private GameObject terrainContainer;
    [SerializeField] private TerrainCustom terrainPrefab;
    [SerializeField] private Agent agentPrefab;

    private List<TerrainCustom> _terrains = new();
    private IAgent _agent;

    private void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Vector3 position = new Vector3(i * 12, j * 12, 0);
                TerrainCustom terrain = Instantiate(terrainPrefab, position, Quaternion.identity,
                    terrainContainer.transform);
                terrain.transform.localPosition = position;
                _terrains.Add(terrain);
            }
        }

        // Connect neighbors using connection points
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                TerrainCustom current = _terrains[i * 12 + j];
                if (i > 0) AddConnectionPoint(current, _terrains[(i - 1) * 12 + j]);
                if (i < 9) AddConnectionPoint(current, _terrains[(i + 1) * 12 + j]);
                if (j > 0) AddConnectionPoint(current, _terrains[i * 12 + (j - 1)]);
                if (j < 9) AddConnectionPoint(current, _terrains[i * 12 + (j + 1)]);
            }
        }

        // Get random terrain
        TerrainCustom randomTerrain = _terrains[Random.Range(0, _terrains.Count)];
        _agent = Instantiate(agentPrefab, randomTerrain.Point.transform.position, Quaternion.identity);
        Debug.Log("Agent position: " + _agent.GetGameObject().transform.position);
        TerrainCustom randomTerrain2 = _terrains[Random.Range(0, _terrains.Count)];
        Vector3 targetPosition = randomTerrain2.Point.transform.position;
        Debug.Log("Random terrain position: " + targetPosition, randomTerrain2.Point);

        // Use A* to find path
        AStarPathfinding pathfinding = new AStarPathfinding();
        List<TerrainCustom> path = pathfinding.FindPath(randomTerrain, randomTerrain2);

        // Add GoToCommand for each step in the path
        foreach (TerrainCustom step in path)
        {
            _agent.AddCommand(new GoToCommand(_agent.GetGameObject(), step.Point.transform.position));
        }
    }

    private void AddConnectionPoint(TerrainCustom from, TerrainCustom to)
    {
        ConnectionPoint connectionPoint = new ConnectionPoint
        {
            connectedTerrain = to,
            connectionTransform = to.Point.transform
        };
        from.connectionPoints.Add(connectionPoint);
    }
}