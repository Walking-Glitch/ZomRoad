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
        RandomPoint();

        enemy.animator.SetBool("isIdle", true);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (enemy.aIPathScript.reachedDestination)
        {
            RandomPoint();
        }
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
        enemy.animator.SetBool("isIdle", false);
    }

    private void RandomPoint()
    {
        int theGScoreToStopAt = 50000;

        // Create a path object
        RandomPath path = RandomPath.Construct(transform.position, theGScoreToStopAt);
        // Determines the variation in path length that is allowed
        path.spread = 5000;

        // Get the Seeker component which must be attached to this GameObject
        Seeker seeker = gameObject.GetComponent<Seeker>();

        // Start the path and return the result to MyCompleteFunction (which is a function you have to define, the name can of course be changed)
        seeker.StartPath(path);
       
    }
}
