using UnityEngine;
using System.Collections;
namespace com
{
    public class MoveMaterialNormalMap : MonoBehaviour
    {
        public float ampitude;
        public float freq;
        public Material material;
        void Update()
        {
            material.SetFloat("_BumpScale", Mathf.Sin(com.GameTime.time * freq) * ampitude);
        }
    }
}