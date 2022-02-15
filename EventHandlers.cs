using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using KeycardPermissions = Interactables.Interobjects.DoorUtils.KeycardPermissions;

namespace CustomDoorAccess
{
    public class EventHandlers
    {
        private readonly CdaPlugin _plugin;
        public EventHandlers(CdaPlugin plugin) => _plugin = plugin;
        public void OnWaitingForPlayers()
        {
            Room.Get(RoomType.Hcz049).Doors.Single(x => x.Base is PryableDoor).Base.gameObject.AddComponent<DoorNametagExtension>().UpdateName("049_GATE");

            foreach (Door door in Door.List)
            {
                if (!door.Base.TryGetComponent(out DoorNametagExtension _)) continue;
                var doorName = door.Base.GetComponent<DoorNametagExtension>().GetName;
                if (_plugin.Config.AccessSet.TryGetValue(doorName, out string valueName))
                {
                    string trimmedValue = valueName.Trim();
                    string[] Permissions = trimmedValue.Split('&');
                    KeycardPermissions keycardPermissions = KeycardPermissions.None;
                    foreach (string Permission in Permissions)
                        if (Enum.TryParse(Permission, out KeycardPermissions permissions))
                            if (keycardPermissions == KeycardPermissions.None)
                                keycardPermissions = permissions;
                            else
                                keycardPermissions &= permissions;
                    door.Base.RequiredPermissions.RequiredPermissions = keycardPermissions;
                }
            }
        }
    }
}