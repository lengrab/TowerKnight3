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
    [SerializeField] private GameObject _startObject;
    [SerializeField] private GameStateManager _gameManager;

    private CharacterMover _character;
    private GameObject _lastPlatform;
    private Queue<GameObject> _platformQueue;
    private float _seed;

    private bool IsFullQueue() => _platformQueue.Count >= _poolSize;


    public void Reset()
    {
        _lastPlatform = _startObject;
        _seed = Random.Range(0, 10000f);
        Generate(_platformQueue.Count);
    }

    private void Awake()
    {
        _platformQueue = new Queue<GameObject>();
        _lastPlatform = _startObject;
        Generate(_poolSize / 3);
    }

    private void Start()
    {
        _character = _gameManager.CharacterMover;
    }

    private void FixedUpdate()
    {
        if (_character.transform.position.y + _offsetForGeneration > _lastPlatform.transform.position.y)
            Generate(_poolSize / 3);
    }

    private void Generate(int count)
    {
        if (_platformTemplates.Count < 1) return;

        GameObject platform;

        for (var i = 0; i < count; i++)
        {
            if (IsFullQueue())
            {
                platform = _platformQueue.Dequeue();
            }
            else
            {
                platform = Instantiate(_platformTemplates[Random.Range(0, _platformTemplates.Count)],
                    transform);
            }

            SetRandomPosition(platform);
            SetRandomRotation(platform);
            _lastPlatform = platform;
            _platformQueue.Enqueue(platform);
        }
    }

    private void SetRandomPosition(GameObject objectForChange)
    {
        var position = _lastPlatform.transform.position;
        objectForChange.transform.position = position + Vector3.up *
            Mathf.Clamp(Mathf.PerlinNoise(_seed + position.y * _noiseScale, 0), _minRangeBetweenPlatform,
                _maxRangeBetweenPlatform);
    }

    private void SetRandomRotation(GameObject objectForChange)
    {
        objectForChange.transform.Rotate(0, GetRandomAngle(), 0);
    }

    private float GetRandomAngle()
    {
        var range = 10f;
        var noise = Mathf.PerlinNoise(0, _seed + _lastPlatform.transform.position.y * _noiseScale);
        var isPositiveRotation = Random.Range(0, noise * range) - range / 2 >= 0;
        var deltaAngle = Mathf.Clamp(noise * _maxAngleRange, _maxAngleRange / 4, _maxAngleRange);
        var angle = _lastPlatform.transform.rotation.eulerAngles.y;

        if (isPositiveRotation)
        {
            return angle + deltaAngle;
        }

        return angle - deltaAngle;
    }
}