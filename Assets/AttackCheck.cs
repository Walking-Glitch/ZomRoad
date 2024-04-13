using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private Mutant undead;
    // Start is called before the first frame update
    void Start()
    {
        undead = GetComponentInParent<Mutant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            undead.SetIsInAttackArea(true);

            Debug.Log("player Hit");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            undead.SetIsInAttackArea(false);
            Debug.Log("player Left");
            //_enemy.SetCurrentTarget(null);
        }
    }
}
