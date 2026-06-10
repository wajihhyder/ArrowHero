using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public EnemyHealth healthBar;

    public SoundManager soundManager;
    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealth>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            soundManager.PlayEnemyDeathSfx();
            Destroy(gameObject);  
            return;     
        }
        healthBar.setHealth(currentHealth);
    }
}
