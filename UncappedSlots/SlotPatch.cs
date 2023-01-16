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
        foreach (CodeInstruction instruction in instructions)
        {
            if (instruction.opcode != OpCodes.Callvirt || instruction.operand is not MethodInfo info || info != AccessTools.Method(typeof(YamlConfig), nameof(YamlConfig.GetInt)))
            {
                yield return instruction;
                continue;
            }

            yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(SlotPatch), nameof(UncappedSlots)));
        }
    }

    private static int UncappedSlots(YamlConfig _, string key, int def) => -1;
}