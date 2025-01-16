using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;


namespace SCPOverPowered.CustomItems.Items;

[CustomItem(ItemType.Adrenaline)]
public class SuperAdrenaline : CustomItem
{
    public override string Description { get; set; } = "Custom adrenaline that give you super speed for 30 seconds";
    public override string Name { get; set; } = "Super Adrenaline";
    public override uint Id { get; set; } = 2001;
    public override float Weight { get; set; } = 1f;
    
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints =
        [
            new DynamicSpawnPoint
            {
                Chance = 50,
                Location = SpawnLocationType.Inside049Armory,
            },
        ],
        LockerSpawnPoints = 
        [
            new LockerSpawnPoint
            {
                Zone = ZoneType.Unspecified,
                UseChamber = true,
                Chance = 50,
                Name = "Super Adrenaline",
                Position = default,
                Type = LockerType.Misc,
            }
        ],
    };

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
        base.SubscribeEvents();
    }
    
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
        base.UnsubscribeEvents();
    }

    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem)) return;

        ev.Player.EnableEffect<MovementBoost>(30f);
    }

}