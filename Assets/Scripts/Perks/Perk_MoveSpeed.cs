using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perk/Move Speed +20%")]
public class Perk_MoveSpeed : PerkSO {
    public float factor = 1.2f;
    public override void Apply(PlayerUpgrades u) { u.moveSpeedMult *= factor; }
}