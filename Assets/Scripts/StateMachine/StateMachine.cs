using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateMachine
{
    private IRobotState _currentRobotState;
    private RobotResourses _robotResourses;
    public IRobotState CurrentRobotState => _currentRobotState;

    [SerializeField] private RobotConfig _robotConfig;

    private Pathfinding _gameManager;
    private List<Node> _path;
    private int counter = 0;
    private Node temp = new Node();

    void Awake()
    {
        _robotResourses = new RobotResourses
        {
            Water = _robotConfig.Water,
            Energy = _robotConfig.Energy,
            Speed = _robotConfig.Speed,
            MaxEnergy = _robotConfig.MaxEnergy,
            MaxWater = _robotConfig.MaxWater
        };
    }

    void Update()
    {
        CurrentRobotState.Process(_robotResourses);

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

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(temp.Position.x, temp.Position.y + 1, temp.Position.z), Time.deltaTime * _robotResourses.Speed);

    }

    public void ChangeCurrentState(IRobotState robotState)
    {
        _currentRobotState = robotState;
    }
}
