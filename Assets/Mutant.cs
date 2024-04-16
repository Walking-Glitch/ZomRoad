using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : UndeadBase
{
    public bool IsInAttackArea { get; set; }
    public AudioClip swingSfx;
    

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

        if (player != null && isAgentOnNavMesh && !isDead) // Use cached result
        {
            undead.SetDestination(player.position);
        }


        if (!IsAgentOnNavMesh(undead) && !IsAgentOnOffMeshLink(undead))
        {
            CleanerDestroyZombie();
        }

        if (IsInAttackArea && !isDead)
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
            gameManager.wheelController.PlaySfx();
            player.gameObject.GetComponent<Rigidbody>().AddForce((undead.transform.forward.normalized + undead.transform.up.normalized) * 5000f, ForceMode.Impulse);
        }

        else
        {
            Debug.Log("missed");
        }
      
    }

    private void PlaySfx()
    {
        audioSource.clip = swingSfx;
        audioSource.Play();
    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            isDead = true;
            audioSource.Stop();
            bloodVisualEffect.Play();
            undead.isStopped = true;
            anim.SetBool("Dead", true);
            PlayerDestroyZombie();

            return;
        }

        bloodVisualEffect.enabled = true;
        bloodVisualEffect.Play();
        anim.SetTrigger("Hit");



    }

    protected override void PlayerDestroyZombie()
    {
        StartCoroutine(DelayDestruction(3f));

    }

    protected override IEnumerator DelayDestruction(float delay)
    {

        yield return new WaitForSeconds(delay);

        bloodVisualEffect.enabled = false;
        health = maxHealth;
        isDead = false;
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
