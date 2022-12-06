using Verse;

namespace AutocutStumps
{
    public class AutocutStumpSettings : ModSettings
    {
        public static bool spawnStumpsOutsideCutArea = true;
        public static bool onlyAutocutHarvestedStumps = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref spawnStumpsOutsideCutArea, nameof(spawnStumpsOutsideCutArea));
            Scribe_Values.Look(ref onlyAutocutHarvestedStumps, nameof(onlyAutocutHarvestedStumps));
            base.ExposeData();
        }
    }

}
