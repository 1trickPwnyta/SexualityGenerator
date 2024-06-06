using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SexualityGenerator
{
    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt))]
    [HarmonyPatch(nameof(InteractionWorker_RomanceAttempt.RandomSelectionWeight))]
    public static class Patch_InteractionWorker_RomanceAttempt
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Stloc_S && ((LocalBuilder)instruction.operand).LocalIndex == 4)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldc_R4, 1f);
                }

                if (instruction.opcode == OpCodes.Ldloc_S && ((LocalBuilder)instruction.operand).LocalIndex == 7)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, SexualityGeneratorRefs.m_Utility_GetRomanceAttemptSexualityFactor);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
