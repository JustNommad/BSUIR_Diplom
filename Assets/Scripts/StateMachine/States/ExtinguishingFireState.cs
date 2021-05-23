using UnityEngine;

public class ExtinguishingFireState : IRobotState
{
    private RobotResourses _robotResourses;
    private IStateMachine _stateMachine;
    private GameObject _targetObject;
    private float _fireStatus = float.MaxValue;
    public IStateMachine StateMachine => _stateMachine;

    public ExtinguishingFireState(GameObject targetObject = null)
    {
        _targetObject = targetObject;
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

        if (_targetObject == null)
            return;

        ExtiguishingFire();
        _robotResourses.Energy += ChangeRobotEnergy();

        if(!IsEnoughEnergy())
        {
            ChangeCurrentStateToEnergySearching();
        }

        _robotResourses.Water += ChangeWaterStatus();

        if(!IsEnoughWater())
        {
            ChangeCurrentStateToWaterSearching();
        }


    }

    public void TriggerEvent(GameObject objectTrigger)
    {
        _targetObject = objectTrigger;
    }

    private void ExtiguishingFire()
    {
        var fireParticleSystem = _targetObject.GetComponent<ParticleSystem>();
        var mainParticle = fireParticleSystem.main;
        var particleSize = mainParticle.startSize.constant;

        if (_fireStatus == float.MaxValue)
            _fireStatus = particleSize;

        var extiguishingPerSecond = particleSize / 5;

        _fireStatus -= extiguishingPerSecond;

        if(_fireStatus <= 0)
        {
            _targetObject.SetActive(false);

            ChangeCurrentStateToFireSearching();
        }
    }

    private int ChangeRobotEnergy()
    {
        return -5;
    }

    private bool IsEnoughEnergy()
    {
        if(_robotResourses.Energy <= 0)
        {
            return false;
        }

        return true;
    }

    private int ChangeWaterStatus()
    {
        return -10;
    }

    private bool IsEnoughWater()
    {
        if(_robotResourses.Water <= 0)
        {
            return false;
        }

        return true;
    }

    private void ChangeCurrentStateToFireSearching()
    {
        _robotResourses.LastRobotState = this;

        ChangeCurrentState(new SearchFireState());
    }

    private void ChangeCurrentStateToWaterSearching()
    {
        _robotResourses.LastRobotState = this;

        ChangeCurrentState(new SearchWaterState());
    }

    private void ChangeCurrentStateToEnergySearching()
    {
        _robotResourses.LastRobotState = this;

        ChangeCurrentState(new SearchEnergyState());
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
