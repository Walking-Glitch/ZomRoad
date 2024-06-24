using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCell : Consumables
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.CompareTag("Player") && timeValue == MaxtimeValue))
    //    {
    //        flag = true;
    //        //healSound.PlayOneShot(healSound.clip);
    //        consumable.GetComponent<MeshRenderer>().enabled = false;
    //        consumable.GetComponent<BoxCollider>().enabled = false;
    //        gameManager.wheelController.CollectEnergyAmmo(5);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //healSound.PlayOneShot(healSound.clip);
            gameManager.wheelController.CollectEnergyAmmo(5);
            gameManager.consumablesManager.energyCellCtr--;
            gameObject.SetActive(false);
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            gameManager.consumablesManager.energyCellCtr--;
            gameObject.SetActive(false);
        }
    }
}
