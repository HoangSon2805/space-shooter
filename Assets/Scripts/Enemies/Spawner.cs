using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;

    // Khoảng X sinh địch (tọa độ world)
    public Vector2 spawnX = new(-8.5f, 8.5f);

    // Cao hơn mép trên một chút để trôi xuống
    public float spawnY = 6.5f;

    // Nhịp sinh
    public float interval = 0.8f;

    // Tăng độ khó: giảm dần interval theo thời gian
    public float intervalMin = 0.25f;
    public float rampEvery = 15f;     // mỗi 15s
    public float rampFactor = 0.9f;   // giảm 10%

    float _t, _tRamp;

    void Start() {
        // Tự tính spawnX theo camera nếu để (0,0): tiện khi đổi aspect
        if (Mathf.Approximately(spawnX.x, 0f) && Mathf.Approximately(spawnX.y, 0f))
        {
            var cam = Camera.main;
            float halfH = cam.orthographicSize;
            float halfW = halfH * cam.aspect;
            float pad = 0.5f;
            spawnX = new Vector2(-halfW + pad, halfW - pad);
            spawnY = halfH + 1f;
        }
    }

    void Update() {
        _t += Time.deltaTime;
        _tRamp += Time.deltaTime;

        if (_t >= interval)
        {
            _t = 0f;
            float x = Random.Range(spawnX.x, spawnX.y);
            var pos = new Vector3(x, spawnY, 0f);
            var go = Instantiate(enemyPrefab, pos, Quaternion.identity);

            // Random nhẹ tốc độ nếu EnemyBasic có field speed
            var eb = go.GetComponent<EnemyBasic>();
            if (eb) eb.speed *= Random.Range(0.9f, 1.2f);
        }

        if (_tRamp >= rampEvery)
        {
            _tRamp = 0f;
            interval = Mathf.Max(intervalMin, interval * rampFactor);
        }
    }
}
