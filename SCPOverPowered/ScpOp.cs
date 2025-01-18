using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Server = Exiled.Events.Handlers.Server;
namespace SCPOverPowered;

public class ScpOp : Plugin<OpConfig>
{
    public override string Author => "Dashtiss";
    public override string Name => "Scp Over Powered";
    public override string Prefix => "ScpOP";
    public override Version RequiredExiledVersion => new(9, 3, 0);
    public override Version Version => new(1, 0, 0);

    public static ScpOp Instance { get; private set; } = null!;

    private EventHandlers Handlers { get; set; } = null!;

    public Player Scp035 { get; set; } = null!;
    
    public override void OnEnabled()
    {
        Instance = this;
        
        Handlers = new EventHandlers();
        
        Config.LoadItems();
        
        CustomItem.RegisterItems(overrideClass: Config.ItemConfigs);
        
        Server.ReloadedConfigs += EventHandlers.OnReloadingConfigs;
        
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        CustomItem.UnregisterItems();
        
        Instance = null!;
        Server.ReloadedConfigs -= EventHandlers.OnReloadingConfigs;
        
        base.OnDisabled();
    }
}