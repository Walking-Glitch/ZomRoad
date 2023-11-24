using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Ranged Attack", menuName = "Survivor Logic/Attack Logic/Ranged Attack")]
public class SurvivorRangedAttack : SurvivorAttackSOBase
{
    public override void Initialize(GameObject gameObject, Survivor survivor)
    {
        base.Initialize(gameObject, survivor);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        survivor.animator.SetBool("isAttacking", true);
        survivor.aIPathScript.canMove = false;
        
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        transform.LookAt(enemyTransform);
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }
    
    public override void DoAnimationEventTriggerLogic(Survivor.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        survivor.animator.SetBool("isAttacking", false);
        survivor.aIPathScript.canMove = true;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
