using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static readonly string MainMenuScene = "MainMenu";
    private static readonly string GameScene = "Game";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene, LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
    }
}