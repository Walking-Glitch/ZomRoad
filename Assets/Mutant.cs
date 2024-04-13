using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : UndeadBase
{
    public bool IsInAttackArea { get; set; }

    protected override void Start()
    {
        base.Start();
        
    }

    
    protected override void Update()
    {
        //base.Update();
        MutantMovement();
    }

    private void MutantMovement()
    {
        if (Time.frameCount % 10 == 0) // Update every 10 frames for example
        {
            isAgentOnNavMesh = IsAgentOnNavMesh(undead);
        }

        if (player != null && isAgentOnNavMesh) // Use cached result
        {
            undead.SetDestination(player.position);
            undead.isStopped = false;
        }


        if (!IsAgentOnNavMesh(undead) && !IsAgentOnOffMeshLink(undead))
        {
            CleanerDestroyZombie();
        }

        if (IsInAttackArea)
        {
            anim.SetBool("Attack", true);
            undead.isStopped = true;
        }

        else
        {
            anim.SetBool("Attack", false);
        }
    }

    private void Attack()
    {
        if (IsInAttackArea)
        {
            player.gameObject.GetComponent<Rigidbody>().AddForce((undead.transform.forward.normalized + undead.transform.up.normalized) * 5000f, ForceMode.Impulse);
        }

        else
        {
            Debug.Log("missed");
        }
      
    }

   
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            //ApplyKnockbackForce(-500f, new Vector3(25, 5, -50));
            PlayerDestroyZombie();
        }
    }

    protected override void PlayerDestroyZombie()
    {
        StartCoroutine(DelayDestruction(3f));

    }

    protected override IEnumerator DelayDestruction(float delay)
    {

        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        gameManager.enemyManager.DecreaseEnemyCtr();

    }

    protected override void CleanerDestroyZombie()
    {
        gameObject.SetActive(false);
        gameManager.enemyManager.DecreaseEnemyCtr();
    }

    public void SetIsInAttackArea(bool isInAttackArea)
    {
        IsInAttackArea = isInAttackArea;
    }
    




}
