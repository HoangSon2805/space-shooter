using UnityEngine;

public class EnemyBasic : MonoBehaviour {
    public float speed = 2.5f;
    public float minY = -6f;

    void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < minY) gameObject.SetActive(false);
    }
}
