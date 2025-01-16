using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;
using YamlDotNet.Serialization;

namespace SCPOverPowered;

public class OpConfig : IConfig
{
    [YamlIgnore]
    public Configs.Items ItemConfigs { get; private set; } = null!;
    
    
    [Description("If the plugin is enabled or is in debug")]
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    
    [Description("Folder where config and other files are")]
    public string ItemConfigFolder { get; set; } = Path.Combine(Paths.Configs, "SCPOverPowered");
    
    [Description("Config File")]
    public string ItemConfigFile { get; set; } = "global.yml";
    
    public void LoadItems()
    {
        if (!Directory.Exists(ItemConfigFolder))
            Directory.CreateDirectory(ItemConfigFolder);

        string filePath = Path.Combine(ItemConfigFolder, ItemConfigFile);
        Log.Debug($"{filePath}");
        if (!File.Exists(filePath))
        {
            ItemConfigs = new Configs.Items();
            File.WriteAllText(filePath, Loader.Serializer.Serialize(ItemConfigs));
        }
        else
        {
            ItemConfigs = Loader.Deserializer.Deserialize<Configs.Items>(File.ReadAllText(filePath));
            File.WriteAllText(filePath, Loader.Serializer.Serialize(ItemConfigs));
        }
    }
}