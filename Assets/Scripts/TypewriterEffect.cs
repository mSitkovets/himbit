using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private int typewriterSpeed = 50;

    private void Update()
    {
        //change typing speed by slider
    }

    // running the coroutine
    public Coroutine Run(string textToType, TMP_Text textLabel){
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    // typing the text
    private IEnumerator TypeText(string textToType, TMP_Text textLabel){
        textLabel.text = string.Empty;
        // elapsed time since we've begun writing
        float t = 0;
        int charIndex = 0;
        while (charIndex < textToType.Length){
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            textLabel.text = textToType.Substring(0, charIndex);
            // wait one frame 
            yield return null;
        }
        textLabel.text = textToType;
    }
}
