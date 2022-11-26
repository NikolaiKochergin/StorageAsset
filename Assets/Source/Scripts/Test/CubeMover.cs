using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CubeMover : MonoBehaviour
{
    private Collider _collider;
    private Plane _plane;
    private bool _isMoving;
    private bool _isRotating;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _plane = new Plane(Camera.main.transform.forward, transform.position);
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
            if (_collider.Raycast(ray, out var hitInfo, 100f))
                _isMoving = true;
            else
                _isRotating = true;
        if (Input.GetMouseButtonUp(0))
        {
            _isMoving = false;
            _isRotating = false;
        }
        if (_isMoving)
        {
            _plane.Raycast(ray, out var distance);
            transform.position = ray.GetPoint(distance);
        }
        if(_isRotating)
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * 10, -Input.GetAxis("Mouse X") * 10, 0);
        }
    }
}