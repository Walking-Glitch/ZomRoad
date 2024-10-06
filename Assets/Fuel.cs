using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Consumables
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (IsFarFromPlayer())
        {
            DeactivateConsumable();
        }
    }

    private void DeactivateConsumable()
    {
        gameManager.consumablesManager.DecreaseFuelCtr();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && gameManager.wheelController.fuel < gameManager.wheelController.maxFuel))
        {
            gameManager.consumablesManager.PlayFuelRefillSFX();//
            gameManager.wheelController.RefillFuel(25);
            DeactivateConsumable();
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            DeactivateConsumable();
        }
    }
}
