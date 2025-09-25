using UnityEngine;
using System.Collections;

public class CameraShake2D : MonoBehaviour {
    public static CameraShake2D Inst;
    Vector3 _orig;

    void Awake() { Inst = this; _orig = transform.localPosition; }

    public void Shake(float amp = 0.15f, float dur = 0.12f) {
        StopAllCoroutines();
        StartCoroutine(Co(amp, dur));
    }

    IEnumerator Co(float a, float t) {
        float e = 0f;
        while (e < t)
        {
            float k = 1f - (e / t);
            transform.localPosition = _orig + (Vector3)Random.insideUnitCircle * (a * k);
            e += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localPosition = _orig;
    }
}
