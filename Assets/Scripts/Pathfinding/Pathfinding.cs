using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour  
{
    private PathArea _pathAreaReference;

    private void Awake()
    {
        _pathAreaReference = GetComponent<PathArea>();
    }

    public List<Node> FindPath(Transform startPosition, Transform targetPosition)
    {
        var startNode = _pathAreaReference.NodeFromWorldPoint(startPosition.position);
        var targetNode = _pathAreaReference.NodeFromWorldPoint(targetPosition.position);

        var openList = new List<Node>();
        var closedList = new HashSet<Node>();

        openList.Add(startNode);

        while(openList.Count > 0)
        {
            var currentNode = openList[0];

            for(int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].IhCost < currentNode.IhCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                return GetFinalPath(startNode, targetNode);
            }

            foreach (var neighborNode in _pathAreaReference.GetNeighboringNodes(currentNode))
            {
                if (!neighborNode.Wall || closedList.Contains(neighborNode))
                    continue;

                var moveCost = currentNode.IgCost + GetManhattenDistance(currentNode, neighborNode);

                if (moveCost < neighborNode.IgCost || !openList.Contains(neighborNode))
                {
                    neighborNode.IgCost = moveCost;
                    neighborNode.IhCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if(!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    private List<Node> GetFinalPath(Node startingNode, Node endNode)
    {
        var finalPath = new List<Node>();
        var currentNode = endNode;

        while(currentNode != startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();

        _pathAreaReference.FinalPath = finalPath;

        return finalPath;
    }

    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        var ix = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        var iy = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        return ix + iy;
    }
}