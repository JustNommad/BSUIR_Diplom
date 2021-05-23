using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathfindingState : IRobotState
{
    private const int TimeMultiplier = 1000;

    private IStateMachine _stateMachine;
    private IRobotState _targetState;
    private GameObject _targetObject;
    private RobotResourses _robotResourses;
    private List<Node> _pathNodeList;
    private long _searchTime;
    private long _lastTime;
    public IStateMachine StateMachine => _stateMachine;

    public PathfindingState(GameObject targetObject, IRobotState targetState)
    {
        _targetObject = targetObject;
        _targetState = targetState;
    }

    public void Process(RobotResourses robotResourses)
    {
        if (_robotResourses == null)
        {
            _robotResourses = robotResourses;
            _stateMachine = _robotResourses.StateMachine;
        }

        if (_stateMachine == null)
            return;

        if (!IsEnoughEnergy() && _robotResourses.RobotMode > 0)
            ChangeStateToFindEnergy();

        if(_pathNodeList == null)
            _pathNodeList = FindPathToTarget();

        _robotResourses.Energy += ChangeEnergy();

        var currentTimeInMilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        if (currentTimeInMilliseconds < _lastTime + (_searchTime * TimeMultiplier))
            return;

        ChangeCurrentState(new MovementState(_pathNodeList, _targetState));
    }

    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private List<Node> FindPathToTarget()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var pathfindingObjectReferense = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Pathfinding>();
        var robotObject = _robotResourses.RobotObject;
        var pathList = pathfindingObjectReferense.FindPath(robotObject.transform, _targetObject.transform);

        stopWatch.Stop();
        _searchTime = stopWatch.ElapsedMilliseconds;
        _lastTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        UnityEngine.Debug.Log($"StopWatch: {_searchTime}");

        return pathList;
    }

    private int ChangeEnergy()
    {
        return -1;
    }

    private bool IsEnoughEnergy()
    {
        if (_robotResourses.Energy <= 0)
            return false;

        return true;
    }

    private void ChangeStateToFindEnergy()
    {
        _robotResourses.LastRobotState = _targetState;
        _robotResourses.RobotMode = 0;

        ChangeCurrentState(new SearchEnergyState());
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
