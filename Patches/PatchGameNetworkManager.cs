﻿using System;
using HarmonyLib;
using UnityEngine;
using LCSeedPicker.Components;

namespace LCSeedPicker.Patches
{
    [HarmonyPatch(typeof(GameNetworkManager))]
    internal class PatchGameNetworkManager
    {
		[HarmonyPatch("SteamMatchmaking_OnLobbyCreated")]
		[HarmonyPrefix]
		public static void PatchOnLobbyCreated()
		{
			if (Plugin.SeedInput == null)
            {
				Plugin.SeedInput = new GameObject("SeedInputField");
				Plugin.SeedInput.hideFlags = HideFlags.HideAndDontSave;
				Plugin.SeedInput.AddComponent<SeedInput>();
				

				Plugin.Logger.LogInfo((object)"GUI Created");
            }
		}
	}
}
