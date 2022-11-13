using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private const string MaxScoreHash = "MaxScore";

    public UnityEvent<int> NewHeightScore;
    public UnityEvent<int> UpdateScore;
   
    public int Score { get; private set; }
    public int HeightScore { get; private set; }

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

        if (Score <= HeightScore) return;

        HeightScore = Score;
        NewHeightScore?.Invoke(HeightScore);
    }

    private void Load()
    {
        HeightScore = ES3.Load<int>(MaxScoreHash, 0);
    }

    private void Save()
    {
        ES3.Save(MaxScoreHash, HeightScore);
    }
}