using UnityEngine;

public class NeturalAreas : Grid
{
    [SerializeField] private AreaTypes _areaTypes;
    public AreaTypes AreaTypes => _areaTypes;
    public Node[,] NeturalNodes => _nodeArray;

    private void Start()
    {
        CreateNeturalAreas();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector
    }
}
