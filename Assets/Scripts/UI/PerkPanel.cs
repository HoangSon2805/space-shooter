using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PerkPanel : MonoBehaviour {
    [System.Serializable]
    public class Option {
        public Button button;
        public TextMeshProUGUI title;
        public TextMeshProUGUI desc;
        public Image icon;
    }

    public GameObject root;                 // <- trỏ tới Panel (có thể Disable)
    public Option[] options = new Option[3];
    public PerkSO[] perkPool;

    PlayerExperience _xp;
    PlayerUpgrades _upgrades;
    readonly List<PerkSO> _candidates = new();

    void Awake() {
        // Panel có thể đang Disable; script này PHẢI đang trên 1 GO active
        if (root != null) root.SetActive(false);
        _xp = FindObjectOfType<PlayerExperience>();
        _upgrades = FindObjectOfType<PlayerUpgrades>();
        if (_xp != null) _xp.OnLevelUp += OnLevelUp;
    }

    void OnDestroy() {
        if (_xp != null) _xp.OnLevelUp -= OnLevelUp;
    }

    void OnLevelUp(int level) {
        ShowRandomOptions();
    }

    void ShowRandomOptions() {
        if (_upgrades == null || perkPool == null || perkPool.Length == 0) return;

        _candidates.Clear();
        _candidates.AddRange(perkPool);

        for (int i = 0; i < options.Length; i++)
        {
            var perk = TakeRandom();
            Bind(options[i], perk);
        }

        Time.timeScale = 0f;
        if (root != null) root.SetActive(true);
    }

    void Bind(Option opt, PerkSO perk) {
        if (opt == null || opt.button == null) return;

        if (opt.title != null) opt.title.text = (perk != null) ? perk.title : "-";
        if (opt.desc != null) opt.desc.text = (perk != null) ? perk.description : "";
        if (opt.icon != null) opt.icon.sprite = (perk != null) ? perk.icon : null;

        opt.button.onClick.RemoveAllListeners();
        if (perk != null) opt.button.onClick.AddListener(() => Select(perk));
        else opt.button.onClick.AddListener(Hide);
    }

    void Select(PerkSO perk) {
        if (perk != null && _upgrades != null) perk.Apply(_upgrades);
        Hide();
    }

    void Hide() {
        if (root != null) root.SetActive(false);
        Time.timeScale = 1f;
    }

    PerkSO TakeRandom() {
        if (_candidates.Count == 0) return null;
        int idx = Random.Range(0, _candidates.Count);
        var p = _candidates[idx];
        _candidates.RemoveAt(idx);
        return p;
    }

#if UNITY_EDITOR
    void Update() {
        // Hotkey test trong Editor: nhấn P để mở panel (không cần lên level)
        if (Input.GetKeyDown(KeyCode.P)) ShowRandomOptions();
    }
#endif
}
