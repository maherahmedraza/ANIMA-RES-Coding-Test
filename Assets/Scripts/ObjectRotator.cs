using UnityEngine;

/// <summary>
/// A class that rotates an object around a pivot point in an orbit with a given radius
/// </summary>
public class ObjectRotator : MonoBehaviour
{
    [Tooltip("The speed at which the object rotates")]
    public float rotationSpeed = 10f;

    [Tooltip("The axis around which the object rotates")]
    public Vector3 rotationAxis = Vector3.up;

    [Tooltip("The radius of the orbit")]
    public float orbitRadius = 1f;

    [Tooltip("Whether the object should rotate")]
    public bool rotate = true;

    [Tooltip("The object to rotate")]
    public GameObject objectToRotate;

    // The pivot point around which the object rotates
    private GameObject _pivot;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        _pivot = gameObject;
        // set object position in a orbit radius
        if (objectToRotate != null)
            objectToRotate.transform.position = _pivot.transform.position + new Vector3(orbitRadius, 0f, 0f);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
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