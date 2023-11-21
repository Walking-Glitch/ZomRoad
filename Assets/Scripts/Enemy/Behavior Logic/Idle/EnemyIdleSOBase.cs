using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        //survivorTransform = GameObject.FindGameObjectWithTag("Survivor").transform;
        //survivorTransform = enemy.CurrentTarget.transform; NOT NEEDED NOW FOR PATROLLING
    }

    public virtual void DoEnterLogic() {}
    public virtual void DoExitLogic() { ResetValues();}

    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.IsInDetectionArea && enemy.currentTarget != null)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }
    public virtual void DoPhysicsUpdateLogic() {}

    public virtual void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType){}
    public virtual void ResetValues(){}


}
