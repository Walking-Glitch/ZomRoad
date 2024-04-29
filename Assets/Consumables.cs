using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected float speed; 
    protected GameObject consumable;

    [SerializeField] protected float timeValue = MaxtimeValue;
    private AudioSource healSound;
    public const float MaxtimeValue = 5;
    protected bool flag;

    protected MeshRenderer[] meshRenderers;

    protected GameManager gameManager;

    // Update is called once per frame

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        consumable = gameObject;
        healSound = GetComponent<AudioSource>();
    }
    protected virtual void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
        ConsumableTimer(false);
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
   
}
