using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class FXAutoDespawn : MonoBehaviour {
    ParticleSystem _ps;
    PoolLink _link;

    void Awake() {
        _ps = GetComponent<ParticleSystem>();
        _link = GetComponent<PoolLink>(); // nếu dùng ObjectPool của bạn
    }

    void OnEnable() {
        if (_ps) StartCoroutine(Co());
    }

    IEnumerator Co() {
        // Đợi particle chơi xong (kể cả sub-emitters)
        while (_ps != null && _ps.IsAlive(true))
            yield return null;

        if (_link != null && _link.pool != null) _link.ReturnToPool();
        else Destroy(gameObject);
    }
}
