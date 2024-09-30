using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected float speed; 
    protected GameObject consumable;

    [SerializeField] protected float timeValue = MaxtimeValue;
   

    public const float MaxtimeValue = 5;
    protected bool flag;

    protected MeshRenderer[] meshRenderers;

    

    protected GameManager gameManager;

    protected Vector3 playerPos;

    // Update is called once per frame

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        playerPos = gameManager.wheelController.transform.position;
        consumable = gameObject;
    }
    protected virtual void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }

    protected virtual void GetMeshRenderers()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    protected virtual void EnableNestedMeshRenderers()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
    }

    protected virtual void DisableNestedMeshRenderers()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }
    protected virtual void ConsumableTimer(bool isNestedMesh)
    {
        if (flag && timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }

        else if (timeValue <= 0)
        {
            flag = false;
            timeValue = MaxtimeValue;
            if (isNestedMesh)
            {
                EnableNestedMeshRenderers();
            }
            else
            {
                consumable.GetComponent<MeshRenderer>().enabled = true;
            }

            consumable.GetComponent<BoxCollider>().enabled = true;

        }

    }

    //protected virtual bool IsDistanceTooGreat(Vector3 player, Vector3 cons)
    //{
    //    float distance = Vector3.Distance(player, cons);

    //    if (distance > 100)
    //    {
    //        return true;
    //    }

    //    return false;
    //}


}
