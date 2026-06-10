using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;
    public bool[] levelsUnlocked;
    public int[] crowns;
    public int currentLevel;
    public int coins = 0;
    public bool[] itemsBought;
    public bool[] currentWeapon;
    public int currentWeaponInt;
    public int currentDamage;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        levelsUnlocked = new bool[10];
        crowns = new int[10];

        for(int i = 0; i < 10; i++){
            levelsUnlocked[i] = false;
            crowns[i] = 0;
        }
        levelsUnlocked[0] = true;

        itemsBought = new bool[11];
        for(int i = 0; i < 10; i++){
            itemsBought[i] = false;
        }
        itemsBought[10] = true;

        currentWeapon = new bool[11];
        for(int i = 0; i < 10; i++){
            currentWeapon[i] = false;
        }
        currentWeapon[10] = true;
        currentWeaponInt = 10;
        currentDamage = 20;
    }

    public bool checkUnlocked(int level)
    {
        return levelsUnlocked[level];
    }

    public int getCrowns(int level)
    {
        return crowns[level - 1];
    }

    public void unlockLevel(int level)
    {
        levelsUnlocked[level] = true;
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }

    public void minusCoins(int amount)
    {
        coins -= amount;
    }

    public void setCrowns(int level, int amount)
    {
        crowns[level - 1] = amount;
    }

    public int getCoins(){
        return coins;
    }

    public bool checkBought(int item){
        return itemsBought[item];
    }

    public void setBought(int item){
        itemsBought[item] = true;
    }

    public void setCurrentItem(int item, int dmg){
        currentWeapon[item] = true;
        currentWeapon[currentWeaponInt] = false;
        currentWeaponInt = item;
        currentDamage = dmg;
    }

    public int getCurrentItem(){
        return currentWeaponInt;
    }

    public int getDamage(){
        return currentDamage;
    }
    
}
