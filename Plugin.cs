using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace LCSeedPicker
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static GameObject SeedInput;

        internal static new ManualLogSource Logger;

        private Harmony _harmony;


        private void Awake()
        {
            Logger = base.Logger;
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // Patch using Harmony
            PatchAll();
        }

        public void PatchAll()
        {
            
            Logger.LogDebug("Applying harmony patches");

            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll();

            Logger.LogDebug("Patching done.");
        }

        public void UnpatchAll()
        {

            Logger.LogDebug("Removing harmony patches.");

            _harmony.UnpatchSelf();

            Logger.LogDebug("Unpatching done.");
        }
    }
}