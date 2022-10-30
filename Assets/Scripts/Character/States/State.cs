public abstract class State
{
    protected CharacterStateController Controller { get; private set; }

    protected void Init(CharacterStateController controller)
    {
        Controller = controller;
    }

    public abstract void Update();

    public abstract void Destroy();
}