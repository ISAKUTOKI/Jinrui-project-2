using UnityEngine;
using UnityEditor;
using System.Collections;

namespace com
{
    public class DisableMaterialImport : AssetPostprocessor
    {

        public void OnPreprocessModel()
        {
            ModelImporter mi = assetImporter as ModelImporter;
            if (mi != null)
            {
                mi.materialImportMode = ModelImporterMaterialImportMode.None;
            }
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var str in importedAssets)
            {
                DeleteFBM(str);
            }

            foreach (var str in movedAssets)
            {
                DeleteFBM(str);
            }
        }

        private static void DeleteFBM(string path)
        {
            if (path.Contains(".fbm"))
            {
                FileUtil.DeleteFileOrDirectory(path);
                Debug.Log("Deleted automatically: " + path);
            }
        }
    }
}