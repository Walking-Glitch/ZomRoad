using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider xpSlider;
    public Slider fuelSlider;

    public Slider DamageSlider;
    public Slider RangeSlider;
    public Slider FireRateSlider;

    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI slugAmmoText;
    public TextMeshProUGUI bulletAmmoText;
    public TextMeshProUGUI energyAmmoText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI CurrFuelText;
    public TextMeshProUGUI CurrHealthText;
    public TextMeshProUGUI CurrXpText;
    public TextMeshProUGUI MaxXpText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        CurrHealthText.text = health.ToString();
    }

    public void SetMaxFuel(int maxFuel)
    {
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;
    }

    public void SetFuel(float fuel)
    {
        fuelSlider.value = fuel;
        CurrFuelText.text = fuel.ToString("#.#");
    }
    public void SetMaxXp(int maxXp)
    {
        xpSlider.maxValue = maxXp;
        xpSlider.value = 0;
        MaxXpText.text = maxXp.ToString();
    }

    public void SetXp(int xp)
    {
        xpSlider.value = xp;
        CurrXpText.text = xp.ToString();
    }

    public void SetDamage(int damage)
    {
        DamageSlider.value = damage;
    }

    public void SetFireRate(int fireRate)
    {
        FireRateSlider.value = fireRate;
    }

    public void SetRange(int range)
    {
        RangeSlider.value = range;
    }


    public void SetLevelText()
    {
        LevelText.text = gameManager.wheelController.Level.ToString();
    }
    public void SetWeaponNameText( string weaponName)
    {
        weaponNameText.text = weaponName;
    }

    public void SetSlugAmmoText()
    {
        slugAmmoText.text = gameManager.wheelController.slugAmmo.ToString();
    }

    public void SetBulletAmmoText()
    {
        bulletAmmoText.text = gameManager.wheelController.bulletAmmo.ToString();
    }

    public void SetEnergyAmmoText()
    {
        energyAmmoText.text = gameManager.wheelController.energyAmmo.ToString();
    }
}
