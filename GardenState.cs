using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RiHarvest
{
    [Serializable]
    public class GardenState
    {
        [SerializeField] int id;
        [SerializeField] int growth;
        [SerializeField] int harvest;

        public GardenState(int fromID)
        {
            id = fromID;
            growth = GardenTracker.getGrowth();
            harvest = GardenTracker.getHarvest();
        }

        public int getGrowth()
        {
            return growth;
        }

        public int getHarvest()
        {
            return harvest;
        }
    }
}
