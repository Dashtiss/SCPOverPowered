using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using PlayerRoles;
using UnityEngine;
using static SCPOverPowered.ScpOp;
using DamageType = Exiled.API.Enums.DamageType;
using Random = System.Random;

namespace SCPOverPowered.CustomItems.Items;


[CustomItem(ItemType.None)]
public class Scp035 : CustomItem
{
    public override uint Id { get; set; } = 2003;
    public override string Name { get; set; } = "SCP-035";

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The health of the SCP-035 when spawned
    /// </summary>
    public float Health { get; set; } = 2000f;
    
    public override string Description { get; set; } =
        "Pick up this item, you die and a random spectator gets revived as scp-035";

    public override float Weight { get; set; } = 1.0f;
    public override SpawnProperties? SpawnProperties { get; set; } = null;

    protected override void OnAcquired(Player player, Item item, bool displayMessage)
    {
        Vector3 pos = player.Position;
        var items = player.Items;
        RoleTypeId oldRole = player.Role;
        
        player.Kill(DamageType.Custom);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (Instance.Scp035 != null)
            return;
        
        var random = new Random();
        var spectators = Player.Get(RoleTypeId.Spectator).ToList();
        
        
        if (spectators.Count == 0)
            return;
        var index = random.Next(spectators.Count);
        var spectator = spectators[index];
        if (spectator == null)
            return;
        spectator.Role.Set(oldRole);
        spectator.Position = pos;
        spectator.Health = Health;
        spectator.MaxHealth = Health;
        
        foreach (var playerItem in items)
        {
            spectator.AddItem(playerItem);
        }
        Instance.Scp035 = spectator;
        
    }

    public override Pickup Spawn(Vector3 position, Player? owner = null)
    {
        Pickup pickup = Pickup.CreateAndSpawn(RandomType(), position, default);
        pickup.Weight = Weight;
        TrackedSerials.Add(pickup.Serial);
        return pickup;
    }

    private ItemType RandomType()
    {
        var random = new Random();
        var types = Enum.GetValues(typeof(ItemType));
        int index = random.Next(types.Length);
        return (ItemType)types.GetValue(index)!;
    }
}