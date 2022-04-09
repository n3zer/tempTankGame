using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField, Range(1f, 10000f)] private float _speed = 10f;
    [SerializeField, Range(1f, 70f)] private float _rotAngle = 30f;
    [SerializeField] private List<Wheel> _wheels = new List<Wheel>();
    
    private Vector2 _GetInput
    {
        get
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            return new Vector2(ver, hor);
        }
    }


    private List<WheelCollider> _motorWheel = new List<WheelCollider>();
    private List<WheelCollider> _rotateWheel = new List<WheelCollider>();
    private void Start()
    {
        foreach (var wheel in _wheels)
        {
            if (wheel.motor)
            {
                _motorWheel.Add(wheel.collider);
            }
            else
            {
                _rotateWheel.Add(wheel.collider);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        var input = _GetInput;
        if (input != null && input != Vector2.zero)
            MoveWheel(_GetInput, Input.GetButton("Jump"));

    }


    private void MoveWheel(Vector2 input, bool stoping)
    {
        if (stoping) input.x = 0;
        foreach (var wheel in _motorWheel)
        {
            wheel.motorTorque = input.x * _speed;
        }
        foreach(var wheel in _wheels)
        {
            RotateWheel(wheel.collider, wheel.transform);
        }
        foreach (var wheel in _rotateWheel)
        {
            wheel.steerAngle = input.y * _rotAngle;
        }
    }

    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.rotation = rot;
        transform.position = pos;
    }
}

[System.Serializable]
public struct Wheel
{
    public WheelCollider collider;
    public Transform transform;
    public bool motor;
}

