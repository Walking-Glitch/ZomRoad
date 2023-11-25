using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Ranged Attack", menuName = "Survivor Logic/Attack Logic/Ranged Attack")]
public class SurvivorRangedAttack : SurvivorAttackSOBase
{
    
    public LayerMask obstacleLayerMask;
    public override void Initialize(GameObject gameObject, Survivor survivor)
    {
        base.Initialize(gameObject, survivor);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        survivor.animator.SetBool("isAttacking", true);
        survivor.aIPathScript.canMove = false;
        
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        transform.LookAt(enemyTransform);
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }
    
    public override void DoAnimationEventTriggerLogic(Survivor.AnimationTriggerType triggerType)
    {
        base.DoAnimationEventTriggerLogic(triggerType);

        if (triggerType == Survivor.AnimationTriggerType.TriggerAttack1)
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        if (enemy != null)
        {

       
        Vector3 direction = (enemyTransform.position + new Vector3(0,0.4f,0) )- survivor.gunBarrel.transform.position;
        
        RaycastHit hit;

        if (Physics.Raycast(survivor.gunBarrel.transform.position, direction, out hit, Mathf.Infinity))
        {
            // If the ray hits an object on the specified layer mask
            Debug.DrawRay(survivor.gunBarrel.transform.position, direction, Color.red); // For visualization purposes
            Debug.Log("SHOOT IS BEING CALLED INSIDE FIRST IF");
            // Example: handle what happens when the ray hits an object (e.g., apply damage)
            if (hit.collider.isTrigger)//hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("SHOOT IS BEING CALLED INSIDE IF");
                Debug.DrawRay(survivor.gunBarrel.transform.position, direction, Color.black);
                enemy.Damage(25f);
            }
        }
        else
        {
            Debug.Log("SHOOT IS BEING CALLED FROM ELSE");
            // If the ray reaches the target point without hitting anything
            Debug.DrawRay(survivor.gunBarrel.transform.position, direction, Color.green); // For visualization purposes

            // You might want to handle the case where the ray reaches the target without hitting anything
        }

        }

    }
    public override void ResetValues()
    {
        base.ResetValues();
        survivor.animator.SetBool("isAttacking", false);
        survivor.aIPathScript.canMove = true;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
