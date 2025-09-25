using UnityEngine;

public class EnemyShooter : MonoBehaviour {
    public Transform firePoint;          // đặt child ở giữa/miệng súng
    public GameObject bulletPrefab;      // BulletEnemy
    public float interval = 1.2f;        // nhịp bắn
    public float lead = 0f;              // dự đoán đơn giản (0 = tắt)

    public ObjectPool pool;              // dùng pool nếu có
    public string bulletPoolId = "enemy_bullet";

    Transform _player;
    float _t;

    void Start() {
        var go = GameObject.FindGameObjectWithTag("Player");
        _player = go ? go.transform : null;
    }

    void Update() {
        if (!_player || !firePoint) return;

        _t += Time.deltaTime;
        if (_t < interval) return;
        _t = 0f;

        // Hướng tới player (có thể cộng chút lead nếu muốn)
        Vector3 dir = (_player.position - firePoint.position);
        if (lead > 0f)
        {
            dir += (Vector3)_player.GetComponent<Rigidbody2D>()?.velocity * lead;
        }
        if (dir.sqrMagnitude < 0.0001f) return;
        var rot = Quaternion.FromToRotation(Vector3.up, dir.normalized);

        GameObject b = null;
        if (pool) b = pool.Spawn(bulletPoolId, firePoint.position, rot);
        else if (bulletPrefab) b = Instantiate(bulletPrefab, firePoint.position, rot);

        // đảm bảo đạn nhắm Player
        var bb = b ? b.GetComponent<Bullet>() : null;
        if (bb) bb.targetTag = "Player";
    }
}
