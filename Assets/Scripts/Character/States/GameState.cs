using UnityEngine;

public class GameState : State, IState
{
    public void Init(CharacterStateController controller)
    {
        base.Init(controller);
        Controller.TriggerEnter += TriggerEntered;
    }

    public override void Update()
    {
        Controller.Player.SetHeight(Controller.Camera.transform.position.y);
        Vector3 distance = Controller.Camera.transform.position - Controller.transform.position;

        if (distance.y < Controller.DeathHeight) return;
        
        Controller.Loose?.Invoke();
        Controller.SetState(new DeathState());
    }

    public override void Destroy()
    {
        Controller.TriggerEnter -= TriggerEntered;
    }

    private void TriggerEntered(Collider other)
    {
        if (Vector3.Dot(other.transform.up, Controller.Rigidbody.velocity.normalized) < 0.1f)
        {
            Collider[] colliders = Physics.OverlapSphere(Controller.Foot.position, 0.1f);
            
            if (colliders.Length < 1)
            {
                return;
            }

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Platform>(out Platform platform))
                {
                    Controller.Mover.Jump();
                }

            }
        }
    }
}