using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public PlayerHealth healthBar;
    public UIManager uiManager;
    public PersistentData persistentData;
    int currentWeaponInt;
    public SpriteRenderer ownWeaponSprite;
    public Sprite[] WeaponSprites;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        currentWeaponInt = persistentData.getCurrentItem();
        ownWeaponSprite.sprite = WeaponSprites[currentWeaponInt];
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0){
            Destroy(gameObject);
            uiManager.ShowGameoverCanvas();
        }
    }
}
