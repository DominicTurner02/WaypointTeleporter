using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace WaypointTeleporter
{
    class CommandWaypointTP: IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "WaypointTP";
        public string Help => "Teleports you to your Marker";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "WTP" };
        public List<string> Permissions => new List<string>() { "waypointteleport" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            if (uCaller.Player.quests.isMarkerPlaced)
            {
                Vector3 teleportLocation = GetSurface(uCaller.Player.quests.markerPosition).Value;
                uCaller.Teleport(new Vector3(teleportLocation.x, teleportLocation.y + 3, teleportLocation.z), uCaller.Player.look.angle);
                UnturnedChat.Say(uCaller, "Successfully teleported to your Marker.", Color.yellow);
                Logger.LogWarning($"{uCaller.DisplayName} has teleported to their Marker {teleportLocation}.");
                if (WaypointTeleporter.Instance.Configuration.Instance.RemoveMarkerOnTP)
                {
                    uCaller.Player.quests.askSetMarker(uCaller.CSteamID, false, teleportLocation);
                }
                
            } else
            {
                UnturnedChat.Say(uCaller, "You need to set a Marker before using this command!", Color.red);
                return;
            }
        }

        private Vector3? GetSurface(Vector3 Position)
        {
            int layerMasks = (RayMasks.BARRICADE | RayMasks.BARRICADE_INTERACT | RayMasks.ENEMY | RayMasks.ENTITY | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.GROUND2 | RayMasks.ITEM | RayMasks.RESOURCE | RayMasks.STRUCTURE | RayMasks.STRUCTURE_INTERACT | RayMasks.WATER);
            if (Physics.Raycast(new Vector3(Position.x, Position.y + 200, Position.z), Vector3.down, out RaycastHit Hit, 250, layerMasks))
            {
                return Hit.point;
            }
            else
            {
                return null;
            }
        }
    }
}
