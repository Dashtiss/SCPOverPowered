using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;
using YamlDotNet.Serialization;

namespace SCPOverPowered.CustomItems.Weapons;

[CustomItem(ItemType.GunE11SR)]
public class VoidReaper : CustomWeapon
{
    public override string Name { get; set; } = "VoidReaper";
    public override uint Id { get; set; } = 1002;
    [YamlIgnore]
    public override string Description { get; set; } =
        "A sniper rifle that deals massive damage but has a limited magazine. Shots pass through walls but reveal your location.";

    public override float Weight { get; set; } = 4.25f;

    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = [
        AttachmentName.ExtendedBarrel,
        AttachmentName.ScopeSight
    ];
    /// <inheritdoc/>
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints =
        [
            new DynamicSpawnPoint
            {
                Chance = 100,
                Location = SpawnLocationType.InsideHidUpper,
            },

            new DynamicSpawnPoint
            {
                Chance = 40,
                Location = SpawnLocationType.InsideHczArmory,
            }

        ],
    };
    
    /// <inheritdoc/>
    [YamlIgnore]
    public override float Damage { get; set; }
    
    [Description("The amount of extra damage this weapon does, as a multiplier.")]
    public float DamageMultiplier { get; set; } = 7.5f;

    public override byte ClipSize { get; set; } = 1;

    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
            ev.Amount *= DamageMultiplier;
    }
}