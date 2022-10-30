using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Player _player;

    public void UpdateScore(int score)
    {
        _text.text = score.ToString();
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            gameObject.SetActive(false);
        }
        UpdateScore(_player.Score);
    }

    private void OnEnable()
    {
        _player.UpdateScore.AddListener(UpdateScore); 
    }

    private void OnDisable()
    {
        _player.UpdateScore.RemoveListener(UpdateScore); 
    }
}