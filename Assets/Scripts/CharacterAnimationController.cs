using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private Animator _animator;

    public void Jump()
    {
        _animator.SetTrigger(JumpHash);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}