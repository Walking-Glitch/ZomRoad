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
         
        transform.Rotate(rotation * speed * Time.deltaTime);

        //if (IsDistanceTooGreat(playerPos, consumable.transform.position))
        //{
        //    gameManager.consumablesManager.bulletCtr--;
        //    this.gameObject.SetActive(false);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.consumablesManager.PlayAmmoRefillSFX();
            gameManager.wheelController.CollectBulletAmmo(20);
            gameManager.consumablesManager.DecreaseBulletCtr();
            gameObject.SetActive(false);
        }

        if (other.CompareTag("EnemyCleaner"))
        {
            gameManager.consumablesManager.DecreaseBulletCtr();
            gameObject.SetActive(false);
        }
    }
}
