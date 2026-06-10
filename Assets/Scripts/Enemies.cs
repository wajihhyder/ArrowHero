using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject[] objs;

    bool completed = false;
    public UIManager uiManager;

    [SerializeField] public int enemiesCount;

    // Update is called once per frame
    void Update()
    {
        objs = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach (GameObject obj in objs)
        {
            if (obj.transform.childCount > 0)
            {
                return;
            }
        }

        completed = true;
        if (completed)
        {
            uiManager.ShowLevelCompletedCanvas();
        }
    }
}
