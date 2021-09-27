using SaveLoadSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RiHarvest
{
    static class GardenTracker
    {

        public const int maxHarvest = 6;
        public const int maxGrowth = 6;

        static ProgressState saveState;
        static int growthBoost;
        static int harvestBoost;
        const string path = "moddata/ri/gardenEffects";

        static GardenTracker()
        {
            
        }

        public static void setGrowth(int level)
        {
            growthBoost = Mathf.Clamp(level, 0, maxGrowth);
        }

        public static void setHarvest(int level)
        {
            harvestBoost = Mathf.Clamp(level, 0, maxHarvest);
        }

        public static int getGrowth()
        {
            return growthBoost;
        }

        public static int getHarvest()
        {
            return harvestBoost;
        }

        public static void setSaveState(ProgressState state)
        {
            saveState = state;
        }

        public static ProgressState getSaveState()
        {
            return saveState;
        }

        public static void writeCurrentState(ref int id)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var savePath = $"{path}/{saveState.version}_{id}.json";

                GardenState garden = new GardenState(id);

                string json = JsonUtility.ToJson(garden);
                Console.WriteLine(json);
                File.WriteAllText(savePath, json);

            } catch {

                Console.WriteLine("[RiHarvest] failed to save your boosts to slot "+id);
            }
}

        public static void readSavedState(int index)
        {
            try
            {
                string filePath = $"{path}/{saveState.version}_{index}.json";

                GardenState arr = new GardenState(0);
                var saveContents = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(saveContents, arr);

                GardenTracker.setGrowth(arr.getGrowth() | 0);
                GardenTracker.setHarvest(arr.getHarvest() | 0);

            } catch {

                Console.WriteLine("[RiHarvest] failed to load your boosts to slot "+index); ;
                GardenTracker.setGrowth(0);
                GardenTracker.setHarvest(0);
            }
        }

    }
}
