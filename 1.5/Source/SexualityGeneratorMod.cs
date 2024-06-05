using HarmonyLib;
using Verse;

namespace SexualityGenerator
{
    public class SexualityGeneratorMod : Mod
    {
        public const string PACKAGE_ID = "sexualitygenerator.1trickPwnyta";
        public const string PACKAGE_NAME = "Sexuality Generator";

        public SexualityGeneratorMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
