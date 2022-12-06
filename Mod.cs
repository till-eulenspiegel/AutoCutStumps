using UnityEngine;
using Verse;

namespace AutocutStumps
{
    public class AutcutStumpsMod : Mod
    {
        AutocutStumpSettings settings;
        public AutcutStumpsMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<AutocutStumpSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("SettingsSpawnOutsideOption".Translate(), ref AutocutStumpSettings.spawnStumpsOutsideCutArea, "SettingsSpawnOutsideDescription".Translate());
            listingStandard.CheckboxLabeled("SettingsAutocutNotChoppedOption".Translate(), ref AutocutStumpSettings.onlyAutocutHarvestedStumps, "SettingsAutocutNotChoppedDescription".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "Autocut Stumps";
        }
    }
}
