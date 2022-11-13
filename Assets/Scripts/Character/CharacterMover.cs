using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private UnityEvent jump;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void Reset()
    {
        transform.SetPositionAndRotation(_startPosition, _startRotation);
    }

    public void Jump()
    {
        var velocityY = Mathf.Sqrt(jumpHeight * Mathf.Abs(Physics.gravity.y));
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * _rigidbody.mass * velocityY, ForceMode.Impulse);
        jump?.Invoke();
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
}