using UnityEngine;

namespace com
{
    public class SpotLightBehaviour : MonoBehaviour
    {
        public Light spotLight;
        public CommonBehaviourPropertyChangeData intensityData;
        public CommonBehaviourPropertyChangeData angleData;
        public bool flagAwake = false;

        void Start()
        {
            if (spotLight == null)
            {
                spotLight = GetComponent<Light>();
            }

            StopLights();
        }


        void Update()
        {
            if (flagAwake)
            {
                flagAwake = false;
                if (intensityData.enabled)
                {
                    intensityData.Start();
                    spotLight.intensity = intensityData.startValue;
                }
                if (angleData.enabled)
                {
                    angleData.Start();
                    spotLight.spotAngle = angleData.startValue;
                }
                return;
            }

            if (intensityData.enabled && !intensityData.stopped)
            {
                spotLight.intensity = intensityData.Tick(com.GameTime.deltaTime);
            }
            if (angleData.enabled && !angleData.stopped)
            {
                spotLight.spotAngle = angleData.Tick(com.GameTime.deltaTime);
            }
        }

        public void StartLights()
        {
            flagAwake = true;
        }

        public void StopLights()
        {
            flagAwake = false;
            if (intensityData.enabled)
            {
                intensityData.Stop();
                intensityData.Reset();
                spotLight.intensity = intensityData.Tick(0);
            }
            if (angleData.enabled)
            {
                angleData.Stop();
                angleData.Reset();
                spotLight.spotAngle = angleData.Tick(0);
            }
        }
    }
}
