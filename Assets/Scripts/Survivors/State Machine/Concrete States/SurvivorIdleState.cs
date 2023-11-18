using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorIdleState : SurvivorState
{
    public SurvivorIdleState(Survivor survivor, SurvivorStateMachine survivorStateMachine) : base(survivor, survivorStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        survivor.SurvivorIdleBaseInstance.DoEnterLogic();
    }

    public override void AnimationTriggerEvent(Survivor.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        survivor.SurvivorIdleBaseInstance.DoAnimationEventTriggerLogic(triggerType);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        survivor.SurvivorIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        survivor.SurvivorIdleBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        survivor.SurvivorIdleBaseInstance.DoExitLogic();
    }

    
}
