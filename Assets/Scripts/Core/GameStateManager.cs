using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public CameraFollower CameraFollower { get; private set; }
    public CharacterStateController CharacterStateController { get; private set; }
    public CharacterMover CharacterMover { get; private set; }
    public Player Player { get; private set; }

    private PlatformSpawner[] _platformSpawners;

    public void RestartGame()
    {
        Reset();
    }

    private void Reset()
    {
        CameraFollower.Reset();
        CharacterMover.Restart();
        CharacterStateController.SetInGameState();
        Player.Reset();
        
        foreach (var platformSpawner in _platformSpawners)
        {
            platformSpawner.Reset();
        }
    }

    private void Awake()
    {
        CameraFollower = FindObjectOfType<CameraFollower>();
        CharacterStateController = FindObjectOfType<CharacterStateController>();
        CharacterMover = FindObjectOfType<CharacterMover>();
        _platformSpawners = FindObjectsOfType<PlatformSpawner>();
        Player = FindObjectOfType<Player>();
    }
}