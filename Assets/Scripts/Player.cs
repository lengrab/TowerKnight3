using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private const string MaxScoreHash = "MaxScore";

    public UnityEvent<int> NewHeightScore;
    public UnityEvent<int> UpdateScore;
    public int Score { get; private set; }
    public int MaxScore { get; private set; }

    public void SetHeight(float height)
    {
        if (height < 0)
        {
            return;
        }

        if (height > Score)
        {
            Score = Convert.ToInt32(height);
        }
        
        UpdateScore?.Invoke(Score);

        if (Score > MaxScore)
        {
            MaxScore = Score;
            NewHeightScore?.Invoke(MaxScore);
        }
    }

    private void Awake()
    {
        Load();
    }

    public void Reset()
    {
        Score = 0;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        Save();
    }
    
    private void Load()
    {
        ES3.Save(MaxScoreHash, MaxScore);
    }

    private void Save()
    {
        MaxScore = ES3.Load(MaxScoreHash, 0);
    }
}