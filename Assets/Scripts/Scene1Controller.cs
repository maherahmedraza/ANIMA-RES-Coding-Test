using UnityEngine;
using UnityEngine.Events;

public class Scene1Controller : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] public TimelineManager timelineManager;
    [SerializeField] private GameObject sceneRoot;
    [SerializeField] private GameObject titleText;
    [SerializeField] private UnityEvent startEvent;
    [SerializeField] private Camera mainCamera;

    // Private Fields
    private Scene2Controller scene2Controller;
    private Scene3Controller scene3Controller;
    
    //Public Fields

    // Methods
    private void Start()
    {
        // Invoke start event
        startEvent?.Invoke();

        // If no timeline manager was assigned, find it in the scene
        if (timelineManager == null)
        {
            timelineManager = FindObjectOfType<TimelineManager>();
        }
    }
    
    public void StartScene()
    {
        Debug.Log("Start Scene");

        // Activate scene root
        sceneRoot.SetActive(true);

        // Invoke start event
        startEvent?.Invoke();
    }

    /// <summary>
    /// Transition to Scene2
    /// </summary>
    /// <param name="sceneIndex">The build index of Scene 2 </param>
    public void TransitionToScene2(int sceneIndex)
    {
        // Get the current scene index
        int currentSceneIndex = SceneLoader.Instance.currentSceneIndex;

        // Find Scene2Controller in the scene
        if (scene2Controller == null)
        {
            scene2Controller = FindObjectOfType<Scene2Controller>();
        }

        // Set animated objects start positions and clicked object to null
        scene2Controller.SetAnimatedObjectStartPositions();
        scene2Controller.ClickedObject = null;

        // Fade out title text and switch to Scene2
        titleText.GetComponent<TextFader>().FadeOut(() =>
        {
            // Deactivate scene root
            sceneRoot.SetActive(false);

            // Activate Scene2
            SceneLoader.Instance.ActivateScene(sceneIndex);
            scene2Controller.StartScene();
        });

        // Switch timeline to Scene2
        timelineManager.SwitchScene(sceneIndex);
        scene2Controller.timelineManager.SwitchScene(sceneIndex);
    }
    
    public void TransitionToScene3(int sceneIndex)
    {
        // Get the current scene index
        int currentSceneIndex = SceneLoader.Instance.currentSceneIndex;

        // Find Scene3Controller in the scene
        if (scene3Controller == null)
        {
            scene3Controller = FindObjectOfType<Scene3Controller>();
        }
        
        // Fade out title text and switch to Scene3
        titleText.GetComponent<TextFader>().FadeOut(() =>
        {
            // Deactivate scene root
            sceneRoot.SetActive(false);

            // Activate Scene3
            SceneLoader.Instance.ActivateScene(sceneIndex);

            // Set camera position, rotation and field of view
            scene3Controller.cameraPosition = mainCamera.transform.position;
            scene3Controller.cameraRotation = mainCamera.transform.eulerAngles;
            scene3Controller.cameraFOV = mainCamera.fieldOfView;
            scene3Controller.StartScene();
        });

        // Switch timeline to Scene3
        timelineManager.SwitchScene(sceneIndex);
        scene3Controller.timelineManager.SwitchScene(sceneIndex);
    }
}
