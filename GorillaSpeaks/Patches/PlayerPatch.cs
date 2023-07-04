using HarmonyLib;
using Photon.Pun;
using Photon.Voice.PUN;

namespace GorillaSpeaks.Patches
{
    [HarmonyPatch(typeof(VRRigSerializer))]
    [HarmonyPatch("OnInstantiateSetup", MethodType.Normal)]
    internal class VRRigPatch
    {
        internal static void Postfix(VRRigSerializer __instance)
        {
            if (__instance.vrrig.gameObject.GetComponent<Mouthies>() == null)
            {
                __instance.vrrig.gameObject.AddComponent<Mouthies>();
                __instance.vrrig.GetComponent<Mouthies>().wig = __instance.vrrig;
                __instance.vrrig.GetComponent<Mouthies>().voicethingy = __instance.GetComponent<PhotonVoiceView>();
            }
            else
            {
                __instance.vrrig.GetComponent<Mouthies>().wig = __instance.vrrig;
                __instance.vrrig.GetComponent<Mouthies>().voicethingy = __instance.GetComponent<PhotonVoiceView>();
            }
        }
    }
}