using UnityEngine;

namespace com
{
    public class Convert2DAnd3D
    {
        public static Vector3  GetScreenPosition(Camera cam, Vector3 pos, float canvasScale)
        {
            var pp = cam.WorldToScreenPoint(pos);
            pp /= canvasScale;
            return pp;
        }

        public static Vector3 GetWorldPosition(Camera cam, Vector3 pos, float canvasScale)
        {
            var pp = cam.ScreenToWorldPoint(pos);
            pp /= canvasScale;
            return pp;
        }
    }
}
