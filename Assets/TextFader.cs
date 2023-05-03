using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Fade in and out a text
/// </summary>
public class TextFader : MonoBehaviour
{
    [Tooltip("Duration of the fade in seconds")]
    public float fadeDuration = 1f;
    [Tooltip("Delay before the fade in seconds")]
    public float delay = 0f;
    [Tooltip("Text to fade")]
    public TextMeshProUGUI textToFade;
    // Start is called before the first frame update
    void Start()
    {
        if (textToFade)
        {
            // Set the text to be invisible
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
        }
    }

    /// <summary>
    /// Fade in the text
    /// </summary>
    public void FadeIn()
    {
        textToFade.DOFade(1f, fadeDuration).SetDelay(delay);
    }
    
    /// <summary>
    /// Fade in the text with a callback
    /// </summary>
    public void FadeIn(UnityAction onComplete)
    {
        textToFade.DOFade(1f, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    
    /// <summary>
    /// Fade out the text
    /// </summary>
    public void FadeOut()
    {
        textToFade.DOFade(0f, fadeDuration).SetDelay(delay);
    }
    
    
    /// <summary>
    /// Fade out the text with a callback
    /// </summary>
    public void FadeOut(UnityAction onComplete)
    {
        textToFade.DOFade(0f, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
