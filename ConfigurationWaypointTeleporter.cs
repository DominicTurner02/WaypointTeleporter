using Rocket.API;

namespace WaypointTeleporter
{
    public class ConfigurationWaypointTeleporter : IRocketPluginConfiguration
    {
        public bool RemoveMarkerOnTP;

        public void LoadDefaults()
        {
            RemoveMarkerOnTP = false;
        }

    }
}
