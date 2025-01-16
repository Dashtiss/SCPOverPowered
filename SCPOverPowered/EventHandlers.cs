namespace SCPOverPowered;

public class EventHandlers
{
    public void OnReloadingConfigs()
    {
        ScpOp.Instance.Config.LoadItems();
    }
}