using BepInEx;
using GorillaSpeaks.Patches;
using Photon.Pun;
using Photon.Voice.PUN;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Utilla;

namespace GorillaSpeaks
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject Mouth;
        public static volatile Plugin Instance;
        void Start()
        { Utilla.Events.GameInitialized += OnGameInitialized;}
        void OnGameInitialized(object sender, EventArgs e)
        {
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("GorillaSpeaks.dumouthstuff.mouth");
            AssetBundle bundle = AssetBundle.LoadFromStream(str);
            GameObject sluber = bundle.LoadAsset<GameObject>("mouth");
            Mouth = sluber;
            HarmonyPatches.ApplyHarmonyPatches();
            Instance = this;
        }
    }


    public class Mouthies : MonoBehaviour
    {
        GameObject Mouth;
        Plugin p = Plugin.Instance;
        public VRRig wig;
        public PhotonVoiceView voicethingy;
        void Start()
        {
            if (wig.isOfflineVRRig)
            {
                Destroy(this);
                Destroy(Mouth);
            }
            else
            {
                Mouth = Instantiate(p.Mouth, wig.headMesh.transform.Find("gorillaface"));

                if (Mouth != null)
                {
                    Mouth.transform.localPosition = new Vector3(0, -0.0011f, 0);
                    Mouth.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Mouth.GetComponent<Renderer>().material.shader = wig.headMesh.transform.Find("gorillaface").GetComponent<Renderer>().material.shader;
                }
            }
        }
        void Update()
        {
            if (Mouth != null)
            {
                Mouth.SetActive(voicethingy.IsSpeaking || voicethingy.IsRecording);
            }
        }
    }
}
