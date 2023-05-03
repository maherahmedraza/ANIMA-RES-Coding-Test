using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to Object around which the camera will rotate
/// </summary>
public class FreeLookCamera : MonoBehaviour
{
     public float rotateSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 10f;

    private Transform cameraTransform;
    private Quaternion cameraStartRotation;
    private bool isObjectSelected;
    private Vector3 lastMousePosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraStartRotation = cameraTransform.rotation;
    }

    private void Update()
    {
        if (!isObjectSelected)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the camera around the scene
            if (Input.GetMouseButton(0))
            {
                cameraTransform.RotateAround(Vector3.zero, Vector3.up, mouseX * rotateSpeed);
                cameraTransform.RotateAround(Vector3.zero, cameraTransform.right, -mouseY * rotateSpeed);
            }

            // Zoom the camera in and out
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");
            float zoomDistance = Vector3.Distance(cameraTransform.position, Vector3.zero);
            if (zoomInput != 0 && (zoomInput < 0 || zoomDistance > minZoomDistance))
            {
                cameraTransform.Translate(Vector3.forward * zoomInput * zoomSpeed, Space.Self);
                float clampedDistance = Mathf.Clamp(zoomDistance, minZoomDistance, maxZoomDistance);
                cameraTransform.position = cameraTransform.position.normalized * clampedDistance;
            }
        }

        // Handle object rotation
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject == gameObject)
                {
                    isObjectSelected = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isObjectSelected = false;
        }

        if (isObjectSelected)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            transform.Rotate(Vector3.up, mouseDelta.x * rotateSpeed);
            transform.Rotate(Camera.main.transform.right, -mouseDelta.y * rotateSpeed, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void LateUpdate()
    {
        // Reset camera rotation if object is selected
        if (isObjectSelected)
        {
            cameraTransform.rotation = cameraStartRotation;
        }
    }
}
