using System;
using UnityEngine;

public class ObjectTransitionController : MonoBehaviour
{
    public Scene2Controller scene2Controller;

    private void Start()
    {
        scene2Controller = FindObjectOfType<Scene2Controller>();
    }

    public void TriggerSceneTransition(GameObject gameObjectToAnimate)
    {
       
        scene2Controller.SelectedObjectIndex = scene2Controller.animatedObjects.IndexOf(gameObjectToAnimate);
        // call TransitionToScene3
        scene2Controller.TransitionToScene3(2);
    }
}