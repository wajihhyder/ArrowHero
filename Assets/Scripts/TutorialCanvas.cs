using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    public GameObject[] panels;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable(){
        for(int i = 1; i < panels.Length; i++){
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);
    }

    public void onClickRight(int index){
        panels[index].SetActive(false);
        panels[index+1].SetActive(true);
    }

    public void onClickLeft(int index){
        panels[index].SetActive(false);
        panels[index-1].SetActive(true);
    }

    public void closeTutorial(){
        gameObject.SetActive(false);
    }
}
