using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace SexualityGenerator
{
    public static class SexualityGeneratorRefs
    {
        public static readonly FieldInfo f_TraitDefOf_Bisexual = AccessTools.Field(typeof(TraitDefOf), nameof(TraitDefOf.Bisexual));
        public static readonly FieldInfo f_Pawn_gender = AccessTools.Field(typeof(Pawn), nameof(Pawn.gender));
        public static readonly FieldInfo f_Pawn_RelationsTracker_pawn = AccessTools.Field(typeof(Pawn_RelationsTracker), "pawn");

        public static readonly MethodInfo m_TraitSet_HasTrait = AccessTools.Method(typeof(TraitSet), nameof(TraitSet.HasTrait), new[] { typeof(TraitDef) });
        public static readonly MethodInfo m_Utility_IsAttractedToAllGenders = AccessTools.Method(typeof(Utility), nameof(Utility.IsAttractedToAllGenders));
        public static readonly MethodInfo m_Utility_GetRomanceAttemptSexualityFactor = AccessTools.Method(typeof(Utility), nameof(Utility.GetRomanceAttemptSexualityFactor));
        public static readonly MethodInfo m_Utility_IsAttractedTo = AccessTools.Method(typeof(Utility), nameof(Utility.IsAttractedTo));
        public static readonly MethodInfo m_Utility_IsSexualityTraitDef = AccessTools.Method(typeof(Utility), nameof(Utility.IsSexualityTraitDef));
    }
}
