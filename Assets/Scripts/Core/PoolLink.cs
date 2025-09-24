using UnityEngine;

public class PoolLink : MonoBehaviour {
    public string id;
    public ObjectPool pool;

    public void ReturnToPool() {
        if (pool) pool.Despawn(gameObject);
        else gameObject.SetActive(false);
    }
}
