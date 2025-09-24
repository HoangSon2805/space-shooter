using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [System.Serializable]
    public class Item {
        public string id;
        public GameObject prefab;
        public int initialSize = 16;
    }

    public List<Item> items = new();

    readonly Dictionary<string, Queue<GameObject>> _queues = new();

    void Awake() {
        // Khởi tạo sẵn các hàng đợi theo cấu hình
        foreach (var it in items)
        {
            var q = new Queue<GameObject>(it.initialSize);
            for (int i = 0; i < it.initialSize; i++)
                q.Enqueue(Create(it.id, it.prefab));
            _queues[it.id] = q;
        }
    }

    GameObject Create(string id, GameObject prefab) {
        var go = Instantiate(prefab, transform);
        go.SetActive(false);
        var link = go.GetComponent<PoolLink>();
        if (!link) link = go.AddComponent<PoolLink>();
        link.pool = this;
        link.id = id;
        return go;
    }

    public GameObject Spawn(string id, Vector3 pos, Quaternion rot) {
        if (!_queues.TryGetValue(id, out var q))
        {
            // Chưa cấu hình trước: tạo queue động
            q = new Queue<GameObject>();
            var prefab = items.Find(x => x.id == id)?.prefab;
            if (!prefab) return null;
            _queues[id] = q;
        }

        GameObject go = (q.Count > 0) ? q.Dequeue() : Create(id, items.Find(x => x.id == id).prefab);
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public void Despawn(GameObject go) {
        var link = go.GetComponent<PoolLink>();
        if (link == null || !_queues.ContainsKey(link.id))
        {
            go.SetActive(false);
            Destroy(go); // trường hợp ngoài pool
            return;
        }
        go.SetActive(false);
        go.transform.SetParent(transform);
        _queues[link.id].Enqueue(go);
    }
}
