using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp1344;
using InventorySystem.Items.Usables.Scp1344;

namespace SCPOverPowered.CustomItems.Items;

[CustomItem(ItemType.SCP1344)]
public class ScrambleGoggles : CustomItem
{
    public override uint Id { get; set; } = 2002;
    public override string Name { get; set; } = "Scramble Goggles";
    public override string Description { get; set; } = "Goggles that allow you to look at SCP-096 without enraging him";
    public override float Weight { get; set; } = 1.0f;

    private List<Player> ActivePlayers = [];
    
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 5,
        LockerSpawnPoints =
        [
            new LockerSpawnPoint
            {
                Zone = ZoneType.HeavyContainment,
                UseChamber = true,
                Chance = 10,
                Name = "Super Adrenaline",
                Position = default,
                Type = LockerType.Medkit,
            },
            new LockerSpawnPoint
            {
                Zone = ZoneType.HeavyContainment,
                UseChamber = true,
                Chance = 35,
                Name = "Super Adrenaline",
                Position = default,
                
                #pragma warning disable CS0618 // Type or member is obsolete
                
                Type = LockerType.Pedestal
                
                #pragma warning restore CS0618 // Type or member is obsolete
                
            },
            new LockerSpawnPoint
            {
                Zone = ZoneType.HeavyContainment,
                UseChamber = true,
                Chance = 10,
                Name = "Super Adrenaline",
                Position = default,
                Type = LockerType.Medkit,
            },
        ],
    };

    private void ChangingStatus(ChangingStatusEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem)) return;
        
        if (ev.Scp1344.Status == Scp1344Status.Activating)
        {
            ActivePlayers.Add(ev.Player);
            ev.IsAllowed = false;
        } 
        else if (ev.Scp1344.Status == Scp1344Status.Deactivating)
        {
            ActivePlayers.Remove(ev.Player);
            ev.IsAllowed = false;

        }
    }

    private void OnTargetAdded(AddingTargetEventArgs ev)
    {
        if (ev.Player == null) return;

        if (!ActivePlayers.Contains(ev.Player)) return;
        if (ev.Scp096.HasTarget(ev.Player))
        {
            ev.Scp096.RemoveTarget(ev.Player);
                
        }
        ev.IsAllowed = false;
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Scp096.AddingTarget -= OnTargetAdded;
        Exiled.Events.Handlers.Scp1344.ChangingStatus -= ChangingStatus; 
        base.UnsubscribeEvents();
    }
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Scp096.AddingTarget += OnTargetAdded;
        Exiled.Events.Handlers.Scp1344.ChangingStatus += ChangingStatus;
        base.SubscribeEvents();
    }
}