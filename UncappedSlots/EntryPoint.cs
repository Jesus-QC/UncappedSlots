using HarmonyLib;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace UncappedSlots;

public class EntryPoint
{
    public const string Version = "1.0.0.0";
    
    private static readonly Harmony HarmonyPatcher = new("uncappedslots.jesusqc.com");

    [PluginEntryPoint("UncappedSlots", Version, "Removes slot limits by hosting providers.", "Jesus-QC")]
    private void Init()
    {
        Log.Raw($"<color=blue>Loading UncappedSlots {Version} by Jesus-QC</color>");
        HarmonyPatcher.PatchAll();
    }
}
