using System;
using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;
using Log = Exiled.API.Features.Log;

namespace CustomDoorAccess
{
    public class CdaPlugin : Plugin<Configs>
    {
        public EventHandlers _eventHandlers;
        public override string Author { get; } = "Faety";
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 26);
        public override string Prefix { get; } = "cda";
        public override string Name { get; } = "CustomDoorAccess";
        public override Version Version { get; } = new Version(1, 3, 0);

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
            {
                Log.Info("CustomDoorAccess is disabled via configs. It will not be loaded.");
                return;
            }
            _eventHandlers = new EventHandlers(this);
            Server.WaitingForPlayers += _eventHandlers.OnWaitingForPlayers;
            //Player.InteractingDoor += _eventHandlers.OnDoorInteract;
        }

        public override void OnDisabled()
        {
            Server.WaitingForPlayers -= _eventHandlers.OnWaitingForPlayers;
            //Player.InteractingDoor -= _eventHandlers.OnDoorInteract;
            _eventHandlers = null;
        }
    }
}