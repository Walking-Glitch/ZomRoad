using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MedKit : Consumables
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
    //    if ((other.CompareTag("Player") && timeValue == MaxtimeValue) && gameManager.wheelController.health < 100)
    //    {
    //        flag = true;
    //        //healSound.PlayOneShot(healSound.clip);
    //        consumable.GetComponent<MeshRenderer>().enabled = false;
    //        consumable.GetComponent<BoxCollider>().enabled = false;
    //        gameManager.wheelController.Heal(50);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && gameManager.wheelController.health < 100))
        {
           
            //healSound.PlayOneShot(healSound.clip);
            gameManager.wheelController.Heal(50);
            gameManager.consumablesManager.medkitCtr--;
            gameObject.SetActive(false);
        }
    }
}
