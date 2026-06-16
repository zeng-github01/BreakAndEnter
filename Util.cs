using SDG.Unturned;
using UnityEngine;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public static class Util
    {
        public static string Translate(string TranslationKey, params object[] Placeholders) =>
            BreakAndEnter.instance.Translations.Instance.Translate(TranslationKey, Placeholders);

        public static void ToggleDoor(InteractableDoor door, bool open)
        {
            BarricadeManager.ServerSetDoorOpen(door, open);
        }

        public static T SmartFinder<T>(Transform transform) where T : Component
        {
            T comp = transform.GetComponent<T>();
            if (comp != null)
                return comp;

            comp = transform.GetComponentInParent<T>();
            if (comp != null)
                return comp;

            return null;
        }
    }
}
