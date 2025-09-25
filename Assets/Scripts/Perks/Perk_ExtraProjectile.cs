using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perk/Extra Projectile")]
public class Perk_ExtraProjectile : PerkSO {
    public override void Apply(PlayerUpgrades u) { u.extraProjectiles += 1; }
}