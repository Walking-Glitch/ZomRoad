using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorChaseSOBase : ScriptableObject
{
    protected Survivor survivor;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform enemyTransform;

    public virtual void Initialize(GameObject gameObject, Survivor survivor)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.survivor = survivor;

        //enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    public virtual void DoEnterLogic()
    {
        if (survivor.currentTarget != null)
            enemyTransform = survivor.currentTarget.transform;
    }
    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        if (survivor.IsInAttackArea && survivor.currentTarget != null)
        {
            survivor.StateMachine.ChangeState(survivor.AttackState);
        }

        else if (survivor.currentTarget == null && survivor.enemyList.Count == 0)
        {
            survivor.StateMachine.ChangeState(survivor.IdleState);
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }

    public virtual void DoAnimationEventTriggerLogic(Survivor.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }
}
