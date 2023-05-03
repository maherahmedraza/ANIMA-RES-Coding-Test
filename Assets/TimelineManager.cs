using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineManager : MonoBehaviour
{
    public List<Button> buttons;
    public Color selectedColor;
    public Color defaultColor;
    
    
    private int _previousSceneIndex;
    // This script takes list o buttons
    //and when the button the pressed go to the a scene 
    
    /*private static TimelineManager _instance;
    public static TimelineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TimelineManager>();
            }

            return _instance;
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        CheckUI();
    }
    
   // call start logic to check update of UI
   public void CheckUI()
   {
       //get the index of current scene
       int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
       Debug.Log("currentSceneIndex: " + currentSceneIndex);
       _previousSceneIndex = currentSceneIndex;
       buttons[currentSceneIndex].interactable = false;
       buttons[currentSceneIndex].gameObject.GetComponent<Image>().color = selectedColor;
   }

    public void SwitchScene(int sceneIndex)
    {
        Debug.Log("_previousSceneIndex: " + _previousSceneIndex);
        Debug.Log("_sceneIndex: " + sceneIndex);
        buttons[_previousSceneIndex].interactable = true;
        buttons[_previousSceneIndex].gameObject.GetComponent<Image>().color = defaultColor;
        _previousSceneIndex = sceneIndex;
        buttons[sceneIndex].interactable = false;
        buttons[sceneIndex].gameObject.GetComponent<Image>().color = selectedColor;
    }
    
}
