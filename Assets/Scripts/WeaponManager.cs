using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] weapons;
    [SerializeField] protected GameObject[] AmmoUI;
    [SerializeField] protected GameObject[] WeaponsUI;

    [SerializeField] protected GameObject StatsPanelUI;
    [SerializeField] protected GameObject ActivatePromptUI;


    [SerializeField] private int j;

    private string wName;
    private int damage;
    private int fireRate;
    private int range;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        j = 0;
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
            AmmoUI[i].gameObject.SetActive(false);
            WeaponsUI[i].gameObject.SetActive(false);
        }

        StatsPanelUI.SetActive(false);
    }

    void SwapWeapons()
    {

        if (j >= weapons.Length)
        {
            j = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StatsPanelUI.SetActive(true);
            ActivatePromptUI.SetActive(false);

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == j)
                {
                    weapons[i].gameObject.SetActive(true);
                    AmmoUI[i].gameObject.SetActive(true);
                    WeaponsUI[i].gameObject.SetActive(true);
                    RefreshStats(i);
                }
                else
                {
                    weapons[i].gameObject.SetActive(false);
                    AmmoUI[i].gameObject.SetActive(false);
                    WeaponsUI[i].gameObject.SetActive(false);
                }
            }

            j++;
        }

    }

    void RefreshStats(int i)
    {
        wName = weapons[i].gameObject.GetComponent<Turret>().WeaponName;
        damage = weapons[i].gameObject.GetComponent<Turret>().WeaponDamage;
        range = weapons[i].gameObject.GetComponent<Turret>().WeaponRange;
        fireRate = weapons[i].gameObject.GetComponent<Turret>().WeaponFireRate;

        gameManager.uiManager.SetWeaponNameText(wName);
        gameManager.uiManager.SetDamage(damage);
        gameManager.uiManager.SetRange(range);
        gameManager.uiManager.SetFireRate(fireRate);

    }
}

