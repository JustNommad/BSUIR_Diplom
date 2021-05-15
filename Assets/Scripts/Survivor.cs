using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField] private int _water;
    [SerializeField] private int _energy;
    [SerializeField] private float _speed = 3.0f;

    private Pathfinding _gameManager;
    private List<Node> _path;
    private int counter = 0;
    private Node temp = new Node();

    void Update()
    {
        if (_path == null)
        {
            InitPath();
        }
        else
        {
            MoveRobot();
        }
    }

    private void InitPath()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag.Contains("GameManager"))
            {
                _gameManager = obj.GetComponent<Pathfinding>();
            }
        }
        _path = _gameManager.FindPath();
    }

    private void MoveRobot()
    {
        if (counter == 0)
            temp = _path[counter++];
        if (transform.position.x == temp.Position.x && transform.position.z == temp.Position.z)
            if (counter < _path.Count)
                temp = _path[counter++];

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(temp.Position.x, temp.Position.y + 1, temp.Position.z), Time.deltaTime * _speed);

    }
}