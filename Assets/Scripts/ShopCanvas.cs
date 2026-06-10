using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopCanvas : MonoBehaviour
{
    public GameObject levelCanvas;
    public GameObject homeCanvas;
    public GameObject equiqmentCanvas;

    public ShopObject[] shopObjects;

    public PersistentData persistentData;

    public GameObject[] buy;

    public GameObject[] bought;

    int coins;

    public TMP_Text coinsText;

    void Start(){
        
        GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject obj in temp){
            if(levelCanvas == null){
                if (obj.name == "LevelCanvas"){
                    levelCanvas = obj;
                }
            }
            if(persistentData == null){
                if(obj.name == "PersistentData"){
                    persistentData = obj.GetComponent<PersistentData>();
                }
            }
            if(equiqmentCanvas == null){
                if(obj.name == "EquiqmentCanvas"){
                    equiqmentCanvas = obj;
                }
            }
            if(homeCanvas == null){
                if(obj.name == "HomeCanvas"){
                    homeCanvas = obj;
                }
            }
        }

        gameObject.SetActive(false);
        coins = persistentData.getCoins();
        coinsText.text = coins.ToString();
    }

    public void showlevel()
    {
        if (levelCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "LevelCanvas"){
                    levelCanvas = obj;
                }
            }
        }
        gameObject.SetActive(false);
        levelCanvas.SetActive(true);
    }

    public void showEquiqment()
    {
        if (equiqmentCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "EquiqmentCanvas"){
                    equiqmentCanvas = obj;
                }
            }
        }
        gameObject.SetActive(false);
        equiqmentCanvas.SetActive(true);
    }

    public void showHome()
    {
        if (homeCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "HomeCanvas"){
                    homeCanvas = obj;
                }
            }
        }
        gameObject.SetActive(false);
        homeCanvas.SetActive(true);
    }

    public void itemsInteractable()
    {
        if(persistentData == null){
            persistentData = PersistentData.Instance;
        }

        coins = persistentData.getCoins();
        coinsText.text = coins.ToString();
        for(int i = 0; i < 10; i++){
            if(persistentData.checkBought(i) == false){
                if(coins >= shopObjects[i].price){
                    shopObjects[i].itemButton.interactable = true;
                }   
                else{
                    shopObjects[i].itemButton.interactable = false;
                }
                bought[i].SetActive(false);
            }
            else{
                shopObjects[i].itemButton.interactable = false;
                bought[i].SetActive(true);
                buy[i].SetActive(false);
            }
        }
    }

    public void OnClickItem(int item){
        if(persistentData == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){            
                if (obj.name == "PersistentData"){
                    persistentData = obj.GetComponent<PersistentData>();
                }
            }
        }

        shopObjects[item].itemButton.interactable = false;  
        persistentData.setBought(item);
        persistentData.minusCoins(shopObjects[item].price);
        coinsText.text = persistentData.getCoins().ToString();
        coins = persistentData.getCoins();
        buy[item].SetActive(false);
        bought[item].SetActive(true);
        itemsInteractable();
    }
}
