
using UnityEngine;

public class SearchEnergyState : IRobotState
{
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private GameObject[] SearchEnergyObjects()
    {
        return null;
    }

    private GameObject FindClosest(GameObject[] energyStations)
    {
        return null;
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
