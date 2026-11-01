using System;
using System.Collections;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace EnemyBarOnScreen
{
    [BepInPlugin(Guid, ModName, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "EnemyBarOnScreen";
        public const string ModName = "EnemyBarOnScreen";
        public const string Version = "1.0.0";

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<float> TopMargin;

        void Awake()
        {
            Enabled = Config.Bind("General", "Enabled", true,
                "Turns the mod on or off.");
            TopMargin = Config.Bind("General", "TopMarginPercent", 10f,
                new ConfigDescription("How far below the top edge of the screen the health bar stops, in percent of the screen height. " +
                    "Raise it if the bar's name text is cut off or overlaps your hotbar.",
                    new AcceptableValueRange<float>(0f, 50f)));

            new Harmony(Guid).PatchAll(typeof(UpdateHudsPatch));
        }
    }

    // Vanilla EnemyHud.UpdateHuds puts each creature's bar at the screen projection of
    // Character.GetTopPoint, even when that point is above the screen. Up close to a tall creature
    // the bar ends up off screen. This pushes it back down to the top edge, so it sticks there
    // until the creature's head comes back into view.
    [HarmonyPatch(typeof(EnemyHud), "UpdateHuds")]
    static class UpdateHudsPatch
    {
        static readonly FieldInfo hudsField = AccessTools.Field(typeof(EnemyHud), "m_huds");
        static readonly Type hudDataType = AccessTools.Inner(typeof(EnemyHud), "HudData");
        static readonly FieldInfo characterField = AccessTools.Field(hudDataType, "m_character");
        static readonly FieldInfo guiField = AccessTools.Field(hudDataType, "m_gui");

        static void Postfix(EnemyHud __instance)
        {
            if (!Plugin.Enabled.Value) return;
            IDictionary huds = hudsField.GetValue(__instance) as IDictionary;
            if (huds == null) return;

            float top = Screen.height * (1f - Plugin.TopMargin.Value / 100f);
            foreach (object hud in huds.Values)
            {
                GameObject gui = guiField.GetValue(hud) as GameObject;
                if (gui == null || !gui.activeSelf) continue;
                // Bosses use the fixed bar at the top of the screen; other players' names are left alone.
                Character character = characterField.GetValue(hud) as Character;
                if (character == null || character.IsBoss() || character.IsPlayer()) continue;

                // Vanilla keeps the screen depth in z; z <= 0 means the point is behind the camera.
                Transform t = gui.transform;
                Vector3 pos = t.position;
                if (pos.z > 0f && pos.y > top)
                {
                    pos.y = top;
                    t.position = pos;
                }
            }
        }
    }
}
