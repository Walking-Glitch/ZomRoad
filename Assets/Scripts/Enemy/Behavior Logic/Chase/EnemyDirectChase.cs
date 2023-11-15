using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyDirectChase : EnemyChaseSOBase
{
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.aIDestinationSetterScript.target = playerTransform;

        enemy.animator.SetBool("isChasing", true);

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

        enemy.animator.SetBool("isChasing", false);
    }

     
}
