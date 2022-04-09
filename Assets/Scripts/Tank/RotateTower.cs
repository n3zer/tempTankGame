using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTower : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;
    private float _rotTowerX;

    [SerializeField] private Transform _target;

    [SerializeField] private float _distanceFromTarget = 3.0f;
    [SerializeField, Range(0f, 10f)] private float _distanceToTargetStep = 0.1f;
    [SerializeField] private float _minDistance = 5f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] private float _smoothTime = 0.2f;

    [SerializeField] private Vector2 _rotationXMinMax = new Vector2(-40, 40);


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        _rotTowerX += mouseX;

        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;
        transform.position = _target.position - transform.forward * _distanceFromTarget;

        _target.transform.localRotation = Quaternion.Euler(0, _rotTowerX, 0);

        float newDistance = _distanceToTargetStep * Input.GetAxis("Mouse ScrollWheel");
        if (_distanceFromTarget - newDistance >= _minDistance)
            _distanceFromTarget -= newDistance;

    }

}




