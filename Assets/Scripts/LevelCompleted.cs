using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCompleted : MonoBehaviour
{
    public Image[] images;
    public Sprite goldenCrown;


    public TMP_Text coinsCount;
    public TMP_Text headshotsCount;
    public TMP_Text noHitBonusCount;
    public TMP_Text arrowBonusCount;
    public TMP_Text levelBonusCount;

    public void showLevelCompleted(int headshotBonus, int noHitBonus, int arrowBonus, int crowns, int coins)
    {
        headshotsCount.text = "x" + headshotBonus.ToString();
        noHitBonusCount.text = "x" + noHitBonus.ToString();
        arrowBonusCount.text = "x" + arrowBonus.ToString();
        levelBonusCount.text = "x10";
        coinsCount.text = coins.ToString();

        for(int i = 0; i < crowns; i++){
            images[i].sprite = goldenCrown;
        }
    }
}
