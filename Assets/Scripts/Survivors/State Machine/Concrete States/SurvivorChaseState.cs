using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorChaseState : SurvivorState
{
    public SurvivorChaseState(Survivor survivor, SurvivorStateMachine survivorStateMachine) : base(survivor, survivorStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        survivor.SurvivorChaseBaseInstance.DoEnterLogic();
    }

    public override void AnimationTriggerEvent(Survivor.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        survivor.SurvivorChaseBaseInstance.DoAnimationEventTriggerLogic(triggerType);

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        survivor.SurvivorChaseBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        survivor.SurvivorChaseBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        survivor.SurvivorChaseBaseInstance.DoExitLogic();
    }
}
