using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     Fades in and out a TextMeshProUGUI object.
/// </summary>
public class TextFader : MonoBehaviour
{
    [Tooltip("Duration of the fade in seconds.")]
    public float fadeDuration = 1f;

    [Tooltip("Delay before the fade in seconds.")]
    public float delay;

    [Tooltip("Text to fade.")] public TextMeshProUGUI textToFade;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the text to be invisible.
        if (textToFade != null)
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
    }

    /// <summary>
    ///     Fades in the text.
    /// </summary>
    public void FadeIn()
    {
        textToFade.DOFade(1f, fadeDuration).SetDelay(delay);
    }

    /// <summary>
    ///     Fades in the text with a callback.
    /// </summary>
    /// <param name="onComplete">Optional callback function to call after fade in is complete.</param>
    public void FadeIn(UnityAction onComplete = null)
    {
        textToFade.DOFade(1f, fadeDuration).SetDelay(delay).OnComplete(() => { onComplete?.Invoke(); });
    }

    /// <summary>
    ///     Fades out the text.
    /// </summary>
    public void FadeOut()
    {
        textToFade.DOFade(0f, fadeDuration).SetDelay(delay);
    }

    /// <summary>
    ///     Fades out the text with a callback.
    /// </summary>
    /// <param name="onComplete">Optional callback function to call after fade out is complete.</param>
    public void FadeOut(UnityAction onComplete = null)
    {
        textToFade.DOFade(0f, fadeDuration).SetDelay(delay).OnComplete(() => { onComplete?.Invoke(); });
    }
}