using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public UnityEvent Jump;
    public UnityEvent<float> Rotate;

    private Input _input;
    private IEnumerator _valueReader;

    private void Awake()
    {
        _input = new Input();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.InGame.Jump.performed += OnJump;
        _input.InGame.Scroll.started += OnStartScroll;
        _input.InGame.Scroll.performed += OnStopScroll;
        _input.InGame.Scroll.canceled += OnStopScroll;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.InGame.Jump.performed -= OnJump;
        _input.InGame.Scroll.started -= OnStartScroll;
        _input.InGame.Scroll.performed -= OnStopScroll;
        _input.InGame.Scroll.canceled -= OnStopScroll;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jump.Invoke();
    }

    private void OnStartScroll(InputAction.CallbackContext context)
    {
        if (_valueReader != null)
        {
            StopCoroutine(_valueReader);
        }

        _valueReader = ReadValue(context);
        StartCoroutine(_valueReader);
    }

    private void OnStopScroll(InputAction.CallbackContext context)
    {
        StopCoroutine(_valueReader);
    }

    private IEnumerator ReadValue(InputAction.CallbackContext context)
    {
        while (true)
        {
            Rotate.Invoke(context.ReadValue<float>());
            yield return new WaitForFixedUpdate();
        }
    }
}