using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Melee", menuName = "Enemy Logic/Attack Logic/Attack Melee")]
public class EnemyAttackMelee : EnemyAttackSOBase
{
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
        enemy.animator.SetBool("isAttacking", true);

        enemy.aIPathScript.canMove = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        Debug.Log("we are attack state");

    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
        enemy.animator.SetBool("isAttacking", false);
        enemy.aIPathScript.canMove = true;
    }

    public void DealDamage(float damage)
    {
        if (enemy.currentTarget != null)
        {
            survivor.Damage(damage);
        }
        
    }

    public override void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);

        if (triggerType == Enemy.AnimationTriggerType.TriggerAttack1)
        {
            if (enemy.IsInAttackArea)
            {
                DealDamage(25);
                Debug.Log("ANIM EVENT HAS BEEN CALLED");
            }

        }
    }
}
