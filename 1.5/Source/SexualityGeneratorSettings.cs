using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace SexualityGenerator
{
    public class SexualityGeneratorSettings : ModSettings
    {
        public static float GayCommonality;
        public static float StraightCommonality;
        public static float BisexualCommonality;
        public static float AsexualCommonality;
        public static float HeteroflexibleCommonality;
        public static float HomoflexibleCommonality;

        static SexualityGeneratorSettings()
        {
            RestoreDefaultSettings();
        }

        public static void RestoreDefaultSettings()
        {
            GayCommonality = 0.18f;
            StraightCommonality = 0.18f;
            BisexualCommonality = 0.5f;
            AsexualCommonality = 0.1f;
            HeteroflexibleCommonality = 0.02f;
            HomoflexibleCommonality = 0.02f;
        }

        private void AllotLeftovers()
        {
            BisexualCommonality += 1f - TotalCommonality();
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            DoCommonalitySlider(listingStandard, ref BisexualCommonality, "SexualityGenerator_BisexualCommonality", "SexualityGenerator_BisexualCommonalityDesc");
            DoCommonalitySlider(listingStandard, ref HomoflexibleCommonality, "SexualityGenerator_HomoflexibleCommonality", "SexualityGenerator_HomoflexibleCommonalityDesc");
            DoCommonalitySlider(listingStandard, ref HeteroflexibleCommonality, "SexualityGenerator_HeteroflexibleCommonality", "SexualityGenerator_HeteroflexibleCommonalityDesc");
            DoCommonalitySlider(listingStandard, ref GayCommonality, "SexualityGenerator_GayCommonality", "SexualityGenerator_GayCommonalityDesc");
            DoCommonalitySlider(listingStandard, ref StraightCommonality, "SexualityGenerator_StraightCommonality", "SexualityGenerator_StraightCommonalityDesc");
            DoCommonalitySlider(listingStandard, ref AsexualCommonality, "SexualityGenerator_AsexualCommonality", "SexualityGenerator_AsexualCommonalityDesc");
            listingStandard.Label("SexualityGenerator_NoteLeftoverPoints".Translate());
            if (listingStandard.ButtonText("SexualityGenerator_RestoreDefaultSettings".Translate(), null, 0.25f))
            {
                RestoreDefaultSettings();
                SoundDefOf.Click.PlayOneShotOnCamera();
            }

            listingStandard.End();
        }

        private static void DoCommonalitySlider(Listing_Standard listingStandard, ref float commonality, string labelKey, string tooltipKey)
        {
            commonality = Mathf.Min(1f - TotalCommonality() + commonality, Mathf.RoundToInt(listingStandard.SliderLabeled(labelKey.Translate(commonality), commonality, 0f, 1f, 0.5f, tooltipKey.Translate()) * 100) / 100f);
        }

        private static float TotalCommonality()
        {
            return GayCommonality + StraightCommonality + BisexualCommonality + AsexualCommonality + HeteroflexibleCommonality + HomoflexibleCommonality;
        }

        public override void ExposeData()
        {
            AllotLeftovers();
            Scribe_Values.Look(ref GayCommonality, "GayCommonality", 0.18f);
            Scribe_Values.Look(ref StraightCommonality, "StraightCommonality", 0.18f);
            Scribe_Values.Look(ref BisexualCommonality, "BisexualCommonality", 0.5f);
            Scribe_Values.Look(ref AsexualCommonality, "AsexualCommonality", 0.1f);
            Scribe_Values.Look(ref HeteroflexibleCommonality, "HeteroflexibleCommonality", 0.02f);
            Scribe_Values.Look(ref HomoflexibleCommonality, "HomoflexibleCommonality", 0.02f);
            AllotLeftovers();
        }
    }
}
