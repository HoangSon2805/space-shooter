using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 12f;
    public float maxLifetime = 3f;
    public int damage = 1;
    public string targetTag = "Enemy";

    float _t;
    PoolLink _link;

    void Awake() { _link = GetComponent<PoolLink>(); }
    void OnEnable() { _t = 0f; }

    void Update() {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        _t += Time.deltaTime;
        if (_t >= maxLifetime) Destroy(gameObject); // sau sẽ chuyển sang pool
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag(targetTag)) return;
        other.GetComponent<Health>()?.TakeDamage(damage); // áp sát thương nếu có
        Destroy(gameObject);
    }
    void Despawn() {
        if (_link && _link.pool) _link.ReturnToPool();
        else Destroy(gameObject);
    }
}
