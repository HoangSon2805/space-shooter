using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour {
    public static int Score { get; private set; }
    public static event Action<int> OnScoreChanged;

    void Awake() {
        Score = 0; // reset khi vào scene
        OnScoreChanged?.Invoke(Score);
    }

    public static void Add(int v) {
        Score += v;
        OnScoreChanged?.Invoke(Score);
    }
}