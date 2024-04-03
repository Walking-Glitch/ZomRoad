using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GetRagdollBits();
        RagdollModeOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            zombie.destination = player.position;
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

        DestroyZombie();
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

    private void GetRagdollBits()
    {
        ragdollColliders = rig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }

    private void DestroyZombie()
    {
        Destroy(gameObject, 3.0f);

    }

}
