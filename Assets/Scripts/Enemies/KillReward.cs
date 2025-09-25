using UnityEngine;

[RequireComponent(typeof(Health))]
public class KillReward : MonoBehaviour {
    public int score = 10;
    public int xp = 5;
    void Awake() {
        var h = GetComponent<Health>();
        h.OnDead += () =>
        {
            ScoreManager.Add(score);
            var px = FindObjectOfType<PlayerExperience>();
            if (px) px.AddXP(xp);
        };
    }
}