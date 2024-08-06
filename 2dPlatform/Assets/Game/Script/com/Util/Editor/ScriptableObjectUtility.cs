using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace com
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static string CreateAsset<T>() where T : ScriptableObject
        {
            return CreateAsset<T>("New" + typeof(T).ToString());
        }

        public static string CreateAsset<T>(string name, string directory) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(directory + "/" + name + ".asset");
            Debug.Log(assetPathAndName);

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            Debug.Log(assetPathAndName);
            return assetPathAndName;
        }

        public static string CreateAsset<T>(string name) where T : ScriptableObject
        {

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return CreateAsset<T>(name, path);
        }

        public static T GetAssetByFullPath<T>(string path) where T : ScriptableObject
        {
            T asset = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
            return asset;
        }

        public static Object[] GetAllAssetsByFullPath(string path)
        {
            return (Object[])AssetDatabase.LoadAllAssetsAtPath(path);
        }

        public static T GetAssetByRelativePath<T>(string name) where T : ScriptableObject
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            path += name + ".asset";

            return GetAssetByFullPath<T>(path);
        }
    }
}
