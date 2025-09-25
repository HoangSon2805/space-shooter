using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpHudBar : MonoBehaviour {
    public PlayerExperience target;           // để trống: tự tìm theo tag Player
    public Image fill;                         // Image (Filled)
    public TextMeshProUGUI xpText;            // "12/20"
    public TextMeshProUGUI levelText;         // "Lv 3"
    public float smooth = 10f;                 // mượt fill

    float _cur;

    void Start() {
        if (!target)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go) target = go.GetComponent<PlayerExperience>();
        }
        if (fill) _cur = fill.fillAmount;
        RefreshTexts(); // hiển thị ngay khi vào game
    }

    void Update() {
        if (!target || !fill) return;
        float max = Mathf.Max(1, target.xpToNext);
        float t = Mathf.Clamp01((float)target.xp / max);
        _cur = Mathf.MoveTowards(_cur, t, smooth * Time.unscaledDeltaTime);
        fill.fillAmount = _cur;
        RefreshTexts();
    }

    void RefreshTexts() {
        if (!target) return;
        if (levelText) levelText.text = $"Lv {target.level}";
        if (xpText) xpText.text = $"{target.xp}/{target.xpToNext}";
    }
}
