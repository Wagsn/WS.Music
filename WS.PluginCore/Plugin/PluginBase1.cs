using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WS.PluginCore.Plugin
{
    public abstract class PluginBase<TConfig> : PluginBase, IPluginConfig<TConfig>
        where TConfig : class
    {

        public virtual Task<PluginMessage> ConfigChanged(PluginCoreContext context, TConfig newConfig)
        {
            return Task.FromResult(new PluginMessage());
        }

        public abstract TConfig GetDefaultConfig(PluginCoreContext context);

        public Type ConfigType
        {
            get
            {
                return typeof(TConfig);
            }
        }

        public TConfig GetConfig()
        {
            TConfig cfg = PluginCoreContext.Current.PluginConfigStorage.GetConfig<TConfig>(this.PluginID).Result.Extension;
            if (cfg == null)
            {
                cfg = GetDefaultConfig(PluginCoreContext.Current);
            }

            return cfg;
        }

        public async Task<bool> SaveConfig(TConfig cfg)
        {
            var r = await PluginCoreContext.Current.PluginConfigStorage.SaveConfig<TConfig>(this.PluginID, cfg);
            return r.Code == "0";
        }

        public async Task<bool> DeleteConfig()
        {
            var r = await PluginCoreContext.Current.PluginConfigStorage.DeleteConfig(this.PluginID);

            return r.Code == "0";

        }

        public async Task<TConfig> GetConfig(PluginCoreContext context)
        {
            var r = await PluginCoreContext.Current.PluginConfigStorage.GetConfig<TConfig>(this.PluginID);
            TConfig c = null;
            if (r.Code == "0")
            {
                c = r.Extension;
            }
            if (c == null)
            {
                c = GetDefaultConfig(context);
            }
            return c;

        }
    }
}
