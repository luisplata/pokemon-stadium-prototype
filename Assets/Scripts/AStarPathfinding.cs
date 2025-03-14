using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    public List<TerrainCustom> FindPath(TerrainCustom startNode, TerrainCustom targetNode)
    {
        List<TerrainCustom> openSet = new List<TerrainCustom> { startNode };
        HashSet<TerrainCustom> closedSet = new HashSet<TerrainCustom>();
        Dictionary<TerrainCustom, TerrainCustom> cameFrom = new Dictionary<TerrainCustom, TerrainCustom>();
        Dictionary<TerrainCustom, float> gCost = new Dictionary<TerrainCustom, float>();
        Dictionary<TerrainCustom, float> fCost = new Dictionary<TerrainCustom, float>();

        gCost[startNode] = 0;
        fCost[startNode] = Heuristic(startNode, targetNode);

        while (openSet.Count > 0)
        {
            TerrainCustom currentNode = GetLowestFCostNode(openSet, fCost);
            if (currentNode == targetNode)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (ConnectionPoint connection in currentNode.connectionPoints)
            {
                TerrainCustom neighbor = connection.connectedTerrain;
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                float tentativeGCost = gCost[currentNode] + Vector3.Distance(currentNode.Point.transform.position, connection.connectionTransform.position);
                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGCost >= gCost[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = currentNode;
                gCost[neighbor] = tentativeGCost;
                fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, targetNode);
            }
        }

        return null; // No path found
    }

    private float Heuristic(TerrainCustom a, TerrainCustom b)
    {
        return Vector3.Distance(a.Point.transform.position, b.Point.transform.position);
    }

    private TerrainCustom GetLowestFCostNode(List<TerrainCustom> openSet, Dictionary<TerrainCustom, float> fCost)
    {
        TerrainCustom lowestFCostNode = openSet[0];
        foreach (TerrainCustom node in openSet)
        {
            if (fCost[node] < fCost[lowestFCostNode])
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }

    private List<TerrainCustom> ReconstructPath(Dictionary<TerrainCustom, TerrainCustom> cameFrom, TerrainCustom currentNode)
    {
        List<TerrainCustom> path = new List<TerrainCustom> { currentNode };
        while (cameFrom.ContainsKey(currentNode))
        {
            currentNode = cameFrom[currentNode];
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }
}