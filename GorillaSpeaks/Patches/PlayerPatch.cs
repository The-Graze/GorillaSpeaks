using HarmonyLib;

namespace GorillaSpeaks.Patches
{
    [HarmonyPatch(typeof(VRRig))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class VRRigPatch
    {
        internal static void Postfix(VRRig __instance)
        {
            if (__instance.GetComponent<Mouthies>() == null)
            {
                __instance.gameObject.AddComponent<Mouthies>();
            }
        }
    }
}