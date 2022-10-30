using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private UnityEvent _jump;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody _rigidbody;

    public void Jump()
    {
        var velocityY = Mathf.Sqrt(_jumpHeight * Mathf.Abs(Physics.gravity.y));
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * _rigidbody.mass * velocityY, ForceMode.Impulse);
        _jump?.Invoke();
    }


    public void Play()
    {
        _rigidbody.isKinematic = false;
    }

    public void Stop()
    {
        _rigidbody.isKinematic = true;
    }

    public void Restart()
    {
        Reset();
        Play();
    }

    private void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
}