using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Collider mainCollider;

    public Animator anim;

    public GameObject rig;

    private NavMeshAgent zombie;

    private Collider[] ragdollColliders;

    private Rigidbody[] ragdollRigidbodies;

    private GameManager gameManager;
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
        if (player != null && IsAgentOnNavMesh(zombie))
        {
            zombie.destination = player.position;
        }

        if (!IsAgentOnNavMesh(zombie) && !IsAgentOnOffMeshLink(zombie))
        {
            CleanerDestroyZombie();
        }
        
    }

    private void RagdollModeOn()
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

        zombie.isStopped = true;

        PlayerDestroyZombie();
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

        zombie.isStopped = false;
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

    private void PlayerDestroyZombie()
    {
        gameManager.enemyManager.DecreaseEnemyCtr();
        Destroy(gameObject, 3.0f);
    }

    private void CleanerDestroyZombie()
    {
        gameManager.enemyManager.DecreaseEnemyCtr();
        Destroy(gameObject);
    }

    bool IsAgentOnNavMesh(NavMeshAgent agent)
    {
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 0.15f, NavMesh.AllAreas))
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
