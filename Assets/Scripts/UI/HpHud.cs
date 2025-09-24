using UnityEngine;
using TMPro;

public class HpHud : MonoBehaviour {
    public Health target;            // tự tìm theo tag Player
    public TextMeshProUGUI text;

    void Start() {
        if (!target)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go) target = go.GetComponent<Health>();
        }
        if (target)
        {
            target.OnChanged += OnChanged;
            OnChanged(target.Current, target.maxHP);
        }
    }

    void OnDestroy() {
        if (target) target.OnChanged -= OnChanged;
    }

    void OnChanged(int cur, int max) {
        if (text) text.text = $"HP: {cur}/{max}";
    }
}
