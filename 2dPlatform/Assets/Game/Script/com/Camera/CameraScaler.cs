using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com
{
    public class CameraScaler : MonoBehaviour
    {
        public Camera cam;
        public List<Transform> targets;
        public Vector3 offset;
        public float relativeVacantDistanceFactor = 0.2f;
        public float minSize = 5f;
        public float maxSize = 20f;
        private float _screenWHFactor;

        private void Start()
        {
            _screenWHFactor = (float)Screen.width / (float)Screen.height;
        }

        void Update()
        {
            SetCam();
        }

        protected virtual void SetCam()
        {
            var bound = GetBound();
            cam.transform.position = GetCamPos(bound);
            cam.orthographicSize = GetCamOrthoSize(bound);
        }

        protected float GetCamOrthoSize(Vector4 bound)
        {
            float res = 1;
            //当屏幕宽高比1:1时 如果两个物体水平间距为d,则当orthographicSize为d时，两个物体刚好在屏幕边缘
            //当屏幕宽高比2:1时 如果两个物体水平间距为d,则当orthographicSize为2d时，两个物体刚好在屏幕边缘
            float distX = Mathf.Abs(bound.z - bound.x);
            float distY = Mathf.Abs(bound.w - bound.y);
            float distFactor = distX / distY;
            if (distFactor > _screenWHFactor)
            {
                //水平距离相对大，只考虑适配水平最大间距
                res = (relativeVacantDistanceFactor + 1) * distX / _screenWHFactor * 0.5f;
            }
            else
            {
                //垂直距离相对大，只考虑适配垂直最大间距
                res = (relativeVacantDistanceFactor + 1) * distY / _screenWHFactor * 1f;//垂直就是和水平不一样的系数 不知道为什么
            }


            return Mathf.Clamp(res, minSize, maxSize);
        }

        protected Vector3 GetCamPos(Vector4 bound)
        {
            Vector3 centerPosition = Vector3.zero;
            centerPosition.x = (bound.x + bound.z) * 0.5f;
            centerPosition.y = (bound.y + bound.w) * 0.5f;
            return centerPosition + offset;
        }

        /// <summary>
        /// get the bound rectangle of all targets
        /// </summary>
        /// <returns>min x, min y, max x, max y</returns>
        protected Vector4 GetBound()
        {
            Vector4 res = new Vector4(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
            foreach (var t in targets)
            {
                var p = t.position;
                if (p.x > res.x)
                    res.x = p.x;
                if (p.y > res.y)
                    res.y = p.y;
                if (p.x < res.z)
                    res.z = p.x;
                if (p.y < res.w)
                    res.w = p.y;
            }

            return res;
        }
    }
}