using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    [FormerlySerializedAs("_currentSceneIndex")] public int currentSceneIndex; // Index of the current scene
    private int _totalNumberOfScenes; // Total number of scenes in the build settings

    // Singleton
    private static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneLoader>();
            }

            return _instance;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentSceneIndex);
        _totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;
    }
    
    /// <summary>
    /// Load all scenes asynchronously except the current one
    /// </summary>
    public void LoadAllScenesAsync()
    {
        for (int i = 0; i < _totalNumberOfScenes; i++)
        {
            if (i != currentSceneIndex)
            {
                SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            }
        }
    }
    
    /// <summary>
    /// Load a scene asynchronously by its index
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to load</param>
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        currentSceneIndex = sceneIndex;
    }
    
    /// <summary>
    /// Unload a scene asynchronously by its index
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to unload</param>
    public void UnloadScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }
    
    /// <summary>
    /// Activate a scene by its index
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to activate</param>
    public void ActivateScene(int sceneIndex)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
    
    /// <summary>
    /// Deactivate a scene by its index
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to deactivate</param>
    public void DeactivateScene(int sceneIndex)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
}
