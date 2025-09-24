using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHP = 5;
    int _hp;

    void OnEnable() { _hp = maxHP; }

    public void TakeDamage(int dmg) {
        _hp = Mathf.Max(0, _hp - Mathf.Max(0, dmg));
        if (_hp == 0) OnDie();
    }

    void OnDie() {
        gameObject.SetActive(false);
        // Có thể phát sự kiện chết hoặc spawn VFX về sau
    }
}
