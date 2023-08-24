﻿using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Framework.Utilities;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public class CommandDoor : IRocketCommand
    {
        #region Properties
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "door";

        public string Help => "Forces the door that you are looking at to open or close.";

        public string Syntax => "/door";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "breakandenter.door" };
        #endregion

        public void Execute(IRocketPlayer caller, string[] args)
        {
            PlayerLook look = ((UnturnedPlayer)caller).Player.look;

            if (Physics.Raycast(new Ray(look.aim.position, look.aim.forward), out RaycastHit hit, Mathf.Infinity, RayMasks.BARRICADE_INTERACT))
            {
                InteractableDoorHinge hinge = hit.transform.GetComponent<InteractableDoorHinge>();

                if (hinge != null)
                {
                    InteractableDoor door = hinge.door;
                    bool open = !door.isOpen;

                    Util.ToggleDoor(door, open);

                    UnturnedChat.Say(caller, Util.Translate("door_toggle", open ? "opened" : "closed"));

                    if (open && BreakAndEnter.instance.Configuration.Instance.AutoCloseDoors)
                        BreakAndEnter.instance.AutoCloseDoor(door);
                }
                else
                    UnturnedChat.Say(caller, Util.Translate("invalid_door"));
            }
            else
                UnturnedChat.Say(caller, Util.Translate("no_object"));
        }
    }
}