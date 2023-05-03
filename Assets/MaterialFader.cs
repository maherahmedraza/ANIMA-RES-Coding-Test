using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using DG.Tweening;

/// <summary>
/// Fade in and out a material
/// </summary>
public class MaterialFader : MonoBehaviour
{
    [Tooltip("Duration of the fade in seconds")]
    public float fadeDuration = 1f;
    [Tooltip("Delay before the fade in seconds")]
    public float delay = 0f;
    [Tooltip("Material to fade")]
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
            //materialToFade.DOFade(0f,1f);
        }
    }

    /// <summary>
    /// Fade in the material
    /// </summary>
    public void FadeIn()
    {
        Debug.Log("FadeIn");
        materialToFade.DOFade(_initialAlpha, fadeDuration).SetDelay(delay);
    }
    
    public void FadeIn(UnityAction onComplete = null)
    {
        Debug.Log("FadeIn");
        materialToFade.DOFade(_initialAlpha, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    /// <summary>
    /// Fade out the material
    /// </summary>
    
    public void FadeOut()
    {
        materialToFade.DOFade(0f, fadeDuration).SetDelay(delay);
    }
    
    public void FadeOut(UnityAction onComplete = null)
    {
        materialToFade.DOFade(0f, fadeDuration).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}