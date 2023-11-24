using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform survivorTransform;
    protected Survivor survivor;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        //survivorTransform = GameObject.FindGameObjectWithTag("Survivor").transform;
        
    }

    public virtual void DoEnterLogic()
    {
        if (enemy.currentTarget != null)
        {
            survivorTransform = enemy.currentTarget.transform;
            survivor = enemy.currentTarget.GetComponent<Survivor>();
        }
        
    }
    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        if (!enemy.IsInAttackArea && enemy.currentTarget != null)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        else if (enemy.currentTarget == null && enemy.survivorList.Count == 0)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }

    public virtual void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
