using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _panSpeed = 5f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 100f;

    private Camera _camera;
    private Transform _cameraTransform;

    private Vector3 _previousMousePosition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraTransform = _camera.transform;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - _previousMousePosition;

            float rotationX = mouseDelta.y * _rotationSpeed;
            float rotationY = -mouseDelta.x * _rotationSpeed;

            _cameraTransform.RotateAround(Vector3.zero, _cameraTransform.right, rotationX);
            _cameraTransform.RotateAround(Vector3.zero, Vector3.up, rotationY);

            _previousMousePosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float zoom = _camera.fieldOfView - scroll * _zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(zoom, _minZoom, _maxZoom);
        }
    }
}