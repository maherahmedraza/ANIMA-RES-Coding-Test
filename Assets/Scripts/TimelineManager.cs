using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages timeline UI, allowing the user to switch between scenes and highlighting the selected scene
/// </summary>
public class TimelineManager : MonoBehaviour
{
    [Tooltip("List of buttons representing the scenes")]
    public List<Button> buttons;

    [Tooltip("Color of the button when selected")]
    public Color selectedColor;

    [Tooltip("Default color of the button")]
    public Color defaultColor;
    
    private int _previousSceneIndex;

    /// <summary>
    /// Start method called when the scene is loaded. Checks the UI and sets up the button for the current scene
    /// </summary>
    void Start()
    {
        CheckUI();
    }
    
    /// <summary>
    /// Check the UI and sets up the button for the current scene
    /// </summary>
    public void CheckUI()
    {
        // Get the index of current scene
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        // Set the button for the current scene to be interactable and highlight it
        _previousSceneIndex = currentSceneIndex;
        buttons[currentSceneIndex].interactable = false;
        buttons[currentSceneIndex].gameObject.GetComponent<Image>().color = selectedColor;
    }

    /// <summary>
    /// Switch to the selected scene and update the button UI accordingly
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to switch to</param>
    public void SwitchScene(int sceneIndex)
    {
         // Reset the previous scene button and highlight the selected scene button
        buttons[_previousSceneIndex].interactable = true;
        buttons[_previousSceneIndex].gameObject.GetComponent<Image>().color = defaultColor;
        _previousSceneIndex = sceneIndex;
        buttons[sceneIndex].interactable = false;
        buttons[sceneIndex].gameObject.GetComponent<Image>().color = selectedColor;
    }   
}
