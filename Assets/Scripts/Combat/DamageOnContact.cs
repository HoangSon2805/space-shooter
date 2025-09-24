using UnityEngine;

public class DamageOnContact : MonoBehaviour {
    public string targetTag = "Player";
    public int damage = 1;
    public bool selfDisableOnHit = false; // enemy tự tắt khi chạm

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag(targetTag)) return;

        var dr = other.GetComponent<DamageReceiver>();
        if (dr) dr.Apply(damage);
        else other.GetComponent<Health>()?.TakeDamage(damage);

        if (selfDisableOnHit) gameObject.SetActive(false);
    }
}