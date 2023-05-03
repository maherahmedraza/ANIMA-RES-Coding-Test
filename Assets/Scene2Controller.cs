using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Scene2Controller : MonoBehaviour
{
    public TimelineManager timelineManager;
    [SerializeField] private GameObject sceneRoot;
    public List<GameObject> animatedObjects;

    private List<Vector3> _animatedObjectPositions = new List<Vector3>()
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0.676039219f, 0f, -0.181579173f),
        new Vector3(0.999658f, 0f, -0.01884466f),
    };
    
    private List<Vector3> _animatedObjectStartPositions = new List<Vector3>()
    {
        new Vector3(0f,7f,0f),
        new Vector3(0f,-7f,0f),
        new Vector3(7f, 0f, 0f),
    };

    private int _selectedObjectIndex = 0;

    public int SelectedObjectIndex
    {
        get => _selectedObjectIndex;
        set => _selectedObjectIndex = value;
    }

    private GameObject _clickedObject;

    public GameObject ClickedObject
    {
        get => _clickedObject;
        set => _clickedObject = value;
    }

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private UnityEvent startEvent;
    

    // Start is called before the first frame update
    void Start()
    {
        if (timelineManager == null)
        {
            timelineManager = FindObjectOfType<TimelineManager>();
        }
    }

    public void StartScene()
    {
        Debug.Log("Start Scene");
        sceneRoot.SetActive(true);
        // timelineManager.CheckUI();
        ResetAnimatedObjects();
        startEvent?.Invoke();
    }
    
    public void SetAnimatedObjectStartPositions()
    {
        for (int i = 0; i < animatedObjects.Count; i++)
        {
            animatedObjects[i].transform.position = _animatedObjectStartPositions[i];
        }
    }
    
    public void TransitionToScene3(int sceneIndex)
    {
        int currentSceneIndex = SceneLoader.Instance.currentSceneIndex;
        SceneLoader.Instance.ActivateScene(sceneIndex);
        timelineManager.SwitchScene(sceneIndex);
        _animatedObjectPositions = new List<Vector3>();   
        
        // call StopAllAnimatedObjects
        StopAllAnimatedObjects();
        // call AnimateObject
        AnimateObject(animatedObjects[_selectedObjectIndex]);
        // Find the Scene3Controller
        // and set the camera position, rotation, FOV,
        // object index, position and rotation
        var scene3Controller = FindObjectOfType<Scene3Controller>();
        scene3Controller.cameraPosition = _mainCamera.transform.position;
        scene3Controller.cameraRotation = _mainCamera.transform.eulerAngles;
        scene3Controller.cameraFOV = _mainCamera.fieldOfView;
        scene3Controller.objectIndex = _selectedObjectIndex;
        scene3Controller.objectPosition = animatedObjects[_selectedObjectIndex].transform.position;
        scene3Controller.objectRotation = animatedObjects[_selectedObjectIndex].transform.eulerAngles;
        scene3Controller.StartScene();
        scene3Controller.timelineManager.SwitchScene(sceneIndex);

        for (int i = 0; i < animatedObjects.Count; i++)
        {
            _animatedObjectPositions.Add(animatedObjects[i].transform.position);
        }
        sceneRoot.SetActive(false);
        
    }
    
    public void TransitionToScene1(int  sceneIndex)
    {
        SceneLoader.Instance.ActivateScene(sceneIndex);
        timelineManager.SwitchScene(sceneIndex);
        
        // call StopAllAnimatedObjects
        StopAllAnimatedObjects();
        // call AnimateObject
        SpreadObject();
        
        var scene1Controller = FindObjectOfType<Scene1Controller>();
        scene1Controller.timelineManager.SwitchScene(sceneIndex);
        scene1Controller.StartScene();
    }
    
    // Switch OFF ObjectRotator For all animated objects
    public void StopAllAnimatedObjects()
    {
        foreach (var animatedObject in animatedObjects)
        {
            animatedObject.GetComponent<ObjectRotator>().enabled = false;
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
    public void AnimateObject(GameObject clickedObject)
    {
        _clickedObject = clickedObject;
        foreach (var animatedObject in animatedObjects)
        {
            if (animatedObject == clickedObject)
            {
                _selectedObjectIndex = animatedObjects.IndexOf(animatedObject);
            }
            else
            {
                // move object to the left side of the screen
                animatedObject.transform.DOMove(new Vector3(-10, 0, 0), 5f, false);
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
            // move object to the left side of the screen
            animatedObject.transform.DOMove( _animatedObjectStartPositions[animatedObjects.IndexOf(animatedObject)], 5f, false);
            animatedObject.GetComponent<MaterialFader>().FadeOut(() =>
            {
                animatedObject.SetActive(false);
                sceneRoot.SetActive(false);
            });
        }
    }
    
    public void ResetAnimatedObjects()
    {
        foreach (var animatedObject in animatedObjects)
        {
            if (animatedObject != _clickedObject)
            {
                Debug.Log("Reset Animated Object" + animatedObject.name);
                animatedObject.SetActive(true);
                animatedObject.transform.DOMove(_animatedObjectPositions[animatedObjects.IndexOf(animatedObject)], 2f,
                        false)
                    .OnComplete(() => { animatedObject.GetComponent<ObjectRotator>().enabled = true; });
                animatedObject.GetComponent<MaterialFader>().FadeIn(null);
            }
        }
    }
}
