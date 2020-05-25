using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinker : MonoBehaviour
{
    public Text text;
    private void OnEnable()
    {
        StartCoroutine(ShowHideRoutine());
    }

    IEnumerator ShowHideRoutine()
    {
        text.enabled = !text.enabled;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowHideRoutine());
    }
}
