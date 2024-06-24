using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Consumables
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetMeshRenderers();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // base.Update();
        transform.Rotate(rotation * speed * Time.deltaTime);
        //ConsumableTimer(true);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.CompareTag("Player") && timeValue == MaxtimeValue))
    //    {
    //        flag = true;
    //        //healSound.PlayOneShot(healSound.clip);
    //        //consumable.GetComponent<MeshRenderer>().enabled = false;
    //        DisableNestedMeshRenderers();
    //        consumable.GetComponent<BoxCollider>().enabled = false;
    //        gameManager.wheelController.CollectSlugAmmo(10);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //healSound.PlayOneShot(healSound.clip);
            gameManager.wheelController.CollectSlugAmmo(10);
            gameManager.consumablesManager.slugCtr--;
            gameObject.SetActive(false);
           
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            gameManager.consumablesManager.slugCtr--;
            gameObject.SetActive(false);
        }
    }
}
