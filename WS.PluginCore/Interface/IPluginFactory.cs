using WS.PluginCore.Plugin;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WS.PluginCore.Interface
{
    public interface IPluginFactory
    {
        List<Assembly> LoadedAssembly { get; }

        IPlugin GetPlugin(string pluginId);
        PluginItem GetPluginInfo(string pluginId, bool secret = false);
        List<PluginItem> GetPluginList(bool secret = false);
        void Load(string pluginPath);


        Task<bool> Init(PluginCoreContext context);

        Task<bool> Start(PluginCoreContext context);

        Task<bool> Stop(PluginCoreContext context);
    }
}
