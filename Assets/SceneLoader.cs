using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    // Index of the current scene
    [FormerlySerializedAs("_currentSceneIndex")] public int currentSceneIndex;
    private int _totalNumberOfScenes;

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
        print(currentSceneIndex);
        //check the total number of scenes in the build settings
        _totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;
        
        //LoadAllScenesAsync();
    }
    
    /// <summary>
    /// Load all scenes asynchronously except the current one
    /// </summary>
    public void LoadAllScenesAsync()
    {
        // Load all scenes asynchronously except the current one
        for (int i = 0; i < _totalNumberOfScenes; i++)
        {
            if (i != currentSceneIndex)
            {
                SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            }
        }
    }
    
    
    public void LoadScene(int sceneIndex)
    {
        // Load the new scene
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        // Update the current scene index
        currentSceneIndex = sceneIndex;
    }
    
    public void UnloadScene(int sceneIndex)
    {
        // Unload the current scene
        SceneManager.UnloadSceneAsync(sceneIndex);
    }
    
    public void ActivateScene(int sceneIndex)
    {
        // Activate the scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
    
    public void DeactivateScene(int sceneIndex)
    {
        // Deactivate the scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
}
