public interface IState
{
    public void Init(CharacterStateController controller);

    public abstract void Update();

    public abstract void Destroy();
}