using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Consumables
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
        base.Update();
        if (IsFarFromPlayer())
        {
            DeactivateConsumable();
        }
    }

    private void DeactivateConsumable()
    {
        gameManager.consumablesManager.DecreaseBulletCtr();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.consumablesManager.PlayAmmoRefillSFX();
            gameManager.wheelController.CollectBulletAmmo(40);
            DeactivateConsumable();
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            DeactivateConsumable();
        }
    }
}
