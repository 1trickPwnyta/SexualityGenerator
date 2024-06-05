using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SexualityGenerator
{
    public static class Utility
    {
        public static bool HasSexualityTrait(this Pawn pawn)
        {
            return pawn.story.traits.allTraits.Any(t => t.def.exclusionTags.Contains("SexualOrientation"));
        }

        public static bool IsAttractedToAllGenders(this Pawn pawn)
        {
            return !pawn.HasSexualityTrait() || pawn.story.traits.allTraits.Select(t => t.def).Intersect(new List<TraitDef>() { TraitDefOf.Bisexual, TraitDef.Named("Heteroflexible"), TraitDef.Named("Homoflexible") }).Any();
        }

        public static float SexualityCommonality(this TraitDef def)
        {
            if (def == TraitDefOf.Gay)
            {
                return 0.45f;
            }
            if (def == TraitDefOf.Bisexual)
            {
                return 0.2f;
            }
            if (def == TraitDefOf.Asexual)
            {
                return 0.1f;
            }
            if (def == TraitDef.Named("Straight"))
            {
                return 0.18f;
            }
            if (def == TraitDef.Named("Homoflexible"))
            {
                return 0.05f;
            }
            if (def == TraitDef.Named("Heteroflexible"))
            {
                return 0.02f;
            }
            return 0f;
        }
    }
}
