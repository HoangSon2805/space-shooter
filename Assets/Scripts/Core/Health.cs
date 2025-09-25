using UnityEngine;
using System;

public class Health : MonoBehaviour {
    public int maxHP = 3;
    int _hp;

    public int Current => _hp;
    public event Action<int, int> OnChanged; // (current, max)
    public event Action OnDead;

    void OnEnable() {
        _hp = maxHP;
        OnChanged?.Invoke(_hp, maxHP);
    }

    public bool TakeDamage(int dmg) {
        if (dmg <= 0) return false;
        _hp = Mathf.Max(0, _hp - dmg);
        OnChanged?.Invoke(_hp, maxHP);
        if (_hp == 0)
        {
            OnDead?.Invoke();
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
    public void Heal(int v) {
        if (v <= 0) return;
        _hp = Mathf.Min(maxHP, _hp + v);
        OnChanged?.Invoke(_hp, maxHP);
    }
}