using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace com
{
    public class TextureChannelSwapWizzard : ScriptableWizard
    {

        public Texture2D sourceTex;
        public Texture2D destinationTex;

        public Channel srcChannel;
        public Channel dstChannel;

        public enum Channel
        {
            R, G, B, A
        };


        [MenuItem("Custom/Texture/Channels swap")]
        public static void CreateWizzard()
        {
            ScriptableWizard.DisplayWizard<TextureChannelSwapWizzard>("Texture Channels", "Ok");
        }

        void OnWizardCreate()
        {

            Color[] srcPixels = sourceTex.GetPixels();
            Color[] dstPixels = destinationTex.GetPixels();

            if (srcPixels.Length != dstPixels.Length) {
                Debug.LogError("Textures must be the same size");
                return;
            }
            for (int i = 0; i < srcPixels.Length; i++) {
                dstPixels[i] = SetChannel(dstPixels[i], GetChannel(srcPixels[i], srcChannel), dstChannel);
            }

            destinationTex.SetPixels(dstPixels);
            destinationTex.Apply();

            Debug.Log("Done");
        }

        protected float GetChannel(Color color, Channel channel)
        {
            switch (channel) {
                case Channel.A: return color.a;
                case Channel.B: return color.b;
                case Channel.G: return color.g;
                case Channel.R: return color.r;
            }

            Debug.LogError("We shouldnt be here");
            return 0f;
        }

        protected Color SetChannel(Color color, float val, Channel channel)
        {
            switch (channel) {
                case Channel.A: color.a = val; break;
                case Channel.B: color.b = val; break;
                case Channel.G: color.g = val; break;
                case Channel.R: color.r = val; break;
            }

            return color;
        }
    }
}
