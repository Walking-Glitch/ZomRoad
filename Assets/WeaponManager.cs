using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] weapons;
    [SerializeField] private int j = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisableWeapons();
    }

    void Update()
    {
        SwapWeapons();
    }

    void DisableWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    void SwapWeapons()
    {
     
        if (j >= weapons.Length)
        {
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == j)
                {
                    weapons[i].gameObject.SetActive(true);
                    //weapons[i].gameObject.GetComponentInChildren<Turret>().EnableLogic();
                }
                else
                {
                    weapons[i].gameObject.SetActive(false);
                    //weapons[i].gameObject.GetComponentInChildren<Turret>().DisableLogic();
                }
            }

            j++;
        }
        
    }
}
