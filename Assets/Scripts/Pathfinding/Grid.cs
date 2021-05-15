using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;

    [SerializeField] private Vector2 _gridWorldSize;

    protected Node[,] _nodeArray;
    protected float _nodeDiameter;
    protected int _gridSizeX, _gridSizeY;

    protected GridConfig GridConfig => _gridConfig;
    protected Vector2 GridWorldSize => _gridWorldSize;

    private void Awake()
    {
        _nodeDiameter = _gridConfig.NodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
    }

    protected void CreateGrid()
    {
        _nodeArray = new Node[_gridSizeX, _gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;
       
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                UseRayCast(bottomLeft, x, y);
            }
        }
    }

    private void UseRayCast(Vector3 bottomLeft, int x, int y)
    {
        Vector3 worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + _gridConfig.NodeRadius) + Vector3.forward * (y * _nodeDiameter + _gridConfig.NodeRadius);
        bool Wall = true;
        var raycastHit = new RaycastHit();
        var wasYPosUpdate = false;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Physics.Raycast(worldPoint + new Vector3(_gridConfig.NodeRadius * i, 999, _gridConfig.NodeRadius * j), Vector3.down, out raycastHit, Mathf.Infinity))
                {
                    if(!wasYPosUpdate)
                    {
                        var result = raycastHit.point;
                        worldPoint.y += result.y;
                        wasYPosUpdate = true;
                    }

                    if (!raycastHit.collider.gameObject.CompareTag("Ground"))
                    {
                        Wall = false;
                    }
                }
            }
        }

        _nodeArray[x, y] = new Node(Wall, worldPoint, x, y);
    }

    protected void CreateNeturalAreas()
    {
        if (_gridSizeY == 0 || _gridSizeX == 0)
            return;

        _nodeArray = new Node[_gridSizeX, _gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + _gridConfig.NodeRadius) + Vector3.forward * (y * _nodeDiameter + _gridConfig.NodeRadius);
                bool Wall = false;

                _nodeArray[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();//Make a new list of all available neighbors.
        int icheckX;//Variable to check if the XPosition is within range of the node array to avoid out of range errors.
        int icheckY;//Variable to check if the YPosition is within range of the node array to avoid out of range errors.

        //Check the right side of the current node.
        icheckX = a_NeighborNode.GridX + 1;
        icheckY = a_NeighborNode.GridY;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current node.
        icheckX = a_NeighborNode.GridX - 1;
        icheckY = a_NeighborNode.GridY;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current node.
        icheckX = a_NeighborNode.GridX;
        icheckY = a_NeighborNode.GridY + 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current node.
        icheckX = a_NeighborNode.GridX;
        icheckY = a_NeighborNode.GridY - 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        icheckX = a_NeighborNode.GridX - 1;
        icheckY = a_NeighborNode.GridY - 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        icheckX = a_NeighborNode.GridX + 1;
        icheckY = a_NeighborNode.GridY - 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        icheckX = a_NeighborNode.GridX - 1;
        icheckY = a_NeighborNode.GridY + 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        icheckX = a_NeighborNode.GridX + 1;
        icheckY = a_NeighborNode.GridY + 1;
        if (icheckX >= 0 && icheckX < _gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < _gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(_nodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        return NeighborList;//Return the neighbors list.
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    //Gets the closest node to the given world position.
    {
        float ixPos = ((worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
        float iyPos = ((worldPosition.z + _gridWorldSize.y / 2) / _gridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((_gridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((_gridSizeY - 1) * iyPos);

        return _nodeArray[ix, iy];
    }
}