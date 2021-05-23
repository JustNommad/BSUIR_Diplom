using UnityEngine;

public class SearchWaterState : IRobotState
{
    private const string WaterTagName = "Water";
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

        var objectArray = SearchWaterObjects();
        var closestObject = FindClosest(objectArray);

        ChangeCurrentState(new PathfindingState(closestObject, new GrabingWaterState()));
    }
    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private GameObject[] SearchWaterObjects()
    {
        return GameObject.FindGameObjectsWithTag(WaterTagName);
    }

    private GameObject FindClosest(GameObject[] waterObjects)
    {
        var robotPosition = _robotResourses.RobotObject.transform.position;
        var currentDistanse = float.MaxValue;
        var closestObject = _robotResourses.RobotObject;

        foreach (var gameObject in waterObjects)
        {
            var objectPostion = gameObject.transform.position;
            var heading = objectPostion - robotPosition;

            if(currentDistanse > heading.magnitude)
            {
                closestObject = gameObject;
            }
        }

        return closestObject;
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
