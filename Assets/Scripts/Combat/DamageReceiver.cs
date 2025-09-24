using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DamageReceiver : MonoBehaviour {
    public float invulnDuration = 1.0f;   // thời gian vô địch sau khi trúng
    public float blinkInterval = 0.1f;    // nháy sprite
    bool _invuln;
    Health _hp;
    SpriteRenderer[] _srs;

    void Awake() {
        _hp = GetComponent<Health>();
        _srs = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public bool Apply(int dmg) {
        if (_invuln || dmg <= 0) return false;
        bool died = _hp.TakeDamage(dmg);
        if (!died) StartCoroutine(IFrames());
        return died;
    }

    IEnumerator IFrames() {
        _invuln = true;
        float t = 0f;
        bool vis = true;
        while (t < invulnDuration)
        {
            vis = !vis;
            foreach (var sr in _srs) if (sr) sr.enabled = vis;
            yield return new WaitForSeconds(blinkInterval);
            t += blinkInterval;
        }
        foreach (var sr in _srs) if (sr) sr.enabled = true;
        _invuln = false;
    }
}
