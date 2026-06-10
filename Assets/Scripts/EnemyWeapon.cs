using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float timer;
    public int waitingTime;
    public GameObject game;

    void Start()
    {
        game = GameObject.Find("Game");
        if(game == null){
            game = GameObject.Find("Game 1");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime){
            Shoot();
            timer = 0;
        }
    }

    void Shoot(){
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation, game.transform);        
    }
}
