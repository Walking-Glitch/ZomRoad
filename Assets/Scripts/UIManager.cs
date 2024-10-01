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
    public TextMeshProUGUI slugAmmoText;
    public TextMeshProUGUI bulletAmmoText;
    public TextMeshProUGUI energyAmmoText;

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
    }

    public void SetMaxFuel(int maxFuel)
    {
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;
    }

    public void SetFuel(float fuel)
    {
        fuelSlider.value = fuel;
    }
    public void SetMaxXp(int maxXp)
    {
        xpSlider.maxValue = maxXp;
        xpSlider.value = 0;
    }

    public void SetXp(int xp)
    {
        xpSlider.value = xp;
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
