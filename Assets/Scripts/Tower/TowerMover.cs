using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TowerMover : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public void Move(float direction)
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion quaternion = Quaternion.Euler(currentRotation.eulerAngles.x,currentRotation.eulerAngles.y + direction,currentRotation.eulerAngles.z);
        _rigidbody.MoveRotation(quaternion);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
