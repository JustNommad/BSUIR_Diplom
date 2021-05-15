using System;
using System.Collections.Generic;
using UnityEngine;

public class PathArea : Grid
{
    [SerializeField] private NeturalAreas[] _naturalAreas;
    [SerializeField] private bool _useGizmos;

    private const float MaxMovementAngle = 0.4f;
    private const int UssualCost = 10;

    private bool wasInitNeturalArea = false;

    public List<Node> FinalPath;

    private void Start()
    {
        CreateGrid();
        CheckingUnevenSurfaces();
    }

    private void Update()
    {
        if (!wasInitNeturalArea)
            InitNaturalAreaCost();
    }

    private void CheckingUnevenSurfaces()
    {
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                var currentCost = UssualCost;

                if (_nodeArray[x, y].Wall == false && CanWalkOnThisPoint(x, y, ref currentCost))
                {
                    _nodeArray[x, y].Wall = true;
                    _nodeArray[x, y].Cost = currentCost;
                }
            }
        }
    }

    private bool CanWalkOnThisPoint(int x, int y, ref int currentCost)
    {
        if (!IsOutBounds(x, y))
            return false;

        for (int offsetX = -1; offsetX <= 1; offsetX++)
        {
            for (int offsetY = -1; offsetY <= 1; offsetY++)
            {
                if (HightDifference(_nodeArray[x, y].Position, _nodeArray[offsetX + x, offsetY + y].Position, ref currentCost) > MaxMovementAngle)
                    return false;
            }
        }

        return true;
    }

    private bool IsOutBounds(int x, int y)
    {
        return x > 0 && y < _gridSizeY- 1 && y > 0 && x < _gridSizeX - 1;
    }

    private float HightDifference(Vector3 currentVector, Vector3 otherVector, ref int currentCost)
    {
        var result = currentVector.y - otherVector.y;
        currentCost += Mathf.Abs(Mathf.RoundToInt(result * 10));

        return result;
    }

    private void InitNaturalAreaCost()
    {
        foreach (var neturalArea in _naturalAreas)
        {
            foreach (var pathNode in _nodeArray)
            {
                if(IsOnNeturalArea(neturalArea, pathNode))
                {
                    pathNode.AreaCost = (int) neturalArea.AreaTypes;
                }
            }
        }

        wasInitNeturalArea = true;
    }

    private bool IsOnNeturalArea(NeturalAreas neturalArea, Node pathNode)
    {
        var neturalEndXIndex = neturalArea.NeturalNodes.GetUpperBound(0);
        var neturalEndYIndex = neturalArea.NeturalNodes.GetUpperBound(1);

        var neturalFirstPosition = neturalArea.NeturalNodes[0, 0].Position;
        var neturalEndPosition = neturalArea.NeturalNodes[neturalEndXIndex, neturalEndYIndex].Position;

        var pathNodePosition = pathNode.Position;

        if (neturalFirstPosition.x < pathNodePosition.x && neturalFirstPosition.z < pathNodePosition.z)
        {
            if (neturalEndPosition.x > pathNodePosition.x && neturalEndPosition.z > pathNodePosition.z)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (!_useGizmos)
            return;

        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector

        if (_nodeArray != null)//If the grid is not empty
        {
            foreach (Node n in _nodeArray)//Loop through every node in the grid
            {
                if (n.Wall)//If the current node is a wall node
                {
                    if (n.Cost > 10)
                    {
                        Gizmos.color = Color.green;
                    }
                    else if(n.AreaCost > 10)
                    {
                        Gizmos.color = Color.blue;
                    }
                    else
                    {
                        Gizmos.color = Color.white;//Set the color of the node
                    }
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the node
                }


                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that node
                    }

                }

                Gizmos.DrawCube(n.Position, Vector3.one * (_nodeDiameter - GridConfig.DistanceBetweenNodes));//Draw the node at the position of the node.
            }
        }
    }
}
