using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace WaypointTeleporter
{
    class CommandWaypointLocate : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "WaypointLocate";
        public string Help => "Tells you the Coordinates of your Waypoint/Marker";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "WLocate" };
        public List<string> Permissions => new List<string>() { "waypointlocate" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            if (uCaller.Player.quests.isMarkerPlaced)
            {
                Vector3 markerLocation = GetSurface(uCaller.Player.quests.markerPosition).Value;
                Logger.Log($"{markerLocation}", ConsoleColor.Cyan);
                UnturnedChat.Say(uCaller, "Marker Location (Also sent to Console):", Color.yellow);
                UnturnedChat.Say(uCaller, $"{markerLocation}", Color.yellow);
            }
            else
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
