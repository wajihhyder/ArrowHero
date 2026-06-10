using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvas : MonoBehaviour
{
    public GameObject homeCanvas;

    public GameObject eqiuipmentCanvas;

    public levelObject[] levelObjects;
    public Sprite goldenCrown;

    public PersistentData persistentData;
    bool check;
    int crowns;

    public GameObject shopCanvas;

    void Start(){
        GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject obj in temp){
            if (obj.name == "ShopCanvas"){
                shopCanvas = obj;
            }
            else if(obj.name == "HomeCanvas"){
                homeCanvas = obj;
            }
            else if(obj.name == "PersistentData"){
                persistentData = obj.GetComponent<PersistentData>();
            }
            else if(obj.name == "EquiqmentCanvas"){
                eqiuipmentCanvas = obj;
            }
        }
        gameObject.SetActive(false);
    }

    public void OnClickLevel(int level)
    {
        gameObject.SetActive(false);

        persistentData.currentLevel = level;
        SceneManager.LoadScene("Level" + level);
    }

    public void showHome()
    {
        if(homeCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "HomeCanvas"){
                    homeCanvas = obj;
                }
            }
        }

        gameObject.SetActive(false);
        homeCanvas.SetActive(true);
    }

    public void levelsInteractable()
    {
        if(persistentData == null){
            persistentData = PersistentData.Instance;
        }

        for(int i = 0; i < 10; i++){
            check = persistentData.checkUnlocked(i);
            if(check == true){
                levelObjects[i].levelButton.interactable = true;
                crowns = persistentData.getCrowns(i+1);
                for(int j = 0; j < crowns; j++){
                    levelObjects[i].crowns[j].sprite = goldenCrown;
                }      
            }
        }
    }

    public void showShop(){
        if(shopCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "ShopCanvas"){
                    shopCanvas = obj;
                }
            }
        }

        gameObject.SetActive(false);
        shopCanvas.SetActive(true);
    } 

    public void showEquiqment(){
        if(eqiuipmentCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "EquiqmentCanvas"){
                    eqiuipmentCanvas = obj;
                }
            }
        }

        gameObject.SetActive(false);
        eqiuipmentCanvas.SetActive(true);
    }
}
