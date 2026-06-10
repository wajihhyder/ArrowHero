using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates the arm every 3 seconds
public class armRotator : MonoBehaviour
{
    float timer = 0;
    public int max;
    public int min;

    public GameObject arm;

    // Update is called once per frame
    void Update(){
        if(transform.childCount > 0){

            timer += Time.deltaTime;
            if(timer > 3){
                int rotValue = Random.Range(min,max);
                arm.transform.eulerAngles = new Vector3(0, 0, rotValue);
                timer = 0;
            }
        }
    }
}
