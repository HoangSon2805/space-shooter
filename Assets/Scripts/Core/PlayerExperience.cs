using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour {
    public int level = 1;
    public int xp = 0;

    public int baseXp = 20;     // EXP cho level 1→2
    public float growth = 1.2f; // mỗi cấp tăng 20%
    public int xpToNext = 20;

    public event Action<int> OnLevelUp; // level mới

    void Awake() {
        // Đồng bộ xpToNext theo level hiện tại
        xpToNext = NextForLevel(level);
    }

    public void AddXP(int v) {
        xp += Mathf.Max(0, v);
        while (xp >= xpToNext)
        {
            xp -= xpToNext;
            level++;
            OnLevelUp?.Invoke(level);
            xpToNext = NextForLevel(level);
        }
    }

    int NextForLevel(int lv) {
        // EXP cần cho lv hiện tại → lv+1
        // Ceil để tránh bị 0 khi lv thấp và growth nhỏ
        float need = baseXp * Mathf.Pow(growth, Mathf.Max(0, lv - 1));
        return Mathf.Clamp(Mathf.CeilToInt(need), 1, int.MaxValue);
    }
}
