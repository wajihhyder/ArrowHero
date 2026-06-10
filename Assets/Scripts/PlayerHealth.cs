using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    int maxHealth;

    public UIManager uiManager;

    public void setMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
        maxHealth = health;
    }
    public void setHealth(int health){
        slider.value = health;
    }  
}
