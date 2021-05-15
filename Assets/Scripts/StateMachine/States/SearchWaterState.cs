using UnityEngine;

public class SearchWaterState : IRobotState
{
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private GameObject[] SearchWaterObjects()
    {
        return null;
    }

    private GameObject FindClosest(GameObject[] waterObjects)
    {
        return null;
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
