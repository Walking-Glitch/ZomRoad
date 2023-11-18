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
        enemy.aIPathScript.maxSpeed = 5;
        enemy.aIDestinationSetterScript.target = playerTransform;
        Shout();
        
       
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Debug.Log("we are chase state");

    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public void Shout()
    {
        transform.LookAt(playerTransform);

        enemy.aIPathScript.canMove = false;

        enemy.animator.SetBool("isSpotted", true);
        enemy.animator.SetBool("isChasing", false);
    }

    public void Chase()
    {
        enemy.aIPathScript.canMove = true;
       
        enemy.animator.SetBool("isChasing", true);
        enemy.animator.SetBool("isSpotted", false);


        enemy.aIPathScript.canMove = true;
    }
    
    public override void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);

       
        if (triggerType == Enemy.AnimationTriggerType.TriggerChase1)
        {
            Debug.Log("we are inside the enum trigger");
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
