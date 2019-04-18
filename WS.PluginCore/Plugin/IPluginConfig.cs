using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WS.PluginCore.Plugin
{
    public interface IPluginConfig<TConfig>
    {
        Type ConfigType { get; }

        Task<TConfig> GetConfig(PluginCoreContext context);

        Task<bool> SaveConfig(TConfig cfg);

        TConfig GetDefaultConfig(PluginCoreContext context);

        Task<PluginMessage> ConfigChanged(PluginCoreContext context, TConfig newConfig);


    }
}
