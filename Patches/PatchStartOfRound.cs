using HarmonyLib;
using LCSeedPicker.Components;


// Code heavily inspired by MoonOfTheDay (I am new to BepInEx)
namespace LCSeedPicker.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class PatchStartOfRound
    {
        [HarmonyPatch("StartGame")]
        [HarmonyPrefix]
        public static void SetSeed(StartOfRound __instance)
        {
            if (Plugin.SeedInput == null) return;

            SeedInput seedInput = Plugin.SeedInput.GetComponent<SeedInput>();
            // If currentLevel is null don't do anything (in what case is currentLevel null?)
            if (__instance.currentLevel == null || seedInput == null) return;

            int seed = seedInput.GetSeed();
            if (seed == -1)
            {
                Plugin.Logger.LogDebug($"Using random seed for {__instance.currentLevel.PlanetName}...");
                __instance.overrideRandomSeed = false;
                return;
            }

            Plugin.Logger.LogDebug($"Setting seed to {seed} for {__instance.currentLevel.PlanetName}...");

            __instance.overrideRandomSeed = true;
            __instance.overrideSeedNumber = seed;
        }

        [HarmonyPatch("StartGame")]
        [HarmonyPostfix]
        public static void GetSeed(StartOfRound __instance)
        {
            SeedInput seedInput = Plugin.SeedInput.GetComponent<SeedInput>();
            // If currentLevel is null don't do anything (in what case is currentLevel null?)
            if (__instance.currentLevel == null || seedInput == null) return;

            if (!__instance.overrideRandomSeed) {
                seedInput.SetLastSeed(__instance.randomMapSeed);
            }
        }
    }
}