using System;
using UnityEngine;

public class CharacterScaller : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private float _scaleKoefficient = 5;
    private Vector3 _baseScale;

    private void Awake()
    {
        _baseScale = _character.localScale;
    }

    private void Update()
    {
        _character.localScale = _baseScale + Vector3.up * (transform.position.y * _scaleKoefficient);
    }
}