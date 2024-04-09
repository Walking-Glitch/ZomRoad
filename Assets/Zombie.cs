using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Collider mainCollider;

    public Animator anim;

    public GameObject rig;

    public Rigidbody [] knockbackGameObjects;

    private NavMeshAgent zombie;

    private Collider[] ragdollColliders;

    private Rigidbody[] ragdollRigidbodies;

    private GameManager gameManager;

    private bool isAgentOnNavMesh; // Cache the result of IsAgentOnNavMesh

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        zombie = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GetRagdollBits();
        RagdollModeOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 10 == 0) // Update every 10 frames for example
        {
            isAgentOnNavMesh = IsAgentOnNavMesh(zombie);
        }

        if (player != null && isAgentOnNavMesh) // Use cached result
        {
            zombie.SetDestination(player.position);
        }

        //if (player != null && IsAgentOnNavMesh(zombie))
        //{
             
        //    zombie.SetDestination(player.position);
        //}

        if (!IsAgentOnNavMesh(zombie) && !IsAgentOnOffMeshLink(zombie))
        {
            CleanerDestroyZombie();
        }

    }

    public void RagdollModeOn()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigid in ragdollRigidbodies)
        {
            rigid.isKinematic = false;
        }

        anim.enabled = false;

        mainCollider.enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        if (IsAgentOnNavMesh(zombie))
        {
            zombie.isStopped = true;
        }
 
        PlayerDestroyZombie();
    }

    public void ApplyKnockbackForce()
    {
        knockbackGameObjects[Random.Range(0, knockbackGameObjects.Length)].AddForce(gameObject.transform.forward.normalized * -1000f, ForceMode.Impulse);
    }

    private void RagdollModeOff()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rigid in ragdollRigidbodies)
        {
            rigid.isKinematic = true;
        }

        anim.enabled = true;

        mainCollider.enabled = true;

        GetComponent<Rigidbody>().isKinematic = false;

        if (IsAgentOnNavMesh(zombie))
        {
            zombie.isStopped = false;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RagdollModeOn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCleaner"))
        {
            CleanerDestroyZombie();
        }
    }

    private void GetRagdollBits()
    {
        ragdollColliders = rig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }

    public void PlayerDestroyZombie()
    {
        StartCoroutine(DelayAction(3f));
        
    }

    private IEnumerator DelayAction(float delay)
    {

        yield return new WaitForSeconds(delay);
        RagdollModeOff();
        gameObject.SetActive(false);
        gameManager.enemyManager.DecreaseEnemyCtr();
        //Destroy(gameObject);
    }

    private void CleanerDestroyZombie()
    {
        //RagdollModeOff();
        gameObject.SetActive(false);
        gameManager.enemyManager.DecreaseEnemyCtr();
        
        
        //Destroy(gameObject);
    }

    bool IsAgentOnNavMesh(NavMeshAgent agent)
    {
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsAgentOnOffMeshLink(NavMeshAgent agent)
    {
        return agent.isOnOffMeshLink;
    }
}
