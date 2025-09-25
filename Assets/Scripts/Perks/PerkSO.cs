using UnityEngine;

public abstract class PerkSO : ScriptableObject {
    public string title;
    [TextArea] public string description;
    public Sprite icon;

    public abstract void Apply(PlayerUpgrades u);
}
