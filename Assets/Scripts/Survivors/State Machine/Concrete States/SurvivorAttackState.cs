using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorAttackState : SurvivorState
{
    public SurvivorAttackState(Survivor survivor, SurvivorStateMachine survivorStateMachine) : base(survivor, survivorStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        survivor.SurvivorAttackBaseInstance.DoEnterLogic();
    }

    public override void AnimationTriggerEvent(Survivor.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        survivor.SurvivorAttackBaseInstance.DoAnimationEventTriggerLogic(triggerType);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        survivor.SurvivorAttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        survivor.SurvivorAttackBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        survivor.SurvivorAttackBaseInstance.DoExitLogic();
    }
}
