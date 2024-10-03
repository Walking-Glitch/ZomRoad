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

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && gameManager.wheelController.health < gameManager.wheelController.maxHealth))
        {
            gameManager.consumablesManager.PlayHealthRefillSFX();
            gameManager.wheelController.Heal(30);
            gameManager.consumablesManager.DecreaseMedkitCtr();
            gameObject.SetActive(false);
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            gameManager.consumablesManager.DecreaseMedkitCtr();
            this.gameObject.SetActive(false);
        }
    }

}
