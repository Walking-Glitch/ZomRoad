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
        if (IsFarFromPlayer())
        {
            DeactivateConsumable();
        }
    }

    private void DeactivateConsumable()
    {
        gameManager.consumablesManager.DecreaseEnergyCellCtr();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.consumablesManager.PlayAmmoRefillSFX();
            gameManager.wheelController.CollectEnergyAmmo(3);
            DeactivateConsumable();
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            DeactivateConsumable();
        }
    }
}
