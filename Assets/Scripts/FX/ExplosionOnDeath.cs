using UnityEngine;

[RequireComponent(typeof(Health))]
public class ExplosionOnDeath : MonoBehaviour {
    [Header("FX")]
    public GameObject explosionPrefab;  // dùng khi không có pool
    public ObjectPool pool;             // gán nếu dùng pool (có thể để trống -> tự Find)
    public string poolId = "fx_explosion";

    [Header("Options")]
    public Vector2 offset = Vector2.zero;      // lệch vị trí nổ so với tâm
    public bool randomRotation = true;         // xoay ngẫu nhiên FX
    public Vector2 scaleRange = new Vector2(1f, 1f); // random scale
    public bool onlyIfVisible = false;         // chỉ nổ khi thấy trong camera?

    [Header("Feedback (optional)")]
    public bool playSfx = true;
    public bool cameraShake = true;
    public float shakeAmp = 0.07f;
    public float shakeDur = 0.10f;
    public bool hitStop = false;
    public float hitStopTime = 0.04f;

    Health _hp;
    Renderer[] _renderers;

    void Awake() {
        _hp = GetComponent<Health>();
        _renderers = GetComponentsInChildren<Renderer>(true);
        if (!pool) pool = FindObjectOfType<ObjectPool>(); // fallback
    }

    void OnEnable() {
        if (_hp != null) _hp.OnDead += Spawn;
    }

    void OnDisable() {
        if (_hp != null) _hp.OnDead -= Spawn; // tránh đăng ký trùng khi reuse từ pool
    }

    void Spawn() {
        if (onlyIfVisible && !IsVisibleByAnyRenderer()) return;

        // Tính transform FX
        Vector3 pos = transform.position + (Vector3)offset;
        Quaternion rot = randomRotation ? Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) : Quaternion.identity;
        float scl = Random.Range(scaleRange.x, scaleRange.y);

        // Spawn từ pool hoặc Instantiate
        GameObject fx = null;
        if (pool != null) fx = pool.Spawn(poolId, pos, rot);
        else if (explosionPrefab != null) fx = Instantiate(explosionPrefab, pos, rot);

        if (fx != null)
        {
            fx.transform.localScale = Vector3.one * scl;
            // Nếu FX không “tự huỷ”, gắn FXAutoDespawn vào prefab (mục 2)
        }

        // Feedback gọn
        if (cameraShake) CameraShake2D.Inst?.Shake(shakeAmp, shakeDur);
        if (playSfx && AudioManager.I != null) AudioManager.I.SfxEnemyDie();
        
    }

    bool IsVisibleByAnyRenderer() {
        if (_renderers == null || _renderers.Length == 0) return true;
        for (int i = 0; i < _renderers.Length; i++)
            if (_renderers[i] && _renderers[i].isVisible) return true;
        return false;
    }
}
