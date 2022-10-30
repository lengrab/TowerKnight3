using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterStateController : MonoBehaviour
{
    [SerializeField] private UnityEvent _loose;
    [SerializeField] private float _deathHeight = 5;
    [SerializeField] private Transform _foot;

    public UnityEvent Loose => _loose;
    
    public event Action<Collider> TriggerEnter;

    public Transform Foot => _foot;
    public float DeathHeight => _deathHeight;
    public Player Player { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CharacterMover Mover { get; private set; }
    public Camera Camera { get; private set; }

    private IState _state;

    public void SetInGameState()
    {
        SetState(new GameState());
    }

    public void SetState(IState state)
    {
        _state?.Destroy();
        _state = state;
        _state.Init(this);
    }

    private void Awake()
    {
        Camera = Camera.main;
        Player = FindObjectOfType<Player>();
        Rigidbody = GetComponent<Rigidbody>();
        Mover = GetComponent<CharacterMover>();
        
        SetState(new GameState());
    }

    private void FixedUpdate()
    {
        _state.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }
}