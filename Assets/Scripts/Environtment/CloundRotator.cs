using System;
using UnityEngine;

public class CloundRotator : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private void Update()
    {
        transform.Rotate(0, _speed, 0);
    }
}