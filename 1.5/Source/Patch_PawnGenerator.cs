using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch(nameof(PawnGenerator.TryGenerateSexualityTraitFor))]
    public static class Patch_PawnGenerator
    {
        public static bool Prefix(Pawn pawn, bool allowGay)
        {
            if (!pawn.HasSexualityTrait())
            {
                TraitDef def = DefDatabase<TraitDef>.AllDefsListForReading.Where(d => d.SexualityCommonality() > 0f && (allowGay || d != TraitDefOf.Gay)).RandomElementByWeightWithFallback(d => d.SexualityCommonality(), null);
                if (def != null)
                {
                    pawn.story.traits.GainTrait(new Trait(def));
                }
            }

            return false;
        }
    }
}
