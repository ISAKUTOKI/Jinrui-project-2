using System.Collections.Generic;
using UnityEngine;

namespace com
{
    public class TextFormat : MonoBehaviour
    {
        public static void DumpToConsole(object obj)
        {
            var output = JsonUtility.ToJson(obj, true);
            Debug.Log(output);
        }

        public static void LogObj(object obj)
        {
            DumpToConsole(obj);
        }

        public static string GetRestTimeStringFormated(System.TimeSpan time, bool hasDay = false)
        {
            if (time.Ticks <= 0)
                return "00:00:00";

            int days = time.Days;
            int hours = time.Hours;
            int minutes = time.Minutes;
            int secs = time.Seconds;
            if (!hasDay)
                hours += days * 24;

            string s1 = GetNumberWithFixedLength(hours, 2);
            string s2 = GetNumberWithFixedLength(minutes, 2);
            string s3 = GetNumberWithFixedLength(secs, 2);

            if (hasDay)
            {
                string s0 = GetNumberWithFixedLength(days, 2);
                return s0 + ":" + s1 + ":" + s2 + ":" + s3;
            }

            return s1 + ":" + s2 + ":" + s3;
        }

        public static string GetNumberWithFixedLength(int num, int length)
        {
            string res = num + "";
            int k = 3;
            while (res.Length < length && k > 0)
            {
                res = res.Insert(0, "0");
                k--;
            }
            return res;
        }

        public static string GetBigNumberScientific(int number)
        {
            string n = number + "";
            if (number > 10000000)
            {
                n = Mathf.FloorToInt(number / 1000000f) + "M";
            }
            else if (number > 10000)
            {
                n = Mathf.FloorToInt(number / 1000f) + "k";
            }
            return n;
        }

        public static string GetRichTextTag(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "";

            return "<sprite name=" + id + ">";
        }
    }
}