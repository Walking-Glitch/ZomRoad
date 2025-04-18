using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Survivor Logic/Chase Logic/Direct Chase")]
public class SurvivorDirectChase : SurvivorChaseSOBase
{

    public override void Initialize(GameObject gameObject, Survivor survivor)
    {
        base.Initialize(gameObject, survivor);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Chase();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public void Chase()
    {
        survivor.animator.SetBool("isChasing", true);
        survivor.aIPathScript.canMove = true;
        survivor.aIPathScript.maxSpeed = 5;
        survivor.aIDestinationSetterScript.target = enemyTransform;

    }
    public override void DoAnimationEventTriggerLogic(Survivor.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        survivor.animator.SetBool("isChasing", false);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
