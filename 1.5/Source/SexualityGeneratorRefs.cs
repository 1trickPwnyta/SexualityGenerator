using HarmonyLib;
using RimWorld;
using System.Reflection;

namespace SexualityGenerator
{
    public static class SexualityGeneratorRefs
    {
        public static readonly FieldInfo f_TraitDefOf_Bisexual = AccessTools.Field(typeof(TraitDefOf), nameof(TraitDefOf.Bisexual));

        public static readonly MethodInfo m_TraitSet_HasTrait = AccessTools.Method(typeof(TraitSet), nameof(TraitSet.HasTrait));
        public static readonly MethodInfo m_Utility_IsAttractedToAllGenders = AccessTools.Method(typeof(Utility), nameof(Utility.IsAttractedToAllGenders));
    }
}
