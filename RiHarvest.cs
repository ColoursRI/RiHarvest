﻿using BepInEx;
using RiHarvest;
using HarmonyLib;
using ObjectBased.Garden.GrowingSpot;
using System;
[BepInPlugin("com.ri.potioncraft.riharvest", "RiHarvest", "1.0.0.0")]
public class RiPatcher : BaseUnityPlugin
{
    void Awake()
    {
        UnityEngine.Debug.Log("[RiHarvest] launching!");
        Patcher();
    }

    void Patcher()
    {
        try
        {
            var harmony = new Harmony("com.ri.potioncraft.riharvest");
            harmony.PatchAll();
        } catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }
}
