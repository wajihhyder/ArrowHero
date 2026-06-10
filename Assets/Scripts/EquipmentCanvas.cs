using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentCanvas : MonoBehaviour
{
    public GameObject levelCanvas;
    public int[] damage = new int[11];
    public GameObject homeCanvas;
    public GameObject shopCanvas;

    public TMP_Text coinText;
    private int coins;

    public GameObject[] itemObjects;
    public TMP_Text[] textBoxes;

    public Button[] buttons;
    public PersistentData persistentData;

    void Start()
    {
        // Cache all references at the start
        CacheReferences();
        
        // Deactivate this canvas initially
        gameObject.SetActive(false);
    }

    private void CacheReferences()
    {
        if (levelCanvas == null)
            levelCanvas = GameObject.Find("LevelCanvas");

        if (persistentData == null)
            persistentData = GameObject.Find("PersistentData")?.GetComponent<PersistentData>();

        if (homeCanvas == null)
            homeCanvas = GameObject.Find("HomeCanvas");

        if (shopCanvas == null)
            shopCanvas = GameObject.Find("ShopCanvas");
    }

    public void ItemsInteractable()
    {
        if (persistentData == null)
            CacheReferences(); // Ensure references are cached if missing

        coins = persistentData.getCoins();
        coinText.text = coins.ToString();
        int currentItem = persistentData.getCurrentItem();;

        for (int i = 0; i < itemObjects.Length; i++)
        {
            if(persistentData.checkBought(i))
                itemObjects[i].SetActive(true);
            else
                itemObjects[i].SetActive(false);
            
            if(currentItem == i){
                buttons[i].interactable = false;
                textBoxes[i].text = "EQUIPED";
            }
        }
    }

    public void ShowHome()
    {
        if (homeCanvas == null)
            CacheReferences();

        gameObject.SetActive(false);
        homeCanvas.SetActive(true);
    }

    public void ShowShop()
    {
        if (shopCanvas == null)
            CacheReferences();

        gameObject.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void ShowLevel()
    {
        if (levelCanvas == null)
            CacheReferences();

        gameObject.SetActive(false);
        levelCanvas.SetActive(true);
    }

    public void EquipItem(int item)
    {
        if (persistentData == null)
            CacheReferences();

        if (item < 0 || item >= damage.Length || item >= textBoxes.Length)
        {
            Debug.LogError("Item index is out of bounds!");
            return;
        }

        // Deactivate the previously equipped item
        int previousItem = persistentData.getCurrentItem();
        if (previousItem >= 0 && previousItem < textBoxes.Length)
        {
            textBoxes[previousItem].text = "EQUIP";
            buttons[previousItem].interactable = true;
        }

        // Activate the new item
        persistentData.setCurrentItem(item, damage[item]);
        textBoxes[item].text = "EQUIPED";
        buttons[item].interactable = false;
    }
}
