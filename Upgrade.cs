using HarmonyLib;
using System;

namespace MiniRealisticAirways
{
    [HarmonyPatch(typeof(UpgradeManager), "Start", new Type[] { })]
    class PatchUpgradeManagerStart
    {
        static bool Prefix(ref UpgradeManager __instance, ref float ___upgradeInterval)
        {
            // Double the speed for upgrade.
            ___upgradeInterval /= 2;
            return true;
        }

        static void Postfix(ref UpgradeManager __instance, ref int[] ___counter)
        {
            if (___counter.Length == 0)
            {
                return;
            }

            if (MapManager.gameMode == GameMode.SandBox)
            {
                return;
            }

            // Starts with 3 apron upgrade.
            for (int i = 0; i < 3; i++)
            {
                ___counter[(int)UpgradeOpt.LONGER_TAXIWAY]++;
                TakeoffTaskManager.Instance.AddApron();
            }
        }
    }
}