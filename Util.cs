using SDG.Unturned;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public static class Util
    {
        public static string Translate(string TranslationKey, params object[] Placeholders) =>
            BreakAndEnter.instance.Translations.Instance.Translate(TranslationKey, Placeholders);

        public static void ToggleDoor(InteractableDoor door, bool open)
        {
            BarricadeManager.tryGetInfo(door.transform, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion region);

            door.updateToggle(open);

            BarricadeManager.instance.channel.send("tellToggleDoor", ESteamCall.ALL,
                ESteamPacket.UPDATE_RELIABLE_BUFFER, x, y, plant, index, open);
        }
    }
}
