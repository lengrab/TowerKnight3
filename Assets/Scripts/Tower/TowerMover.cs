using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TowerMover : MonoBehaviour
{
    [SerializeField] private float _moveSensity = 0.5f;

    private Rigidbody _rigidbody;
    private Quaternion _startRotation;

    public void Reset()
    {
        transform.rotation = _startRotation;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(float direction)
    {
        direction *= _moveSensity;
        Quaternion currentRotation = transform.rotation;
        Quaternion quaternion = Quaternion.Euler(currentRotation.eulerAngles.x,
            currentRotation.eulerAngles.y + direction, currentRotation.eulerAngles.z);
        _rigidbody.MoveRotation(quaternion);
    }
}