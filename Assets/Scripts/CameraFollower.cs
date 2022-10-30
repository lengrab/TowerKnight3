using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _characterTransform;
    private float _offset;


    private void LateUpdate()
    {
        if (_characterTransform.position.y > transform.position.y + _offset)
        {
            transform.position = transform.position +
                                 Vector3.up * (_characterTransform.position.y - transform.position.y + _offset);
        }
    }
}