using HarmonyLib;
using UnityEngine;
using Verse;

namespace SexualityGenerator
{
    public class SexualityGeneratorMod : Mod
    {
        public const string PACKAGE_ID = "sexualitygenerator.1trickPwnyta";
        public const string PACKAGE_NAME = "Sexuality Generator";

        public static SexualityGeneratorSettings Settings;

        public SexualityGeneratorMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<SexualityGeneratorSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            SexualityGeneratorSettings.DoSettingsWindowContents(inRect);
        }
    }
}
