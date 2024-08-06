using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace com
{
    public class EditorTools
    {
        public static IEnumerable<GameObject> SceneRoots()
        {
            var prop = new HierarchyProperty(HierarchyType.GameObjects);
            var expanded = new int[0];
            while (prop.Next(expanded)) {
                yield return prop.pptrValue as GameObject;
            }
        }

        [MenuItem("Assets/Copy Path(s)")]
        public static void CopyAssetPathToClipboard()
        {
            if (Selection.activeObject != null ) {
                EditorGUIUtility.systemCopyBuffer = "";
                bool first = true;
                foreach ( var guid in Selection.assetGUIDs ) {
                    if ( !first ) {
                        EditorGUIUtility.systemCopyBuffer += "\n";
                    }
                    first = false;
                    EditorGUIUtility.systemCopyBuffer += AssetDatabase.GUIDToAssetPath( guid );
                }
            }
            
        }
        [MenuItem( "Assets/Copy Resource Path(s)" )]
        public static void CopyResourcePathToClipboard()
        {
            CopyAssetPathToClipboard();
            var paths = EditorGUIUtility.systemCopyBuffer;
            List<string> list = new List<string>();
            foreach (var path in paths.Split('\n') ) {
                list.Add( ResourcePath( path ) );
            }
            EditorGUIUtility.systemCopyBuffer = string.Join( "\n", list.ToArray() );
        }

        public static string ResourcePath(string assetPath)
        {
            var res = "Resources/";
            int start = assetPath.IndexOf( res ) + res.Length;
            return assetPath.Substring( start, assetPath.IndexOf( '.' ) - start );
        }
    }
}
