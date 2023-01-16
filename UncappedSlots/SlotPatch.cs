using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace UncappedSlots;

[HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.CreateMatch))]
public class SlotPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
        
        int index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Callvirt && x.operand is MethodInfo info && info == AccessTools.Method(typeof(YamlConfig), nameof(YamlConfig.GetInt)));

        newInstructions[index].operand = AccessTools.Method(typeof(SlotPatch), nameof(UncappedSlots));
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static int UncappedSlots(YamlConfig _, string key, int def) => -1;
}