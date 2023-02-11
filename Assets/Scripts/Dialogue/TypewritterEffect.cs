using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewritterEffect : MonoBehaviour
{
    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float t = 0f;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        textLabel.text = textToType;
    }
}
