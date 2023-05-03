using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// Controls Scene 2
/// </summary>
public class Scene2Controller : MonoBehaviour
{
    // The root game object for this scene
    [SerializeField] private GameObject sceneRoot;

    // The timeline manager for this scene
    public TimelineManager timelineManager;

    // The list of animated objects in this scene
    public List<GameObject> animatedObjects;

    // The positions of the animated objects when they are centred on the screen
    private List<Vector3> _animatedObjectPositions = new List<Vector3>()
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0.676039219f, 0f, -0.181579173f),
        new Vector3(0.999658f, 0f, -0.01884466f),
    };

    // The positions of the animated objects when they start in this scene
    private List<Vector3> _animatedObjectStartPositions = new List<Vector3>()
    {
        new Vector3(0f,7f,0f),
        new Vector3(0f,-7f,0f),
        new Vector3(7f, 0f, 0f),
    };

    // The index of the selected object in the animated objects list
    private int _selectedObjectIndex = 0;

    public int SelectedObjectIndex
    {
        get => _selectedObjectIndex;
        set => _selectedObjectIndex = value;
    }

    // The object that was clicked by the user
    private GameObject _clickedObject;

    public GameObject ClickedObject
    {
        get => _clickedObject;
        set => _clickedObject = value;
    }

    // The main camera in the scene
    [SerializeField] private Camera _mainCamera;

    // The Unity event to call when the scene starts
    [SerializeField] private UnityEvent startEvent;
    

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        if (timelineManager == null)
        {
            timelineManager = FindObjectOfType<TimelineManager>();
        }
    }

    /// <summary>
    /// Starts the scene
    /// </summary>
    public void StartScene()
    {
        Debug.Log("Start Scene");

        // Activate the scene root object
        sceneRoot.SetActive(true);

        // Reset the animated objects to their start positions
        ResetAnimatedObjects();

        // Invoke the start event
        startEvent?.Invoke();
    }
    
    /// <summary>
    /// Sets the animated objects to their start positions
    /// </summary>
    public void SetAnimatedObjectStartPositions()
    {
        for (int i = 0; i < animatedObjects.Count; i++)
        {
            animatedObjects[i].transform.position = _animatedObjectStartPositions[i];
        }
    }
    
    public void TransitionToScene3(int sceneIndex)
    {
        // Get the current scene index
        int currentSceneIndex = SceneLoader.Instance.currentSceneIndex;

        // Activate the target scene and switch the timeline
        SceneLoader.Instance.ActivateScene(sceneIndex);
        timelineManager.SwitchScene(sceneIndex);

        // Reset the list of animated object positions
        _animatedObjectPositions = new List<Vector3>(); 

        // Stop all currently animated objects
        StopAllAnimatedObjects();

        // Animate the selected object to the center of the screen
        AnimateObject(animatedObjects[_selectedObjectIndex]);

        // Find the Scene3Controller and pass relevant data to it
        var scene3Controller = FindObjectOfType<Scene3Controller>();
        scene3Controller.cameraPosition = _mainCamera.transform.position;
        scene3Controller.cameraRotation = _mainCamera.transform.eulerAngles;
        scene3Controller.cameraFOV = _mainCamera.fieldOfView;
        scene3Controller.objectIndex = _selectedObjectIndex;
        scene3Controller.objectPosition = animatedObjects[_selectedObjectIndex].transform.position;
        scene3Controller.objectRotation = animatedObjects[_selectedObjectIndex].transform.eulerAngles;
        scene3Controller.StartScene();
        scene3Controller.timelineManager.SwitchScene(sceneIndex);

        // Save the current positions of all animated objects
        for (int i = 0; i < animatedObjects.Count; i++)
        {
            _animatedObjectPositions.Add(animatedObjects[i].transform.position);
        }

        // Hide the scene root object
        sceneRoot.SetActive(false);
    }

    
    /// <summary>
    /// Transitions to scene 1, deactivates the current scene and activates scene 1, switches the timeline to scene 1,
    /// stops all animated objects, spreads the objects out on the screen and starts scene 1.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to transition to.</param>
    public void TransitionToScene1(int sceneIndex)
    {
        // Activate scene 1
        SceneLoader.Instance.ActivateScene(sceneIndex);

        // Switch the timeline to scene 1
        timelineManager.SwitchScene(sceneIndex);

        // Stop all animated objects
        StopAllAnimatedObjects();

        // Spread objects out on the screen
        SpreadObject();

        // Find the Scene1Controller and start scene 1
        var scene1Controller = FindObjectOfType<Scene1Controller>();
        scene1Controller.timelineManager.SwitchScene(sceneIndex);
        scene1Controller.StartScene();
    }

    
    /// <summary>
    /// Switches OFF ObjectRotator component for all animated objects.
    /// </summary>
    public void StopAllAnimatedObjects()
    {
        foreach (GameObject animatedObject in animatedObjects)
        {
            // Get the ObjectRotator component and disable it
            ObjectRotator rotator = animatedObject.GetComponent<ObjectRotator>();
            if (rotator != null)
            {
                rotator.enabled = false;
            }
            else
            {
                Debug.LogWarning($"Object {animatedObject.name} does not have an ObjectRotator component.");
            }
        }
    }


    
    // Switch ON ObjectRotator For all animated objects
    public void StartAllAnimatedObjects()
    {
        foreach (var animatedObject in animatedObjects)
        {
            animatedObject.GetComponent<ObjectRotator>().enabled = true;
        }
    }
    
    // Animate the clicked object to the centre of screen 
    // Animate the remaining objects to the edge of the screen
    /// <summary>
    /// Animate the clicked object to the center of the screen and the remaining objects to the edge of the screen.
    /// </summary>
    /// <param name="clickedObject">The object that was clicked by the user.</param>
    public void AnimateObject(GameObject clickedObject)
    {
        // Set the clicked object as the selected object.
        _clickedObject = clickedObject;

        // Loop through all animated objects.
        foreach (var animatedObject in animatedObjects)
        {
            // If this object is the clicked object, set it as the selected object.
            if (animatedObject == clickedObject)
            {
                _selectedObjectIndex = animatedObjects.IndexOf(animatedObject);
            }
            else
            {
                // Move the object to the left side of the screen.
                animatedObject.transform.DOMove(new Vector3(-10, 0, 0), 5f, false);

                // Fade out the material of the object and then disable it.
                animatedObject.GetComponent<MaterialFader>().FadeOut(() =>
                {
                    animatedObject.SetActive(false);
                });
            }
        }
    }

    public void SpreadObject()
    {
        foreach (var animatedObject in animatedObjects)
        {
            // move object to its start position
            animatedObject.transform.DOMove(_animatedObjectStartPositions[animatedObjects.IndexOf(animatedObject)], 5f, false);

            // fade out the object's material and set it inactive
            animatedObject.GetComponent<MaterialFader>().FadeOut(() =>
            {
                animatedObject.SetActive(false);
                sceneRoot.SetActive(false);
            });
        }
    }

    /// <summary>
    /// Resets the positions and rotations of all animated objects to their initial positions.
    /// </summary>
    public void ResetAnimatedObjects()
    {
        foreach (var animatedObject in animatedObjects)
        {
            if (animatedObject != _clickedObject)
            {
                animatedObject.SetActive(true);
                animatedObject.transform.DOMove(_animatedObjectPositions[animatedObjects.IndexOf(animatedObject)], 2f,
                        false)
                    .OnComplete(() => { animatedObject.GetComponent<ObjectRotator>().enabled = true; });
                animatedObject.GetComponent<MaterialFader>().FadeIn(null);
            }
        }
    }
}
