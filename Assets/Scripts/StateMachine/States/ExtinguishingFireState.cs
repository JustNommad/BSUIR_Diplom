using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingFireState : IRobotState
{
    private RobotResourses _robotResourses;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private void ExtiguishingFire()
    {

    }

    private int ChangeRobotEnergy()
    {
        return 0;
    }

    private bool IsEnoughEnergy()
    {
        return true;
    }

    private int ChangeWaterStatus()
    {
        return 0;
    }

    private bool IsEnoughWater()
    {
        return true;
    }

    private void ChangeCurrentStateToWaterSearching()
    {

    }

    private void ChangeCurrentStateToEnergySearching()
    {

    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
