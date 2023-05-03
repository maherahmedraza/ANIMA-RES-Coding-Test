using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Scene3Controller : MonoBehaviour
{
    
    [SerializeField] public TimelineManager timelineManager;
    [SerializeField] private GameObject sceneRoot;
    [SerializeField] private List<GameObject> objectsToAnimate;
    [SerializeField] private Transform observationPoint;
    [SerializeField] private UnityEvent startEvent;

    [Header("Camera")]
    [SerializeField] public Camera mainCamera;
    [SerializeField] public Vector3 cameraPosition;
    [SerializeField] public Vector3 cameraRotation;
    [SerializeField] public float cameraFOV;

    [Header("Object")]
    [SerializeField] public int objectIndex;
    [SerializeField] public Vector3 objectPosition;
    [SerializeField] public Vector3 objectRotation;

    [Header("Extra Object")]
    [SerializeField] public GameObject extraObject;
    [SerializeField] public Vector3 extraObjectPosition = new Vector3(10f, 10f, 10f);

    private Scene2Controller _scene2Controller;
    private Scene1Controller _scene1Controller;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        if (timelineManager == null)
        {
            timelineManager = FindObjectOfType<TimelineManager>();
        }

        _scene2Controller = FindObjectOfType<Scene2Controller>();
        _scene1Controller = FindObjectOfType<Scene1Controller>();
    }
    
    
    /// <summary>
    /// Starts the scene with initial values
    /// </summary>
    public void StartScene()
    {
        Debug.Log("Start Scene");

        // Set camera position
        if (mainCamera != null)
        {
            mainCamera.transform.position = cameraPosition;
            mainCamera.transform.eulerAngles = cameraRotation;
            mainCamera.fieldOfView = cameraFOV;
        }

        // Set object position
        if (objectsToAnimate != null)
        {
            objectsToAnimate[objectIndex].SetActive(true);
            objectsToAnimate[objectIndex].transform.position = objectPosition;
            objectsToAnimate[objectIndex].transform.eulerAngles = objectRotation;
        }

        sceneRoot.SetActive(true);
        startEvent?.Invoke();
        extraObject.transform.position = extraObjectPosition;
        AnimateObjectToPosition(objectIndex, observationPoint.position);
    }

    /// <summary>
    /// Transitions to Scene 2
    /// </summary>
    /// <param name="sceneIndex">The index of the Scene to be transitioned to</param>
    public void TransitionToScene2(int sceneIndex)
    {
        SceneLoader.Instance.ActivateScene(sceneIndex);
        timelineManager.SwitchScene(sceneIndex);
        ResetAnimateObjectPosition(objectIndex, objectPosition, sceneIndex);

        var scene2Controller = FindObjectOfType<Scene2Controller>();
        scene2Controller.animatedObjects[objectIndex].SetActive(false);
        scene2Controller.StartScene();
        scene2Controller.animatedObjects[objectIndex].transform.rotation = objectsToAnimate[objectIndex].transform.rotation;
        scene2Controller.timelineManager.SwitchScene(sceneIndex);
    }

    /// <summary>
    /// Transitions to Scene 1
    /// </summary>
    /// <param name="sceneIndex">The index of the Scene to be transitioned to</param>
    public void TransitionToScene1(int sceneIndex)
    {
        SceneLoader.Instance.ActivateScene(sceneIndex);
        timelineManager.SwitchScene(sceneIndex);
        ResetAnimateObjectPosition(objectIndex, new Vector3(10, 0f, 0f), sceneIndex);

        var scene1Controller = FindObjectOfType<Scene1Controller>();
        scene1Controller.timelineManager.SwitchScene(sceneIndex);
    }
    
   /// <summary>
/// Animates the object at the specified index to the specified position.
/// </summary>
/// <param name="objectIndex">The index of the object to animate.</param>
/// <param name="position">The position to animate the object to.</param>
public void AnimateObjectToPosition(int objectIndex, Vector3 position)
{
    if (objectsToAnimate == null || objectsToAnimate.Count <= objectIndex)
    {
        Debug.LogError($"Invalid object index ({objectIndex}) or object list is null or empty.");
        return;
    }

    // Activate extra object to be used for fading in the animated object
    extraObject.SetActive(true);

    // Move the animated object and extra object to the specified position
    var sequence = DOTween.Sequence();
    sequence.Append(objectsToAnimate[objectIndex].transform.DOMove(position, 5f));
    sequence.Join(extraObject.transform.DOMove(position + new Vector3(-0.10f, 0, -0.10f), 2.5f)).OnComplete(() =>
    {
        extraObject.GetComponent<MaterialFader>().FadeIn();
    });
}

/// <summary>
/// Resets the position of the object at the specified index to the specified position,
/// disables the animated object, and handles scene transitions if necessary.
/// </summary>
/// <param name="objectIndex">The index of the object to reset.</param>
/// <param name="position">The position to reset the object to.</param>
/// <param name="sceneIndex">The index of the scene to transition to.</param>
public void ResetAnimateObjectPosition(int objectIndex, Vector3 position, int sceneIndex)
{
    if (objectsToAnimate == null || objectsToAnimate.Count <= objectIndex)
    {
        Debug.LogError($"Invalid object index ({objectIndex}) or object list is null or empty.");
        return;
    }

    // Move the animated object and extra object to their original positions and disable the animated object
    var sequence = DOTween.Sequence();
    sequence.Append(extraObject.transform.DOMove(extraObjectPosition, 2f));
    sequence.Join(objectsToAnimate[objectIndex].transform.DOMove(position, 2f)).OnComplete(
        () =>
        {
            objectsToAnimate[objectIndex].SetActive(false);
            if (sceneIndex == 0)
            {
                // Transition to Scene 1 and disable the extra object and scene root
                _scene1Controller.StartScene();
                extraObject.SetActive(false);
                sceneRoot.SetActive(false);
            }
            else if (sceneIndex == 1)
            {
                // Transition to Scene 2, enable the animated object, and fade out the extra object
                _scene2Controller.animatedObjects[objectIndex].SetActive(true);
                _scene2Controller.animatedObjects[objectIndex].GetComponent<ObjectRotator>().enabled = true;
                extraObject.GetComponent<MaterialFader>().FadeOut(() =>
                {
                    extraObject.SetActive(false);
                    sceneRoot.SetActive(false);
                });
            }
        });
}

}