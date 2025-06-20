using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace SexualityGenerator
{
    public static class Utility
    {
        public const float FlexibleSexualityNonPreferredFactor = 0.25f;

        public static bool IsSexualityTraitDef(this TraitDef def)
        {
            return def.exclusionTags != null && def.exclusionTags.Contains("SexualOrientation");
        }

        public static bool HasSexualityTrait(this Pawn pawn)
        {
            if (pawn.story != null && pawn.story.traits != null)
            {
                return pawn.story.traits.allTraits.Any(t => t.def.IsSexualityTraitDef());
            }
            else
            {
                return false;
            }
        }

        public static TraitDef GetSexualityTraitDef(this Pawn pawn)
        {
            if (pawn.story != null && pawn.story.traits != null)
            {
                return pawn.story.traits.allTraits.Select(t => t.def).Where(d => d.IsSexualityTraitDef()).FirstOrFallback(TraitDefOf.Bisexual);
            }
            else
            {
                return TraitDef.Named("Straight");
            }
        }

        public static bool IsAttractedTo(this Pawn pawn, Gender gender)
        {
            return pawn.GetAttractionFactor(gender) > 0f;
        }

        public static bool IsAttractedToAllGenders(this Pawn pawn)
        {
            return pawn.IsAttractedTo(Gender.Male) && pawn.IsAttractedTo(Gender.Female);
        }

        public static float GetAttractionFactor(this Pawn pawn, Gender gender)
        {
            TraitDef def = pawn.GetSexualityTraitDef();
            if (def == TraitDef.Named("Heteroflexible"))
            {
                return gender == pawn.gender ? FlexibleSexualityNonPreferredFactor : 1f;
            }
            if (def == TraitDef.Named("Homoflexible"))
            {
                return gender == pawn.gender ? 1f : FlexibleSexualityNonPreferredFactor;
            }
            if (def == TraitDefOf.Gay)
            {
                return gender == pawn.gender ? 1f : 0f;
            }
            if (def == TraitDef.Named("Straight"))
            {
                return gender == pawn.gender ? 0f : 1f;
            }
            if (def == TraitDefOf.Asexual)
            {
                return 0f;
            }
            return 1f;
        }

        public static float GetRomanceAttemptSexualityFactor(Pawn initiator, Pawn recipient)
        {
            return Mathf.Max(initiator.GetAttractionFactor(recipient.gender) * recipient.GetAttractionFactor(initiator.gender), 0.15f);
        }

        public static float SexualityCommonality(this TraitDef def)
        {
            if (def == TraitDefOf.Gay)
            {
                return SexualityGeneratorSettings.GayCommonality;
            }
            if (def == TraitDefOf.Bisexual)
            {
                return SexualityGeneratorSettings.BisexualCommonality;
            }
            if (def == TraitDefOf.Asexual)
            {
                return SexualityGeneratorSettings.AsexualCommonality;
            }
            if (def == TraitDef.Named("Straight"))
            {
                return SexualityGeneratorSettings.StraightCommonality;
            }
            if (def == TraitDef.Named("Homoflexible"))
            {
                return SexualityGeneratorSettings.HomoflexibleCommonality;
            }
            if (def == TraitDef.Named("Heteroflexible"))
            {
                return SexualityGeneratorSettings.HeteroflexibleCommonality;
            }
            return 0f;
        }
    }
}
