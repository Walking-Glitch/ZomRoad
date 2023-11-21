using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Walk", menuName = "Enemy Logic/Idle Logic/Random Walk")]
public class EnemyRandomPatrol : EnemyIdleSOBase
{
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.aIPathScript.maxSpeed = 1;
        Stop();
       
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Debug.Log("we are enemy patrol state");

        if (enemy.aIPathScript.reachedDestination)
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
        path.spread = 8000;

        // Get the Seeker component which must be attached to this GameObject
        Seeker seeker = gameObject.GetComponent<Seeker>();

        // Start the path and return the result to MyCompleteFunction (which is a function you have to define, the name can of course be changed)
        seeker.StartPath(path);
    }

    public void Stop()
    {
        enemy.aIPathScript.canMove = false;
        enemy.animator.SetBool("isPatrolling", false);
        enemy.animator.SetBool("isIdle", true);
    }

    public void Move()
    {
        enemy.animator.SetBool("isIdle", false);
        enemy.animator.SetBool("isPatrolling", true);

        enemy.aIPathScript.canMove = true;

        RandomPoint();
    }

    public override void DoAnimationEventTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);
        if (triggerType == Enemy.AnimationTriggerType.TriggerIdle1)
        {
            Move();
        }
       

    }

    public override void ResetValues()
    {
        base.ResetValues();
        enemy.animator.SetBool("isIdle", false);
        enemy.animator.SetBool("isPatrolling", false);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }


}
