using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScoreView : MonoBehaviour
{
    private Text _text;
    private Player _player;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        UpdateScore(_player.HeightScore);
        _player.NewHeightScore.AddListener(UpdateScore); 
    }

    private void OnDisable()
    {
        _player.NewHeightScore.RemoveListener(UpdateScore); 
    }

    private void UpdateScore(int score)
    {
        _text.text = score.ToString();
    }
}