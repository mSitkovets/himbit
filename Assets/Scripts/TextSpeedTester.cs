using System.Collections;
using UnityEngine;
using TMPro;

public class TextSpeedTester : MonoBehaviour
{
    private string s = "This is a test for speed.";
    public TMP_Text box;
    public TypewriterEffect type;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine("typing");
    }

    IEnumerator typing()
    {
        while (true)
        {
            type.Run(s, box);
            yield return new WaitForSeconds(2f);
        }
    }
}
