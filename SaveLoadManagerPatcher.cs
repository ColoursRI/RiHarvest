using HarmonyLib;
using SaveLoadSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiHarvest
{
     /* 
     * SaveLoadManager.LoadProgress
     * Intent: Lad the correct garden bonus data for this save
     */
    [HarmonyPatch(typeof(SaveLoadManager), "LoadProgress")]
    internal class SaveLoadManager_LoadProgress_Patch
    {
        static void Postfix(SaveLoadManager __instance)
        {
            string str;
            int i;
        
            // I know this bit might be upsetting.
            if (__instance.SelectedProgressState.name[0].ToString().Equals("A"))
            {
                i = 0;
            } 
            else if ((__instance.SelectedProgressState.name[0].ToString().Equals("N")))
            {
                return;
            }
            else
            {
                str = __instance.SelectedProgressState.name[6].ToString();
                i = int.Parse(str);
            }

            
            GardenTracker.setSaveState(__instance.SelectedProgressState);
            GardenTracker.readSavedState(i);
        }
    }



    /*
    * SaveLoadProgressInPlayerPrefs.SaveStateToPlayerPrefs
    * Intent: Write the garden bonus data for this save
    */
    [HarmonyPatch(typeof(SaveLoadProgressInPlayerPrefs), "SaveStateToPlayerPrefs")]
    static class Daymanager_OnSave_Patch
    {
        static void Postfix(ref bool __result, ref int toId, ProgressState progressState)
        {
            Console.WriteLine("[RIHarvest] Writing state in slot "+toId);
            GardenTracker.writeCurrentState(ref toId);
  
        }
    }
}
