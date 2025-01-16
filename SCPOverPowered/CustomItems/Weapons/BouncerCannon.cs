using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;
using CollisionHandler = Exiled.API.Features.Components.CollisionHandler;

namespace SCPOverPowered.CustomItems.Weapons;

[CustomItem(ItemType.GunShotgun)]
public class BouncerCannon : CustomWeapon
{
    
    public override string Name { get; set; } = "Bouncer Cannon";
    public override string Description { get; set; } = "This gun is so powerful that it can destroy any object. Shoots SCP-018";
    public override uint Id { get; set; } = 1001;

    public override float Weight { get; set; } = 1f;
    public override SpawnProperties? SpawnProperties { get; set; }

    public override float Damage { get; set; }
    public override byte ClipSize { get; set; } = 1;
    protected override void OnReloading(ReloadingWeaponEventArgs ev)
    {
       
        ev.IsAllowed = false;

        if (ev.Player.CurrentItem is not Firearm firearm || firearm.MagazineAmmo >= ClipSize)
            return;
        foreach (var item in ev.Player.Items.ToList())
        {
            Log.Debug($"{Name}.{nameof(OnReloading)}: Found item: {item.Type} - {item.Serial}");
            if (item.Type != ItemType.SCP018)
                continue;

            ev.Player.DisableEffect(EffectType.Invisible);

            Timing.CallDelayed(3f, () => firearm.MagazineAmmo = ClipSize);
                
            ev.Player.RemoveItem(item);

            return;
        }
    }

    protected override void OnShooting(ShootingEventArgs ev)
    {
        ev.IsAllowed = false;
        
        if (ev.Player.CurrentItem is Firearm firearm)
            firearm.MagazineAmmo -= 1;
        // ReSharper disable once SuggestVarOrType_SimpleTypes
        Vector3 pos = ev.Player.CameraTransform.TransformPoint(new Vector3(0.0715f, 0.0225f, 0.45f));

        var projectile = ev.Player.ThrowGrenade(ProjectileType.Scp018).Projectile;
        projectile.GameObject.AddComponent<CollisionHandler>().Init(ev.Player.GameObject, projectile.Base);

        base.OnShooting(ev);
    }
}