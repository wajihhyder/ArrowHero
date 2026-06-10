using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour
{
   public TMP_Text arrowCountText;

    void Update(){
        
        if(arrowCountText == null){
            arrowCountText = GameObject.FindWithTag("ArrowCount").GetComponent<TMP_Text>();
        }
        
    }

    public void UpdateArrowCount(int arrowCount){
        Debug.Log(message: "Arrow Count: " + arrowCount);

        arrowCountText.text = arrowCount.ToString();
    }
}
