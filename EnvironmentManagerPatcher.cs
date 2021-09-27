using HarmonyLib;
using ObjectBased.Garden.GrowingSpot;
using SaveLoadSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RiHarvest
{
    /*EnvironmentManager.StartNight
     * Intent: Apply bonus growth effect to the garden overnight
     */
    [HarmonyPatch(typeof(EnvironmentManager), "StartNight")]
    internal class EnvironmentManager_StartNight_Patch
    {
        static void Postfix()
        {
            DayTracker.isBoosted = false;
            int hits = 0;
            for (int i = 0; i < GardenTracker.getGrowth(); i++)
            {
                int rnd = Random.Range(1, 3);
                Console.WriteLine(rnd);
                
                if (rnd == 1)
                {
                    Console.WriteLine("Extra growth achieved!");
                    hits++;
                    GrowingSpotObject.GrowPlants();
                }
            }

            GardenTracker.setGrowth(GardenTracker.getGrowth()-hits);

        }
    }
}