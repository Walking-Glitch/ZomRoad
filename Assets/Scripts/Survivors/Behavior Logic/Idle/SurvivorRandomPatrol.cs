using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Patrol", menuName = "Survivor Logic/Idle Logic/Random Patrol")]
public class SurvivorRandomPatrol : SurvivorIdleSOBase
{
    public override void Initialize(GameObject gameObject, Survivor survivor)
    {
        base.Initialize(gameObject, survivor);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        survivor.aIPathScript.maxSpeed = 1;
        Stop();

    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Debug.Log("we are survivor patrol state");

        if (survivor.aIPathScript.reachedDestination)
        {
            Stop();
        }
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    private void RandomPoint()
    {
        int theGScoreToStopAt = 10000;

        // Create a path object
        RandomPath path = RandomPath.Construct(transform.position, theGScoreToStopAt);
        // Determines the variation in path length that is allowed
        path.spread = 5000;

        // Get the Seeker component which must be attached to this GameObject
        Seeker seeker = gameObject.GetComponent<Seeker>();

        // Start the path and return the result to MyCompleteFunction (which is a function you have to define, the name can of course be changed)
        seeker.StartPath(path);
    }

    public void Stop()
    {
        survivor.aIPathScript.canMove = false;
        survivor.animator.SetBool("isPatrolling", false);
        survivor.animator.SetBool("isIdle", true);
    }

    public void Move()
    {
        survivor.animator.SetBool("isIdle", false);
        survivor.animator.SetBool("isPatrolling", true);

        survivor.aIPathScript.canMove = true;

        RandomPoint();
    }

    public override void DoAnimationEventTriggerLogic(Survivor.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);
        if (triggerType == Survivor.AnimationTriggerType.TriggerIdle1)
        {
            Move();
        }


    }

    public override void ResetValues()
    {
        base.ResetValues();
        survivor.animator.SetBool("isIdle", false);
        survivor.animator.SetBool("isPatrolling", false);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
