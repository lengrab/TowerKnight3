using UnityEngine;

public class CharacterScaller : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private float scaleCoefficient = 5;
    
    private Vector3 _baseScale;

    private void Awake()
    {
        _baseScale = character.localScale;
    }

    private void Update()
    {
        character.localScale = _baseScale + Vector3.up * ((character.transform.position.y - transform.position.y) * scaleCoefficient);
    }
}