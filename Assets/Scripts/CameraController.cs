using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Controls the behavior of a camera in response to mouse input.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("The speed at which the camera rotates around the scene in response to mouse movement.")] [SerializeField]
    private float rotationSpeed = 5f;

    [Tooltip("The speed at which the camera pans in response to mouse movement.")] [SerializeField]
    private float panSpeed = 5f;

    [Tooltip("The speed at which the camera zooms in and out in response to mouse scroll wheel movement.")]
    [SerializeField]
    private float zoomSpeed = 5f;

    [Tooltip("The minimum field of view the camera can have (controls the maximum zoom out).")] [SerializeField]
    private float minZoom = 5f;

    [Tooltip("The maximum field of view the camera can have (controls the maximum zoom in).")] [SerializeField]
    private float maxZoom = 100f;

    private Camera _camera; // Reference to the camera component attached to this object.
    private Transform _cameraTransform; // Reference to the transform component attached to the camera.

    private Vector3
        _previousMousePosition; // The position of the mouse on the previous frame (used to calculate the mouse delta).

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
        if (Input.GetMouseButtonDown(0)) _previousMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            var mouseDelta = Input.mousePosition - _previousMousePosition;

            var rotationX = mouseDelta.y * rotationSpeed;
            var rotationY = -mouseDelta.x * rotationSpeed;

            _cameraTransform.RotateAround(Vector3.zero, _cameraTransform.right, rotationX);
            _cameraTransform.RotateAround(Vector3.zero, Vector3.up, rotationY);

            _previousMousePosition = Input.mousePosition;
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            var zoom = _camera.fieldOfView - scroll * zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
    }
}