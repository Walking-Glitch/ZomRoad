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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.consumablesManager.PlayAmmoRefillSFX();
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
