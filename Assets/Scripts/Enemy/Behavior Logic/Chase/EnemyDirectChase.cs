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
        Shout();
        
       
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public void Shout()
    {
        enemy.aIPathScript.canMove = false;

        enemy.animator.SetBool("isSpotted", true);
        enemy.animator.SetBool("isChasing", false);
    }

    public void Chase()
    {
        enemy.aIPathScript.canMove = true;
        Debug.Log("WE ADASD  CHECKING");
        enemy.animator.SetBool("isChasing", true);
        enemy.animator.SetBool("isSpotted", false);

        

        enemy.aIPathScript.canMove = true;
    }

    public override void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);

        if (triggerType == Enemy.AnimationTriggerType.TriggerChase1)
        {
            Chase();
        }
       
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();

        enemy.animator.SetBool("isChasing", false);
        enemy.animator.SetBool("isSpotted", false);
    }

     
}
