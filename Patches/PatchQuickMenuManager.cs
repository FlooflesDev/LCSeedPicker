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
			GetSeedInput().DestroyInput();
			Plugin.SeedInput = null;
		}

		// TODO: Open input when leave game is declined.
		[HarmonyPatch("LeaveGame")]
		[HarmonyPrefix]
		public static void openLeaveGameWindow()
		{
			close();
		}



		private static void open()
        {
			SeedInput seedInput = GetSeedInput();
			if (seedInput != null) seedInput.isVisible = true;
		}
		private static void close()
        {
			SeedInput seedInput = GetSeedInput();
			if (seedInput != null) seedInput.isVisible = false;
		}

		private static SeedInput GetSeedInput()
        {
			if (Plugin.SeedInput == null) return null;
			return Plugin.SeedInput.GetComponent<SeedInput>();
		}
	}
}
