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
                    if (!raycastHit.collider.gameObject.CompareTag("Ground") &&
                        !raycastHit.collider.gameObject.CompareTag("Water") &&
                        !raycastHit.collider.gameObject.CompareTag("Fire") &&
                        !raycastHit.collider.gameObject.CompareTag("Energy"))
                    {
                        Wall = false;
                    }

                    if (!wasYPosUpdate)
                    {
                        var result = raycastHit.point;
                        worldPoint.y += result.y;
                        wasYPosUpdate = true;
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
    public List<Node> GetNeighboringNodes(Node currentNode)
    {
        var neighborNodes = new List<Node>();

        for(var i = -1; i <= 1; i++)
        {
            for(var j = -1; j <= 1; j++)
            {
                var icheckX = currentNode.GridX + i;
                var icheckY = currentNode.GridY + j;

                if (icheckX >= 0 && icheckX < _gridSizeX &&
                    icheckY >= 0 && icheckY < _gridSizeY)
                {
                    neighborNodes.Add(_nodeArray[icheckX, icheckY]);
                }
            }
        }

        return neighborNodes;//Return the neighbors list.
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    //Gets the closest node to the given world position.
    {
        var ixPos = ((worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
        var iyPos = ((worldPosition.z + _gridWorldSize.y / 2) / _gridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        var ix = Mathf.RoundToInt((_gridSizeX - 1) * ixPos);
        var iy = Mathf.RoundToInt((_gridSizeY - 1) * iyPos);

        return _nodeArray[ix, iy];
    }
}