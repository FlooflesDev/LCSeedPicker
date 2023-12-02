using HarmonyLib;
using LCSeedPicker.Components;

namespace LCSeedPicker.Patches
{

    [HarmonyPatch(typeof(QuickMenuManager))]
    class PatchQuickMenuManager
    {
        [HarmonyPatch("OpenQuickMenu")]
        [HarmonyPrefix]
        public static void openMenu()
        {
			open();
		}

		[HarmonyPatch("CloseQuickMenu")]
		[HarmonyPrefix]
		public static void closeMenu()
		{
			close();
		}

		[HarmonyPatch("LeaveGameConfirm")]
		[HarmonyPrefix]
		public static void destroyMenuOnLeave()
		{
			Plugin.Logger.LogInfo("Left game, input should be destroyed.");

			GetSeedInput().DestroyInput();
			Plugin.SeedInput = null;
		}

		// TODO: Open menu when leave game is declined.
		[HarmonyPatch("LeaveGame")]
		[HarmonyPrefix]
		public static void openLeaveGameWindow()
		{
			close();
		}



		private static void open()
        {
			Plugin.Logger.LogInfo("Menu opened, input should be visible.");
			Plugin.Logger.LogInfo($"Seedinput: {GetSeedInput()}");
			GetSeedInput().isVisible = true;
			Plugin.Logger.LogInfo($"Visible from Menu: {GetSeedInput().isVisible}");
		}
		private static void close()
        {
			Plugin.Logger.LogInfo("Menu closed, input should be hidden.");
			Plugin.Logger.LogInfo($"Seedinput: {GetSeedInput()}");
			GetSeedInput().isVisible = false;
			Plugin.Logger.LogInfo($"Visible from Menu: {GetSeedInput().isVisible}");
		}

		private static SeedInput GetSeedInput()
        {
			return Plugin.SeedInput.GetComponent<SeedInput>();
		}
	}
}
