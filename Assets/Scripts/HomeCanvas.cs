using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCanvas : MonoBehaviour
{
    public GameObject levelCanvas; 
    public GameObject shopCanvas;
    public GameObject tutorialCanvas;

    public GameObject settingsCanvas;

    public GameObject equiqmentCanvas;
    void Awake(){
        gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Game is exiting.");
        Application.Quit();
    }

    // Load the level selection screen
    public void showLevel()
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

    // Load the shop screen
    public void showShop()
    {
        if (shopCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){            
                if (obj.name == "ShopCanvas"){
                    shopCanvas = obj;
                }
            }
        }

        gameObject.SetActive(false);
        shopCanvas.SetActive(true);
    }

    // Load the equipment screen
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

    // Load the settings screen
    public void showSettings()
    {
        if (settingsCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){            
                if (obj.name == "SettingsCanvas"){
                    settingsCanvas = obj;
                }
            }
        }

        settingsCanvas.SetActive(true);
    }

    // Load the tutorial screen
    public void showTutorial()
    {
        if (tutorialCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){            
                if (obj.name == "TutorialCanvas"){
                    tutorialCanvas = obj;
                }
            }
        }

        tutorialCanvas.SetActive(true);
    }
}
