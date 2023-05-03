using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInteractor : MonoBehaviour
{
    
    [SerializeField] private UnityEvent onClickEvent = default;
    [SerializeField] private UnityEvent onDragEndEvent = default;
    private CameraController _cameraController;
    private bool _isRotating;
    private bool _isDragging;
    private Vector3 _lastMousePosition;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        _isRotating = true;
        if (_cameraController != null) _cameraController.enabled = false;
        _lastMousePosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        _isRotating = false;
        if (_cameraController != null) _cameraController.enabled = true;
        if (!_isDragging)
        {
            onClickEvent?.Invoke();
        }
        else
        {
            onDragEndEvent?.Invoke();
        }
        _isDragging = false;
    }

    private void OnMouseDrag()
    {
        if (_isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, -mouseX * 5, Space.World);
            transform.Rotate(Vector3.right, mouseY * 5, Space.World);

            // Check if mouse position has changed
            if (Input.mousePosition != _lastMousePosition)
            {
                _isDragging = true;
            }
        }

        _lastMousePosition = Input.mousePosition;
    }
}