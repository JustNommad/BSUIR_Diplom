using UnityEngine;

public interface IRobotState
{
    IStateMachine StateMachine { get; }
    void Process(RobotResourses robotResourses);
    void TriggerEvent(GameObject objectTrigger);
}
