using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _platformTemplates;
    [SerializeField] private int _poolSize = 3;
    [SerializeField] private int _offsetForGeneration = 3;
    [SerializeField] private float _noiseScale = 3;
    [SerializeField] private float _minRangeBetweenPlatform = 1;
    [SerializeField] private float _maxRangeBetweenPlatform = 3;
    [SerializeField] private float _maxAngleRange = 90f;
    [SerializeField] private GameObject _start;

    private float _seed;
    private CharacterMover _character;
    private Queue<GameObject> _platformQueue;
    private GameObject _lastPlatform;

    public void Reset()
    {
        foreach (var platform in _platformQueue)
        {
            Destroy(platform.gameObject);
        }

        _platformQueue = new Queue<GameObject>();
        _lastPlatform = _start;
        _seed = Random.Range(0, 10000f);
        Generate(_poolSize / 3);
    }

    private void Generate(int count)
    {
        if (_platformTemplates.Count < 1)
        {
            return;
        }


        for (var i = 0; i < count; i++)
        {
            if (IsFullQueue())
            {
                var platform = _platformQueue.Dequeue();
                platform.transform.position = _lastPlatform.transform.position +
                                              Vector3.up * Random.Range(_minRangeBetweenPlatform,
                                                  _maxRangeBetweenPlatform); // TODO Add perlin noise
                _platformQueue.Enqueue(platform);
                _lastPlatform = platform;
            }
            else
            {
                var platform = Instantiate(_platformTemplates[Random.Range(0, _platformTemplates.Count)],
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

    private bool IsFullQueue() => _platformQueue.Count >= _poolSize;

    private float GetRandomAngle(float angle)
    {
        var range = 10f;
        var noise = Mathf.PerlinNoise(0, _seed + _lastPlatform.transform.position.y * _noiseScale);
        var isPositiveRotation = Random.Range(0, noise * range) - range / 2 >= 0;
        var deltaAngle = Mathf.Clamp(noise * _maxAngleRange, _maxAngleRange / 4, _maxAngleRange);

        if (isPositiveRotation)
        {
            return angle + deltaAngle;
        }
        else
        {
            return angle - deltaAngle;
        }
    }

    private void FixedUpdate()
    {
        if (_character.transform.position.y + _offsetForGeneration > _lastPlatform.transform.position.y)
        {
            Generate(_poolSize / 3);
        }
    }

    private void Awake()
    {
        _platformQueue = new Queue<GameObject>();
        _character = FindObjectOfType<CharacterMover>();
        Reset();
    }
}