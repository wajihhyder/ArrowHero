using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider slider;
    int maxHealth;
    public void setMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
        maxHealth = health;
    }
    public void setHealth(int health){
        slider.value = health;
    }
}
