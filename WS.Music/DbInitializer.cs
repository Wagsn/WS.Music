using System;

namespace WS.Music
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {
            WS.Music.Stores.MusicDbInitializer.Initialize(services);
            MessageServer.Stores.MessageDbInitializer.Initialize(services);
        }
    }
}
