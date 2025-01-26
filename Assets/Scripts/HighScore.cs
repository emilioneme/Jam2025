using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static HighScore Instance { get; private set; }

    private int currentHighScore = 0;

    // Awake is called before Start and ensures the singleton pattern
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;    // Set this as the singleton instance
            DontDestroyOnLoad(gameObject); // Make it persistent across scenes
        }
    }

    // Method to get the high score
    public int GetHighScore()
    {
        return currentHighScore;
    }

    // Method to update the high score
    public void UpdateHighScore(int score)
    {
        if (score > currentHighScore)
        {
            currentHighScore = score;
            Debug.Log($"New High Score: {currentHighScore}");
        }
    }
}

