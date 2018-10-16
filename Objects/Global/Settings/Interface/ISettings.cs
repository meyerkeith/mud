﻿using System.Collections.Generic;
using System.Net;

namespace Objects.Global.Settings.Interface
{
    public interface ISettings
    {
        int AssignableStatPoints { get; set; }
        int BaseStatValue { get; set; }
        int MaxLevel { get; set; }
        int MaxCalculationLevel { get; }
        double Multiplier { get; set; }
        string PlayerCharacterDirectory { get; set; }
        string VaultDirectory { get; set; }
        string ZoneDirectory { get; set; }
        string AssetsDirectory { get; set; }
        string AsciiArt { get; set; }
        int Port { get; set; }
        bool SendMapPosition { get; set; }
        int OddsOfGeneratingRandomDrop { get; set; }
        int OddsOfDropBeingPlusOne { get; set; }
        List<IPAddress> BannedIps { get; set; }
    }
}