using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endScore : MonoBehaviour
{
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = "Score:\n" + PlayerQuest.instance.Score;
    }
}
