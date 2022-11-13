using System;
using UnityEngine;

public class CloundRotator : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    private void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}