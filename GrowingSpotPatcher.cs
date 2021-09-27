using HarmonyLib;
using ObjectBased.Garden.GrowingPot;
using ObjectBased.Garden.GrowingSpot;
using ObjectBased.UIElements.FloatingText;
using SaveLoadSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPAtlasGenerationSystem;
using TutorialSystem;
using UnityEngine;
using System.Reflection;
using Random = UnityEngine.Random;

namespace RiHarvest
{
    /* 
     * GrowingObject.InstantiatePlant
     * Intent: Roll to give new plants extra ingredients according to the harvest bonus
     */
    [HarmonyPatch(typeof(GrowingSpotObject), "InstantiatePlant")]
    internal class HarvestToInventory_Patch
    {
        static void Postfix(SpotPlant __instance, GameObject ___plantGameObject)
        {
            SpotPlant plant = ___plantGameObject.GetComponent<SpotPlant>();

            if (plant != null)
            {
                var field = plant.GetType().GetField("ingredientAmount", BindingFlags.NonPublic | BindingFlags.Instance);

                Vector2Int vector = (Vector2Int)field.GetValue(plant);
                var harvest = GardenTracker.getHarvest();

                int hits = 0;

                for (int i = 0; i < GardenTracker.getHarvest(); i++)
                {
                    int rnd = Random.Range(1, 3);
                    Console.WriteLine(rnd);

                    if (rnd == 1)
                    {
                        Console.WriteLine("[RiHarvest] Extra harvest recieved on grown plant!");
                        hits++;
                    }

                }

                if (hits == 0) { return; }

                int mult = (int)(1.25 * hits); /// Ch-ch-ch-ch-changes
                Vector2Int newVector = new Vector2Int((int)(vector.x * mult), (int)((int)vector.y * mult));
                field.SetValue(plant, newVector);
            }
        }
    }


    /* 
     * GGrowingSpotObject.GrowPlants
     * Intent: Track degredation of our harvest bonus. -1 harvest each time a new bunch of plants grow.
     */
    [HarmonyPatch(typeof(GrowingSpotObject), "GrowPlants")]
    internal class HarvestToInventory3_Patch
    {
        static void Postfix(GrowingSpotObject __instance)
        {
            if (GardenTracker.getHarvest() > 0)
            {
                GardenTracker.setHarvest(GardenTracker.getHarvest() - 1);
            }
        }
    }
}
