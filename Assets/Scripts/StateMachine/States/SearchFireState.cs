using UnityEngine;

public class SearchFireState : IRobotState
{
    private const string FireTagName = "Fire";
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

        var objectArray = SearchFireObjects();
        var closestObject = FindClosest(objectArray);

        ChangeCurrentState(new PathfindingState(closestObject, new ExtinguishingFireState()));
    }
    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private GameObject[] SearchFireObjects()
    {
        return GameObject.FindGameObjectsWithTag(FireTagName);
    }

    private GameObject FindClosest(GameObject[] fireObjects)
    {
        var robotPosition = _robotResourses.RobotObject.transform.position;
        var currentDistanse = float.MaxValue;
        var closestObject = _robotResourses.RobotObject;

        foreach (var gameObject in fireObjects)
        {
            var objectPostion = gameObject.transform.position;
            var heading = objectPostion - robotPosition;

            if (!gameObject.activeSelf)
                continue;

            if (currentDistanse > heading.magnitude)
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
