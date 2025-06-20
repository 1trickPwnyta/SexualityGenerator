using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker))]
    [HarmonyPatch(nameof(Pawn_RelationsTracker.SecondaryLovinChanceFactor))]
    public static class Patch_Pawn_RelationsTracker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundBisexual = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == SexualityGeneratorRefs.f_TraitDefOf_Bisexual)
                {
                    foundBisexual = true;
                }

                if (foundBisexual && !finished && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == SexualityGeneratorRefs.m_TraitSet_HasTrait)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, SexualityGeneratorRefs.f_Pawn_RelationsTracker_pawn);
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_IsAttractedToAllGenders);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        public static void Postfix(Pawn ___pawn, Pawn otherPawn, ref float __result)
        {
            __result *= ___pawn.GetAttractionFactor(otherPawn.gender);
        }
    }
}
