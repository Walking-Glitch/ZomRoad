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
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
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
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
