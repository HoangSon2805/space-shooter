using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perk/+1 Max HP")]
public class Perk_MaxHP : PerkSO {
    public override void Apply(PlayerUpgrades u) {
        u.maxHPBonus += 1;
        u.ApplyMaxHP();
    }
}