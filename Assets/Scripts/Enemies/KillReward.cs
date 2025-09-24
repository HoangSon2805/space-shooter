using UnityEngine;

[RequireComponent(typeof(Health))]
public class KillReward : MonoBehaviour {
    public int score = 10;

    void Awake() {
        var h = GetComponent<Health>();
        h.OnDead += () => ScoreManager.Add(score);
    }
}