using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class UndeadBase : MonoBehaviour
{
    [SerializeField] protected Transform player;

    public Collider mainCollider;

    public Animator anim;

    public GameObject rig;

    public Rigidbody[] knockbackGameObjects;

    protected NavMeshAgent undead;

    protected Collider[] ragdollColliders;

    protected Rigidbody[] ragdollRigidbodies;

    protected GameManager gameManager;

    protected bool isAgentOnNavMesh; // Cache the result of IsAgentOnNavMesh

    protected VisualEffect bloodVisualEffect;

    protected AudioSource audioSource;

    public int maxHealth;

    public int health;

    public bool isDead;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        undead = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bloodVisualEffect = GetComponentInChildren<VisualEffect>();
        bloodVisualEffect.enabled = false;
        audioSource = GetComponent<AudioSource>();

        health = maxHealth;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.frameCount % 10 == 0) // Update every 10 frames 
        {
            isAgentOnNavMesh = IsAgentOnNavMesh(undead);
        }

        if (player != null && isAgentOnNavMesh && !isDead) // Use cached result
        {
            undead.SetDestination(player.position);
        }


        if ((gameManager.wheelController.IsInvincible) || !IsAgentOnNavMesh(undead) && !IsAgentOnOffMeshLink(undead))
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

        if (IsAgentOnNavMesh(undead))
        {
            undead.isStopped = true;
        }

    }

    public virtual void ApplyKnockbackForce(float force, Vector3 bloodSpeed, bool explosion)
    {
        if (explosion)
        {
            Vector3 direction = transform.position - gameManager.EnergyGun.explosionObject.transform.position;//
            knockbackGameObjects[Random.Range(0, knockbackGameObjects.Length)].AddForce(direction.normalized * force, ForceMode.Impulse);
            bloodVisualEffect.SetVector3("BloodVelocity", bloodSpeed);
            bloodVisualEffect.enabled = true;

        }

        else
        {
            knockbackGameObjects[Random.Range(0, knockbackGameObjects.Length)].AddForce(gameObject.transform.forward.normalized * force, ForceMode.Impulse);
            bloodVisualEffect.SetVector3("BloodVelocity", bloodSpeed);
            bloodVisualEffect.enabled = true;
        }

       
    }

    protected virtual void RagdollModeOff()
    {
        isDead = false;

        health = maxHealth;

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

        if (IsAgentOnNavMesh(undead))
        {
            undead.isStopped = false;
        }

        bloodVisualEffect.enabled = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCleaner"))
        {
            CleanerDestroyZombie();
        }
    }

    protected void GetRagdollBits()
    {
        ragdollColliders = rig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }

    protected virtual void PlayerDestroyZombie()
    {
        StartCoroutine(DelayDestruction(3f));

    }

    protected virtual IEnumerator DelayDestruction(float delay)
    {

        yield return new WaitForSeconds(delay);
        RagdollModeOff();
        gameObject.SetActive(false);

        gameManager.enemyManager.DecreaseEnemyCtr();

    }

    protected virtual void CleanerDestroyZombie()
    {
        RagdollModeOff();
        gameObject.SetActive(false);
        gameManager.enemyManager.DecreaseEnemyCtr();
    }

    public virtual void TakeDamage(int damage, Vector3 bloodSpeed, bool explosion, float force)
    {
        health -= damage;

    }

    protected bool IsAgentOnNavMesh(NavMeshAgent agent)
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

    protected bool IsAgentOnOffMeshLink(NavMeshAgent agent)
    {
        return agent.isOnOffMeshLink;
    }
}
