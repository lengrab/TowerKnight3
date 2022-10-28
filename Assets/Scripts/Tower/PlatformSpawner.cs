using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _platformTemplates;
    [SerializeField] private int _poolSize = 3;
    [SerializeField] private float _minRangeBetweenPlatform = 1;
    [SerializeField] private float _maxRangeBetweenPlatform = 3;
    [SerializeField] private float _maxAngleRange = 90f;
    [SerializeField] private GameObject _start;
    private Queue<GameObject> _platformQueue;
    private GameObject _lastPlatform;

    public void Reset()
    {
        foreach (var platform in _platformQueue)
        {
            Destroy(platform);
        }
    }

    public void Generate(int count)
    {
        if (_platformTemplates.Count < 1)
        {
            return;
        }


        for (var i = 0; i < count; i++)
        {
            if (IsFullQueue())
            {
                GameObject platform = _platformQueue.Dequeue();
                platform.transform.position = _lastPlatform.transform.position +
                                              Vector3.up * Random.Range(_minRangeBetweenPlatform,
                                                  _maxRangeBetweenPlatform); // TODO Add perlin noise
                _platformQueue.Enqueue(platform);
                _lastPlatform = platform;
            }
            else
            {
                GameObject platform = Instantiate(_platformTemplates[Random.Range(0, _platformTemplates.Count)],
                    transform);
                platform.transform.position = _lastPlatform.transform.position +
                                              Vector3.up * Random.Range(_minRangeBetweenPlatform,
                                                  _maxRangeBetweenPlatform); // TODO Add perlin noise
                platform.transform.rotation = Quaternion.Euler(platform.transform.rotation.eulerAngles.x,
                    GetRandomAngle(_lastPlatform.transform.rotation.eulerAngles.y),
                    platform.transform.rotation.eulerAngles.z);
                _platformQueue.Enqueue(platform);
                _lastPlatform = platform;
            }
        }
    }

    private bool IsFullQueue() => _platformQueue?.Count >= _poolSize;

    private float GetRandomAngle(float angle)
    {
        var isPositiveRotation = Random.Range(0, 10) > 5;
        float deltaAngle = Random.Range(_maxAngleRange / 2, _maxAngleRange);

        if (isPositiveRotation)
        {
            return angle + deltaAngle;
        }
        else
        {
            return angle - deltaAngle;
        }
    }

    private void Awake()
    {
        _platformQueue = new Queue<GameObject>();
        _lastPlatform = _start;
        Generate(3);
    }
}