using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 0.45f;
    [SerializeField] private float movementSharpness = 5f;

    [Header("Zoom")]
    [SerializeField] private float zoomSharpness = 10f;
    [SerializeField] private Vector3 zoomAmount = Vector3.zero;

    private Vector3 _newPosition = Vector3.zero;
    private Vector3 _newZoom = Vector3.zero;
    private Quaternion _newRotation = Quaternion.identity;

    private Vector3 _dragStartPosition = Vector3.zero;
    private Vector3 _dragCurrentPosition = Vector3.zero;
    private Vector3 _rotationStartPosition = Vector3.zero;
    private Vector3 _rotationCurrentPosition = Vector3.zero;

    private Transform _followTransform;

    private void Start()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = cameraTransform.localPosition;
    }

    private void LateUpdate()
    {
        if (Input.anyKey)
        {
            _followTransform = null;
        }

        if (_followTransform != null)
        {
            transform.position = _followTransform.position;
            return;
        }

        HandleMovementInput();
        HandleRotationInput();
        HandleCameraZoom();
        HandleMouseInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -movementSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * movementSharpness);
    }

    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * 10f);
    }

    private void HandleCameraZoom()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _newZoom += zoomAmount; 
        }

        if (Input.GetKey(KeyCode.X))
        {
            _newZoom -= zoomAmount;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * zoomSharpness);
    }

    private void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _newZoom += Input.mouseScrollDelta.y * zoomAmount; 
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);
                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            _rotationStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            _rotationCurrentPosition = Input.mousePosition;
            Vector3 difference = _rotationStartPosition - _rotationCurrentPosition;
            _rotationStartPosition = _rotationCurrentPosition;

            _newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    public void SetObjectToFollow(Transform target)
    {
        _followTransform = target;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public GameObject GetHoveredObject()
    {
        Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}