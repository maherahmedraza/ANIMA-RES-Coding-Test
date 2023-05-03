using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Vector3 rotationAxis = Vector3.up;
    public float orbitRadius = 1f;
    public bool rotate = true;
    public GameObject objectToRotate;

    private GameObject _pivot;
    // Start is called before the first frame update
    void Start()
    {
        _pivot = gameObject;
        // set object position in a orbit radius
        if (objectToRotate != null)
            objectToRotate.transform.position = _pivot.transform.position + new Vector3(orbitRadius, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToRotate && rotate)
        {
            // keep the rotating object in the orbit radius
            objectToRotate.transform.position = _pivot.transform.position +
                                                (objectToRotate.transform.position - _pivot.transform.position)
                                                .normalized * orbitRadius;
            
            // Rotate the object around the pivot in an orbit with a given radius
            objectToRotate.transform.RotateAround(_pivot.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
            
            
            
        }
    }
}
