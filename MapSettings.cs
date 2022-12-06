using Verse;

namespace AutocutStumps
{
    internal class AutocutStumpMapSettings : MapComponent
    {
        private Area internalArea;
        public Area area
        {
            set
            {
                internalArea = value;
                //Log.Message("Setting area to " + value);
            }
        }

        public AutocutStumpMapSettings(Map map) : base(map) { } // Will this reload from the game-file everytime? Is that okay for a stump?

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref internalArea, "autcutStumpsArea");
        }

        public static AutocutStumpMapSettings getOrCreateMapSettings(Map map)
        {
            if (map == null)
            {
                return null;
            }

            var settings = map.GetComponent<AutocutStumpMapSettings>();
            if (settings == null)
            {
                settings = new AutocutStumpMapSettings(map);
                map.components.Add(settings);
            }

            return settings;
        }

        public Area getArea()
        {
            if (internalArea != null)
            {
                //Log.Message("Internal area set to" + internalArea);
                return internalArea;
            }

            //Log.Message("Internal area not set");
            return map.areaManager.Home;
        }
    }

}
