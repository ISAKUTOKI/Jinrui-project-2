using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com
{
    public class MiscUtil
    {
        private static System.Text.StringBuilder _stringBuilder = new System.Text.StringBuilder();

        public static string Md5Sum(string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
#if UNITY_WP8
            //System.Security.Cryptography.SHA1CryptoServiceProvider crypt = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashBytes = Crypto.ComputeSHA1Hash(bytes);
#else
            System.Security.Cryptography.MD5CryptoServiceProvider crypt = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = crypt.ComputeHash(bytes);
#endif


            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        /// <summary>
        /// Gets the screenshot.
        /// WARNING: use Destroy(texture) after the screenshot is no longer needed
        /// </summary>
        /// <value>The screenshot.</value>
        public static Texture2D Screenshot
        {
            get
            {
                List<Camera> cameras = new List<Camera>();
                cameras.Add(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("GUICamera"))
                {
                    cameras.Add(go.GetComponent<Camera>());
                }

                cameras.Sort((x, y) => x.depth.CompareTo(y.depth));

                int width = Screen.width;
                int height = Screen.height;
                Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
                RenderTexture rt = new RenderTexture(width, height, 24);


                GameObject watermarkGO = GameObject.FindWithTag("Watermark");
                if (watermarkGO == null)
                {
                    Debug.LogError("No watermark found!");
                }
                else
                {
                    //watermarkGO.SetActive(true);
                }

                foreach (Camera cam in cameras)
                {
                    RenderTexture current = cam.targetTexture;
                    cam.targetTexture = rt;
                    bool enabled = cam.enabled;
                    cam.enabled = true;
                    cam.Render();
                    cam.enabled = enabled;
                    cam.targetTexture = current;
                }

                if (watermarkGO != null)
                {
                    //watermarkGO.SetActive(false);
                }
                RenderTexture.active = rt;
                // Read render texture contents into the texture
                tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                tex.Apply();

                RenderTexture.active = null;
                Object.Destroy(rt);
                return tex;
            }
        }

        public static object JsonDeserialize(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                return MiniJSON.Json.Deserialize(data);
            }
            return null;
        }

        public static string JsonSerialize(object o)
        {
            return MiniJSON.Json.Serialize(o);
        }

        public static object DeserializeAndDecrypt(string data, string key)
        {
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    string json = XOREncryptOrDecrypt(data, XOREncryptOrDecrypt(key, "512"));
                    return MiniJSON.Json.Deserialize(json);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning("Error when deserializing: " + e.ToString());
                }
            }
            return null;
        }

        public static string SerializeAndEncrypt(object obj, string key)
        {
            string json = MiniJSON.Json.Serialize(obj);
            return XOREncryptOrDecrypt(json, XOREncryptOrDecrypt(key, "512"));
        }

        public static string XOREncryptOrDecrypt(string text, string key)
        {
            _stringBuilder.Length = 0;

            for (int c = 0; c < text.Length; c++)
                _stringBuilder.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return _stringBuilder.ToString();
        }

        public static byte[] ToBytes(string dataToByte)
        {
            return System.Text.Encoding.UTF8.GetBytes(dataToByte);
        }

        public static string FromBytes(byte[] dataToString)
        {
            return System.Text.Encoding.UTF8.GetString(dataToString);
        }

        public static int ToCharArray(uint value, char[] buffer)
        {
            const int maxLength = 10;
            System.Array.Clear(buffer, 0, buffer.Length);

            if (value == 0)
            {
                buffer[0] = '0';
                return 1;
            }

            int startIndex = maxLength - 1;
            int index = startIndex;
            do
            {
                buffer[index] = (char)('0' + value % 10);
                value /= 10;
                --index;
            }
            while (value != 0);

            int length = startIndex - index;

            int bufferIndex = 0;
            if (bufferIndex != index + 1)
            {
                while (index != startIndex)
                {
                    ++index;
                    buffer[bufferIndex] = buffer[index];
                    ++bufferIndex;
                }
            }

            return length;
        }
    }
}
