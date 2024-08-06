using UnityEngine;

namespace com
{
    public class MathGame
    {
        public static Vector3 Lerp3Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;

            return p;
        }

        public static Vector3 SmallerAngleVector3(Vector3 p)
        {
            if (p.x > 180)
            {
                p.x -= 360;
            }
            if (p.y > 180)
            {
                p.y -= 360;
            }
            if (p.z > 180)
            {
                p.z -= 360;
            }

            return p;
        }

        public static Quaternion Lerp3Bezier(Quaternion q0, Quaternion q1, Quaternion q2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p0 = q0.eulerAngles;
            Vector3 p1 = q1.eulerAngles;
            Vector3 p2 = q2.eulerAngles;
            p0 = SmallerAngleVector3(p0);
            p1 = SmallerAngleVector3(p1);
            p2 = SmallerAngleVector3(p2);

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            //var q = Quaternion(p.x, p.y, p.z);
            var q = new Quaternion();
            q.eulerAngles = p;
            return q;
        }

        public static Quaternion Lerp3(Quaternion a, Quaternion b, Quaternion c, float f)
        {
            if (f < 0.5f)
            {
                return Quaternion.Lerp(a, b, f * 2);
            }

            return Quaternion.Lerp(b, c, f * 2 - 1);
        }

        public static Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float f)
        {
            if (f < 0.5f)
            {
                return Vector3.Lerp(a, b, f * 2);
            }

            return Vector3.Lerp(b, c, f * 2 - 1);
        }

        public float EvauateCommonFormular(float p, float a3, float a2, float a1, float a0)
        {
            return a3 * Mathf.Pow(p, 3) + a2 * Mathf.Pow(p, 2) + a1 * p + a0;
        }

        public static int GetPercentage(int v, int percentage)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentage) / 100f;
            return Mathf.FloorToInt(res);
        }

        public static float GetPercentage(float v, int percentage)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentage) / 100f;
            return res;
        }

        public static int GetPercentageAdded(int v, int percentAdd)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentAdd + 100f) / 100f;
            return Mathf.FloorToInt(res);
        }

        public static float GetPercentageAdded(float v, int percentAdd)
        {
            // Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = v * (percentAdd + 100f) / 100f;
            return res;
        }

        public static int GetPercentage(int v, float percentage)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentage) / 100f;
            return Mathf.FloorToInt(res);
        }

        public static float GetPercentage(float v, float percentage)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentage) / 100f;
            return res;
        }

        public static int GetPercentageAdded(int v, float percentAdd)
        {
            //Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = (float)v * (percentAdd + 100f) / 100f;
            return Mathf.FloorToInt(res);
        }

        public static float GetPercentageAdded(float v, float percentAdd)
        {
            // Debug.Log("GetPercentageAdded " + v + " percentAdd " + percentAdd);
            float res = v * (percentAdd + 100f) / 100f;
            return res;
        }

        public static long SecondToTimeSpanTicks(float s)
        {
            // There are 10,000 ticks in a millisecond.
            return (long)s * 10000000;
        }

        public static float TimeSpanTicksToSeconds(long t)
        {
            //A single tick represents one hundred nanoseconds or one ten-millionth of a second.
            return t * 0.0000001f;
        }


        [System.Serializable]
        public struct CommonFormular
        {
            float a3;
            float a2;
            float a1;
            float a0;
        }
    }
}
