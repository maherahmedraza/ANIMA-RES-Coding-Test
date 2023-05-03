using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using DG.Tweening;

/// <summary>
/// Fades in and out a material.
/// </summary>
public class MaterialFader : MonoBehaviour
{
    /// <summary>
    /// Duration of the fade in seconds.
    /// </summary>
    [Tooltip("Duration of the fade in seconds.")]
    public float fadeDuration = 1f;

    /// <summary>
    /// Delay before the fade in seconds.
    /// </summary>
    [Tooltip("Delay before the fade in seconds.")]
    public float delay = 0f;

    /// <summary>
    /// Material to fade.
    /// </summary>
    [Tooltip("Material to fade.")]
    public Material materialToFade;

    private readonly float _initialAlpha = 1f;

    private void Awake()
    {
        if (materialToFade)
        {
            // Save the initial alpha value of the material
            //_initialAlpha = materialToFade.color.a;

            // Set the material to be invisible
            materialToFade.color = new Color(materialToFade.color.r, materialToFade.color.g, materialToFade.color.b, 0f);
        }
    }

    /// <summary>
    /// Fades in the material.
    /// </summary>
    public void FadeIn()
    {
        materialToFade.DOFade(_initialAlpha, fadeDuration).SetDelay(delay);
    }

    /// <summary>
    /// Fades in the material and calls the specified action when the fade is complete.
    /// </summary>
    /// <param name="onComplete">Action to call when the fade is complete.</param>
    public void FadeIn(UnityAction onComplete = null)
    {
        materialToFade.DOFade(_initialAlpha, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    /// <summary>
    /// Fades out the material.
    /// </summary>
    public void FadeOut()
    {
        materialToFade.DOFade(0f, fadeDuration).SetDelay(delay);
    }

    /// <summary>
    /// Fades out the material and calls the specified action when the fade is complete.
    /// </summary>
    /// <param name="onComplete">Action to call when the fade is complete.</param>
    public void FadeOut(UnityAction onComplete = null)
    {
        materialToFade.DOFade(0f, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
