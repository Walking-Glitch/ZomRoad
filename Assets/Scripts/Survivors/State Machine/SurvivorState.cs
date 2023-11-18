using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorState
{
    protected Survivor survivor;
    protected SurvivorStateMachine survivorStateMachine;

    public SurvivorState(Survivor survivor, SurvivorStateMachine survivorStateMachine)
    {
        this.survivor = survivor;
        this.survivorStateMachine = survivorStateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(Survivor.AnimationTriggerType triggerType) { }
}
