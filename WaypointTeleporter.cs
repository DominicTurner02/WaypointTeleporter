using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;

namespace WaypointTeleporter
{
    public class WaypointTeleporter : RocketPlugin<ConfigurationWaypointTeleporter>
    {
        public static WaypointTeleporter Instance { get; private set; }

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.LogWarning("\n Loading WaypointTeleporter, made by Mr.Kwabs...");
            Logger.LogWarning("\n Successfully loaded WaypointTeleporter, made by Mr.Kwabs!");
        }

        protected override void Unload()
        {
            Instance = null;
            base.Unload();
        }
    }
}

