using UnityEngine;

public class PlayerController2D : MonoBehaviour {
    public float moveSpeed = 8f;
    public Vector2 minBounds = new(-3f, -4.5f);
    public Vector2 maxBounds = new(3f, 4.5f);

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.15f;
    public ObjectPool pool;           // dùng pool
    public string bulletPoolId = "player_bullet";
    float _cooldown;

    void Update() {
        // Di chuyển (WASD/Arrow)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(h, v, 0f).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;
        ClampToBounds();

        // Bắn đạn (Space/Mouse0)
        _cooldown -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && _cooldown <= 0f)
        {
            _cooldown = fireRate;
            if (bulletPrefab && firePoint)
            {
                var go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                var b = go.GetComponent<Bullet>();
                if (b != null) b.targetTag = "Enemy";
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Thiếu bulletPrefab hoặc firePoint.");
#endif
        }
    }

    void ClampToBounds() {
        var p = transform.position;
        p.x = Mathf.Clamp(p.x, minBounds.x, maxBounds.x);
        p.y = Mathf.Clamp(p.y, minBounds.y, maxBounds.y);
        transform.position = p;
    }
}
