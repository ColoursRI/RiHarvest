using HarmonyLib;
using ObjectBased.Garden.GrowingSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiHarvest
{
    /*ItemFromInventory.ReleaseToPlayZone
     * Intent: To detect potions being dropped in the gardens and keep count of our effects
     */
    [HarmonyPatch(typeof(ItemFromInventory), "ReleaseToPlayZone")]
    internal class ItemFromInventory_ReleaseToPlayZone_Patch
    {
        static void Postfix(ItemFromInventory __instance)
        {

           
            RoomManager roommng = Managers.Room;

            if (roommng.currentRoom == RoomManager.RoomIndex.Garden && __instance is PotionItem)
            {
                int effects = 0;
                int boost = 0;

                PotionItem potion = (PotionItem)__instance;
                Potion potion1 = (Potion)potion.inventoryItem;
                

                foreach (PotionEffect effect in potion1.effects)
                {
                    

                    Console.Write(effect.ToString());
                    if (effect.name == "Crop" && GardenTracker.getHarvest() < GardenTracker.maxHarvest)
                    {
                        GardenTracker.setHarvest(GardenTracker.getHarvest() + 1);
                        effects++;
                    }

                    


                    if (effect.name == "Growth" && GardenTracker.getGrowth() < GardenTracker.maxGrowth)
                    {
                        Console.WriteLine("Growth detected");
                        GardenTracker.setGrowth(GardenTracker.getGrowth() + 1);
                        boost++;
                        effects++;
                    }


                }


                if (boost > 2) // Growth 3 results in instant growth
                {
                    Console.WriteLine("Applyig instant bonus from level III potion!");
                    GrowingSpotObject.GrowPlants();
                    GardenTracker.setGrowth(GardenTracker.getGrowth() - 1);
                }

                if (effects > 0)
                {
                    potion.DestroyItem(); // Move this, probably, yeah?
                }

            }

        }
    }
}
