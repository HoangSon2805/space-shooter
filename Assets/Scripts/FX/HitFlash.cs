using UnityEngine;
using System.Collections;

public class HitFlash : MonoBehaviour {
    public Color hitColor = Color.white;
    public float dur = 0.08f;

    SpriteRenderer[] _srs;
    Color[] _orig;

    void Awake() {
        _srs = GetComponentsInChildren<SpriteRenderer>(true);
        _orig = new Color[_srs.Length];
        for (int i = 0; i < _srs.Length; i++) _orig[i] = _srs[i].color;
    }

    public void Trigger() {
        StopAllCoroutines();
        StartCoroutine(Co());
    }

    IEnumerator Co() {
        for (int i = 0; i < _srs.Length; i++) if (_srs[i]) _srs[i].color = hitColor;
        yield return new WaitForSeconds(dur);
        for (int i = 0; i < _srs.Length; i++) if (_srs[i]) _srs[i].color = _orig[i];
    }
}
