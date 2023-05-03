using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Controls the behavior of a camera in response to mouse input.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;   // The speed at which the camera rotates around the scene in response to mouse movement.
    [SerializeField] private float panSpeed = 5f;        // The speed at which the camera pans in response to mouse movement.
    [SerializeField] private float zoomSpeed = 5f;       // The speed at which the camera zooms in and out in response to mouse scroll wheel movement.
    [SerializeField] private float minZoom = 5f;         // The minimum field of view the camera can have (controls the maximum zoom out).
    [SerializeField] private float maxZoom = 100f;       // The maximum field of view the camera can have (controls the maximum zoom in).

    private Camera _camera;                              // Reference to the camera component attached to this object.
    private Transform _cameraTransform;                   // Reference to the transform component attached to the camera.

    private Vector3 _previousMousePosition;               // The position of the mouse on the previous frame (used to calculate the mouse delta).

    /// <summary>
    /// Initializes the camera and its transform.
    /// </summary>
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraTransform = _camera.transform;
    }

    /// <summary>
    /// Responds to mouse input every frame.
    /// </summary>
    private void Update()
    {
        HandleMouseInput();
    }

    /// <summary>
    /// Calculates and applies the changes to the camera based on mouse input.
    /// </summary>
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - _previousMousePosition;

            float rotationX = mouseDelta.y * rotationSpeed;
            float rotationY = -mouseDelta.x * rotationSpeed;

            _cameraTransform.RotateAround(Vector3.zero, _cameraTransform.right, rotationX);
            _cameraTransform.RotateAround(Vector3.zero, Vector3.up, rotationY);

            _previousMousePosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float zoom = _camera.fieldOfView - scroll * zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
    }
}
