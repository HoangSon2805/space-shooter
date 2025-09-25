using UnityEngine;

[RequireComponent(typeof(Health))]
public class HitFlashOnDamage : MonoBehaviour {
    Health _hp; HitFlash _fx; int _prev;

    void Awake() {
        _hp = GetComponent<Health>();
        _fx = GetComponent<HitFlash>();
        _prev = _hp ? _hp.maxHP : 1;
        if (_hp) _hp.OnChanged += OnChanged;
        if (_hp) _hp.OnDead += OnDead;
    }

    void OnChanged(int cur, int max) {
        if (_fx && cur < _prev) _fx.Trigger();
        _prev = cur;
    }

    void OnDead() {
        _fx?.Trigger();
        CameraShake2D.Inst?.Shake(0.25f, 0.15f);
    }
}
