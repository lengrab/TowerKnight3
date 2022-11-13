using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private CameraFollower cameraFollower;
    [SerializeField] private CharacterStateController characterStateController;
    [SerializeField] private CharacterMover characterMover;
    [SerializeField] private Player player;
    [SerializeField] private PlatformSpawner[] environtmentSpawners;
    [SerializeField] private TowerMover towerMover;

    public CameraFollower CameraFollower => cameraFollower;
    public CharacterStateController CharacterStateController => characterStateController;
    public CharacterMover CharacterMover => characterMover;
    public Player Player => player;


    private void Reset()
    {
        CameraFollower.Reset();
        CharacterMover.Restart();
        CharacterStateController.SetInGameState();
        Player.Reset();
        towerMover.Reset();

        foreach (var platformSpawner in environtmentSpawners)
        {
            platformSpawner.Reset();
        }
    }
    
    public void RestartGame()
    {
        Reset();
    }
}