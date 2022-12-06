using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace AutocutStumps
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        public static readonly Texture2D autocutStumpButton;
        static HarmonyPatches()
        {

            var h = new Harmony("com.autochopstumps.rimworld.mod");
            autocutStumpButton = ContentFinder<Texture2D>.Get("AutocutIcon");
            h.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


    [HarmonyPatch(typeof(Plant), "TrySpawnStump")]
    public static class Patch_Plant_TrySpawnStump
    {
        static void Postfix(Plant __instance, ref PlantDestructionMode treeDestructionMode, ref Thing __result)
        {
            bool harvestStump = true;
            Thing stump = __result;

            if (stump == null)
            {
                return;
            }

            var currentMap = __instance.Map;
            AutocutStumpMapSettings mapSettings = AutocutStumpMapSettings.getOrCreateMapSettings(currentMap);




            var area = mapSettings.getArea();

            if (AutocutStumpSettings.onlyAutocutHarvestedStumps)
            {
                harvestStump = treeDestructionMode == PlantDestructionMode.Chop;
            }


            if (harvestStump && area[stump.Position])
            {

                harvestStump = true;
                __instance.Map.designationManager.AddDesignation(new Designation(__result, DesignationDefOf.HarvestPlant));
            }
            else
            {
                harvestStump = false;
            }

            if (!harvestStump && !AutocutStumpSettings.spawnStumpsOutsideCutArea)
            {
                __result.DeSpawn();
            }

            return;
        }
    }

    [HarmonyPatch(typeof(PlaySettings), "DoPlaySettingsGlobalControls")]
    public static class Patch_PlaySettings_DoPlaySettingsGlobalControls
    {
        private static bool showFloatingMenu;

        static void Postfix(WidgetRow row, bool worldView)
        {

            if (!worldView && row.ButtonIcon(HarmonyPatches.autocutStumpButton, "SelectAreaForAutocutStump".Translate()))
            {
                showFloatingMenuFunction();
            }
        }

        private static void showFloatingMenuFunction()
        {
            showFloatingMenu = false;
            FloatMenu floatMenu = makeFloatMenu();
            if (floatMenu != null)
            {
                Find.WindowStack.Add(floatMenu);
            }
        }

        private static FloatMenu makeFloatMenu()
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            Map currentMap = Find.CurrentMap;
            if (currentMap == null)
            {
                return null;
            }
            AutocutStumpMapSettings mapComponent = AutocutStumpMapSettings.getOrCreateMapSettings(currentMap);
            foreach (Area area in currentMap.areaManager.AllAreas)
            {
                list.Add(new FloatMenuOption(area.Label, delegate
                {
                    mapComponent.area = area;
                }));
            }

            return new FloatMenu(list);
        }
    }



}
