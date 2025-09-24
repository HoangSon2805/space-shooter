using UnityEngine;
using TMPro;

public class ScoreHud : MonoBehaviour {
    public TextMeshProUGUI text;

    void OnEnable() {
        UpdateText(ScoreManager.Score);
        ScoreManager.OnScoreChanged += UpdateText;
    }

    void OnDisable() {
        ScoreManager.OnScoreChanged -= UpdateText;
    }

    void UpdateText(int s) {
        if (text) text.text = $"Score: {s}";
    }
}