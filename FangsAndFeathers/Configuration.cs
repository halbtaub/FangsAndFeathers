using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.IO;

namespace FangsAndFeathers;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool IsConfigWindowMovable { get; set; } = true;
    public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;

    public string Test { get; set; } = "JAJA";
    
    public Dictionary<uint,uint> AetheryteBeastTribeMap { get; set; } = new Dictionary<uint, uint>()
    {
        {1,19},     // Amalj'aa    - Little Ala Mhigo
        {2,4},      // Sylph       - The Hawthorne Hut
        {3,16},     // Kobolds     - Camp Overlook
        {4,14},     // Sahagin     - Aleport
        {5,7},      // Ixal        - Fallgourd Float
        {6,73},     // Vanu Vanu   - Ok' Zundu
        {7,76},     // Vath        - Tailfeather
        {8,79},     // Moogles     - Zenith
        {9,105},    // Kojin       - Tamamizu
        {10,99},    // Ananta      - The Peering Stones
        {11,128},   // Namazu      - Dhoro Iloh
        {12,144},   // Pixies      - Lydha Lran
        {13,143},   // Qitari      - Fanow
        {14,136},   // Dwarves     - The Ostall Imperative
        {15,169},   // Arkasodara  - Yedlihmad
        {16,181},   // Omicrons    - Base Omicron
        {17,175},   // Loporrits   - Bestways Burrow
        {18,238}    // Pelupelu    - Dock Poga
    };
    
    // the below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
