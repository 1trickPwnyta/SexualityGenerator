using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch(nameof(PawnGenerator.TryGenerateSexualityTraitFor))]
    public static class Patch_PawnGenerator_TryGenerateSexualityTraitFor
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

    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch(nameof(PawnGenerator.GenerateTraitsFor))]
    public static class Patch_PawnGenerator_GenerateTraitsFor
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundBisexual = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundBisexual && instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == SexualityGeneratorRefs.f_TraitDefOf_Bisexual)
                {
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_IsSexualityTraitDef);
                    foundBisexual = true;
                    continue;
                }

                if (foundBisexual && !finished && instruction.opcode == OpCodes.Beq)
                {
                    instruction.opcode = OpCodes.Brtrue;
                    finished = true;
                }

                yield return instruction;
            }
        }
    }
}
