using Exiled.API.Interfaces;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using System.ComponentModel;

namespace CustomDoorAccess
{
    public class Configs : IConfig
    {
        [Description("Enable or disable CustomDoorAccess.")]
        public bool IsEnabled { get; set; } = false;

        [Description("Gives access to the door with the item(s) that you set.")]
        public Dictionary<string, string> AccessSet { get; set; } =
            new Dictionary<string, string> {{ "049_GATE", KeycardPermissions.ContainmentLevelTwo.ToString()}};

        [Description("Debug")]
        public bool Debug { get; set; } = false;
    }
}