using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perk/Bullet Damage +1")]
public class Perk_BulletDamage : PerkSO {
    public override void Apply(PlayerUpgrades u) { u.bulletDamage += 1; }
}