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
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

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
    }

    
}
