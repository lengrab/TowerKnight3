using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 2;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        var velocityY = Mathf.Sqrt(_jumpHeight * Mathf.Abs(Physics.gravity.y));
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * _rigidbody.mass * velocityY, ForceMode.Impulse);
    }
}