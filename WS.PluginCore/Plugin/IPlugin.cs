using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WS.PluginCore.Plugin
{
    public interface IPlugin
    {
        string PluginID { get; }

        string PluginName { get; }

        string Description { get; }

        int Order { get; }


        Task<PluginMessage> Init(PluginCoreContext context);

        Task<PluginMessage> Start(PluginCoreContext context);

        Task<PluginMessage> Stop(PluginCoreContext context);

        Task OnMainConfigChanged(PluginCoreContext context, PluginCoreConfig newConfig);
    }
}
