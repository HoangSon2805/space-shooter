using UnityEngine;

[RequireComponent(typeof(PlayerUpgrades))]
[RequireComponent(typeof(PlayerExperience))]
[RequireComponent(typeof(Health))]
public class PlayerController2D : MonoBehaviour {
    public float moveSpeed = 8f;
    public Vector2 minBounds = new(-3f, -4.5f);
    public Vector2 maxBounds = new(3f, 4.5f);

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.15f;

    public ObjectPool pool;
    public string bulletPoolId = "player_bullet";

    PlayerUpgrades _up;
    float _cd;

    void Awake() {
        _up = GetComponent<PlayerUpgrades>();
        if (!pool) pool = FindObjectOfType<ObjectPool>();
    }

    void Update() {
        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var move = new Vector3(h, v, 0f).normalized;
        float spd = moveSpeed * (_up ? _up.moveSpeedMult : 1f);
        transform.position += move * spd * Time.deltaTime;
        Clamp();

        // Shoot
        float rate = fireRate * (_up ? _up.fireRateMult : 1f);
        _cd -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && _cd <= 0f)
        {
            _cd = Mathf.Max(0.02f, rate);
            Fire();
        }
    }

    void Fire() {
        int extra = _up ? _up.extraProjectiles : 0;
        int count = 1 + Mathf.Max(0, extra);
        if (AudioManager.I != null) AudioManager.I.SfxShoot();
        if (count == 1)
        {
            SpawnBullet(firePoint.position, firePoint.rotation);
            
            return;
        }

        float spread = _up ? _up.spreadAngle : 15f;
        float step = (count > 1) ? spread / (count - 1) : 0f;
        float start = -spread * 0.5f;

        for (int i = 0; i < count; i++)
        {
            float ang = start + step * i;
            var rot = firePoint.rotation * Quaternion.Euler(0, 0, ang);
            SpawnBullet(firePoint.position, rot);
        }
        
    }

    void SpawnBullet(Vector3 pos, Quaternion rot) {
        GameObject go = null;
        if (pool) go = pool.Spawn(bulletPoolId, pos, rot);
        else if (bulletPrefab) go = Instantiate(bulletPrefab, pos, rot);

        var b = go ? go.GetComponent<Bullet>() : null;
        if (b)
        {
            b.targetTag = "Enemy";
            if (_up) b.damage = _up.bulletDamage;
        }
    }

    void Clamp() {
        var p = transform.position;
        p.x = Mathf.Clamp(p.x, minBounds.x, maxBounds.x);
        p.y = Mathf.Clamp(p.y, minBounds.y, maxBounds.y);
        transform.position = p;
    }
}
