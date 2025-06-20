using HarmonyLib;
using RimWorld;
using Verse;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(RelationsUtility))]
    [HarmonyPatch(nameof(RelationsUtility.AttractedToGender))]
    public static class Patch_RelationsUtility
    {
        public static bool Prefix(Pawn pawn, Gender gender, ref bool __result)
        {
            __result = pawn.IsAttractedTo(gender);

            return false;
        }
    }
}
