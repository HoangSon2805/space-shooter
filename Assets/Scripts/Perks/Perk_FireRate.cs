using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perk/Fire Rate +15%")]
public class Perk_FireRate : PerkSO {
    public float factor = 0.85f;
    public override void Apply(PlayerUpgrades u) { u.fireRateMult *= factor; }
}
