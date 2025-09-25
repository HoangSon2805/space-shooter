using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {
    public int bulletDamage = 1;
    public float fireRateMult = 1f;
    public float moveSpeedMult = 1f;
    public int extraProjectiles = 0;
    public float spreadAngle = 15f;
    public int maxHPBonus = 0;

    Health _hp; int _baseMaxHP;

    void Awake() {
        _hp = GetComponent<Health>();
        if (_hp) _baseMaxHP = _hp.maxHP;
    }

    public void ApplyMaxHP() {
        if (!_hp) return;
        int prev = _hp.maxHP;
        _hp.maxHP = _baseMaxHP + maxHPBonus;
        if (_hp.maxHP > prev) _hp.Heal(_hp.maxHP - prev);
    }
}