using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider xpSlider;
    public TextMeshProUGUI ammoText;



    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
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

    public void SetAmmo(int ammo)
    {
        
    }
}
