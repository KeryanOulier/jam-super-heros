using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] private TMP_Text score_label;
    [SerializeField] private int score = 0;

    public void Update()
    {
        score_label.text = "Score: " + score;
    }

    public void addScore(int score)
    {
        this.score += score;
    }
}
