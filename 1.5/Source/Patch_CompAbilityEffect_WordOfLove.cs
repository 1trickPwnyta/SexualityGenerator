using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(CompAbilityEffect_WordOfLove))]
    [HarmonyPatch(nameof(CompAbilityEffect_WordOfLove.ValidateTarget))]
    public static class Patch_CompAbilityEffect_WordOfLove
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
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_IsAttractedToAllGenders);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
