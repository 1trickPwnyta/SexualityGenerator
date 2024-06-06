using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(LovePartnerRelationUtility))]
    [HarmonyPatch(nameof(LovePartnerRelationUtility.LovePartnerRelationGenerationChance))]
    public static class Patch_LovePartnerRelationUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundGay = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundGay && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == SexualityGeneratorRefs.m_TraitSet_HasTrait)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, SexualityGeneratorRefs.f_Pawn_gender);
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_IsAttractedTo);
                    foundGay = true;
                    continue;
                }

                if (foundGay && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == SexualityGeneratorRefs.m_TraitSet_HasTrait)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, SexualityGeneratorRefs.f_Pawn_gender);
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_IsAttractedTo);
                    yield return new CodeInstruction(OpCodes.Neg);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
