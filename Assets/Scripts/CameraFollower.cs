using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;

    private float _offset;
    private Vector3 _startPosition;

    public void Reset()
    {
        transform.position = _startPosition;
    }

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void LateUpdate()
    {
        var position = transform.position;
        var characterPosition = characterTransform.position;

        if (characterPosition.y > position.y + _offset)
        {
            transform.position = position + Vector3.up * (characterPosition.y - position.y + _offset);
        }
    }
}