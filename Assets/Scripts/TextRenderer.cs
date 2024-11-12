using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private CanvasGroup canvasGroup;

    private Coroutine textTypeCoroutine, canvasGroupCoroutine;
    private int charactersToDisplay;

    private void Start()
    {
        charactersToDisplay = 0;
    }

    IEnumerator TextAlphaRoutine(float end)
    {
        float start = canvasGroup.alpha;
        
        float t = 0;
        while (t <= 0.5f)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, t / 0.5f);
            yield return null;
        }
    }

    IEnumerator AppearTextRoutine()
    {
        while (charactersToDisplay < displayText.text.Length)
        {
            charactersToDisplay++;
            displayText.maxVisibleCharacters = charactersToDisplay;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator DisappearTextRoutine()
    {
        while (charactersToDisplay > 0)
        {
            charactersToDisplay--;
            displayText.maxVisibleCharacters = charactersToDisplay;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (textTypeCoroutine != null)
            StopCoroutine(textTypeCoroutine);
        textTypeCoroutine = StartCoroutine(AppearTextRoutine());
        
        if (canvasGroupCoroutine != null)
            StopCoroutine(canvasGroupCoroutine);
        canvasGroupCoroutine = StartCoroutine(TextAlphaRoutine(1));
    }

    private void OnTriggerExit(Collider other)
    {
        if (textTypeCoroutine != null)
            StopCoroutine(textTypeCoroutine);
        textTypeCoroutine = StartCoroutine(DisappearTextRoutine());
        
        if (canvasGroupCoroutine != null)
            StopCoroutine(canvasGroupCoroutine);
        canvasGroupCoroutine = StartCoroutine(TextAlphaRoutine( 0));
    }
}
