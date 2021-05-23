using System.Collections.Generic;
using UnityEngine;

public class MovementState : IRobotState
{
    private List<Node> _pathList;
    private RobotResourses _robotResourses;
    private IRobotState _targetState;
    private IStateMachine _stateMachine;
    private int counter;
    private Node temp = new Node();
    private Transform _robotTransform;
    public IStateMachine StateMachine => _stateMachine;

    public MovementState(List<Node> pathList, IRobotState targetState)
    {
        _pathList = pathList;
        _targetState = targetState;
    }

    public void Process(RobotResourses robotResourses)
    {
        if (_robotResourses == null)
        {
            _robotResourses = robotResourses;
            _robotTransform = _robotResourses.RobotObject.transform;
            _stateMachine = _robotResourses.StateMachine;
        }

        if (_stateMachine == null)
            return;

        MoveRobot();

        if (_robotTransform.position == _pathList[_pathList.Count - 1].Position)
        {
            ChangeCurrentState(_targetState);
        }
    }
    
    private void MoveRobot()
    {
        var robotPosition = _robotTransform.position;

        if (counter == 0)
        {
            temp = _pathList[counter++];
        }

        if (_robotTransform.position.x == temp.Position.x &&
            _robotTransform.position.z == temp.Position.z &&
            counter < _pathList.Count)
        {
            temp = _pathList[counter++];
        }

        _robotTransform.position = Vector3.MoveTowards(
            robotPosition, 
            new Vector3(temp.Position.x, temp.Position.y + 1, temp.Position.z),
            Time.deltaTime * _robotResourses.Speed);
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }

    public void TriggerEvent(GameObject objectTrigger)
    {
        if(objectTrigger.CompareTag("Energy") && _targetState is ChargingState)
        {
            ChangeCurrentState(_targetState);
        }

        if (objectTrigger.CompareTag("Water") && _targetState is GrabingWaterState)
        {
            ChangeCurrentState(_targetState);
        }

        if (objectTrigger.CompareTag("Fire") && _targetState is ExtinguishingFireState)
        {
            ChangeCurrentState(_targetState);
        }

    }
}
