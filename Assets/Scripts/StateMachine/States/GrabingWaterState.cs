using UnityEngine;

public class GrabingWaterState : IRobotState
{
    private const int WaterPerSecond = 20;

    private RobotResourses _robotResourses;
    private Transform _fireTarget;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public GrabingWaterState(Transform fireTarget = null)
    {
        _fireTarget = fireTarget;
    }

    public void Process(RobotResourses robotResourses)
    {
        if(_robotResourses == null)
        {
            _robotResourses = robotResourses;
            _stateMachine = _robotResourses.StateMachine;
        }

        if (_stateMachine == null)
            return;

        GrabWater();
        _robotResourses.Energy += ChangeRobotEnergy();

        if(!IsEnoughEnergy())
        {
            ChangeStateToFindEnergy();
        }

        if(_robotResourses.Water >= _robotResourses.MaxWater)
        {
            _robotResourses.Water = _robotResourses.MaxWater;

            if(_fireTarget != null)
            {
                ChangeStateToPathfinding();
            }

            ChangeStateToFindFire();
        }
    }

    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private void GrabWater()
    {
        _robotResourses.Water += WaterPerSecond;
    }

    private int ChangeRobotEnergy()
    {
        return -3;
    }

    private bool IsEnoughEnergy()
    {
        if (_robotResourses.Energy <= 0)
            return false;

        return true;
    }

    private void ChangeStateToFindEnergy()
    {
        _robotResourses.LastRobotState = this;

        ChangeCurrentState(new SearchEnergyState());
    }

    private void ChangeStateToFindFire()
    {
        ChangeCurrentState(new SearchFireState());
    }

    private void ChangeStateToPathfinding()
    {
        ChangeCurrentState(new PathfindingState(_fireTarget.gameObject, new ExtinguishingFireState()));
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
