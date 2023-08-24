using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Framework.Utilities;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public class CommandDestroy : IRocketCommand
    {
        #region Properties
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "destroy";

        public string Help => "Destroys the barricade or structure that you are looking at.";

        public string Syntax => "/destroy";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "breakandenter.destroy" };
        #endregion

        public void Execute(IRocketPlayer caller, string[] args)
        {
            Player player = ((UnturnedPlayer)caller).Player;
            PlayerLook look = player.look;

            if (Physics.Raycast(new Ray(look.aim.position, look.aim.forward), out RaycastHit hit, Mathf.Infinity, RayMasks.BARRICADE_INTERACT | RayMasks.STRUCTURE))
            {
                Interactable2SalvageBarricade barri = hit.transform.GetComponent<Interactable2SalvageBarricade>();
                Interactable2SalvageStructure struc = hit.transform.GetComponent<Interactable2SalvageStructure>();

                if (barri != null)
                {

                    var drop = BarricadeManager.FindBarricadeByRootTransform(barri.root);
                    BarricadeManager.tryGetRegion(barri.root, out byte x, out byte y, out ushort plant, out _);

                    BarricadeManager.destroyBarricade(drop, x, y, plant);

                    UnturnedChat.Say(caller, Util.Translate("barricade_removed"));
                }
                else if (struc != null)
                {
                   var drop = StructureManager.FindStructureByRootTransform(struc.transform);

                    StructureManager.tryGetRegion(struc.transform, out byte x, out byte y,out _);

                    StructureManager.destroyStructure(drop,x,y,(drop.model.position - player.transform.position).normalized * 100f);

                    UnturnedChat.Say(caller, Util.Translate("structure_removed"));
                }
                else
                {
                    UnturnedChat.Say(caller, Util.Translate("invalid_destroy"));
                }
            }
            else
            {
                UnturnedChat.Say(caller, Util.Translate("no_object"));
            }
        }
    }
}