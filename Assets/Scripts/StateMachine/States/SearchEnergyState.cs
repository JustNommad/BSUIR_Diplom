
using UnityEngine;

public class SearchEnergyState : IRobotState
{
    private const string EnergyTagName = "Energy";
    private IStateMachine _stateMachine;
    private RobotResourses _robotResourses;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        if (_robotResourses == null)
        {
            _robotResourses = robotResourses;
            _stateMachine = _robotResourses.StateMachine;
        }

        if (_stateMachine == null)
            return;

        var objectArray = SearchEnergyObjects();
        var closestObject = FindClosest(objectArray);

        ChangeCurrentState(new PathfindingState(closestObject, new ChargingState()));
    }

    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private GameObject[] SearchEnergyObjects()
    {
        return GameObject.FindGameObjectsWithTag(EnergyTagName);
    }

    private GameObject FindClosest(GameObject[] energyObjects)
    {
        var robotPosition = _robotResourses.RobotObject.transform.position;
        var currentDistanse = float.MaxValue;
        var closestObject = _robotResourses.RobotObject;

        foreach (var gameObject in energyObjects)
        {
            var objectPostion = gameObject.transform.position;
            var distance = Vector3.Distance(objectPostion, robotPosition);

            if (currentDistanse > distance)
            {
                closestObject = gameObject;
                currentDistanse = distance;
            }
        }

        return closestObject;
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _robotResourses.RobotMode = 1;
        _stateMachine.ChangeCurrentState(robotState);
    }
}
