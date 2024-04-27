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
        ConsumableTimer();
    }

    private void ConsumableTimer()
    {
        if (flag && timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }

        else if (timeValue <= 0)
        {
            flag = false;
            timeValue = MaxtimeValue;
            consumable.GetComponent<MeshRenderer>().enabled = true;
            consumable.GetComponent<BoxCollider>().enabled = true;
        }

    }
   
}
