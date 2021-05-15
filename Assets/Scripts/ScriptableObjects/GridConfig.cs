using UnityEngine;

[CreateAssetMenu(fileName = "Grid Config")]
public class GridConfig : ScriptableObject
{
    [SerializeField] private float _nodeRadius;

    [SerializeField] private float _distanceBetweenNodes;

    public float NodeRadius => _nodeRadius;
    public float DistanceBetweenNodes => _distanceBetweenNodes;
}
