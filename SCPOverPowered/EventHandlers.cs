using Exiled.Events.EventArgs.Player;

namespace SCPOverPowered;

public class EventHandlers
{
    public static void OnReloadingConfigs()
    {
        ScpOp.Instance.Config.LoadItems();
    }

    public static void OnPlayerHurt(HurtingEventArgs ev)
    {
        if (ev.Player == null) return;
        
        if (ev.Player != ScpOp.Instance.Scp035) return;
        
        if (ev.Attacker.IsScp) ev.IsAllowed = false;

        ev.Attacker.ShowHint("<color=red>You are not allowed to hurt SCP035!</color>", 2f);
    }
}