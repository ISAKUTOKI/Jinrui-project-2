using UnityEngine;
using UnityEditor;
using System.Collections;

namespace com
{
    [System.Serializable]
    public struct RenderScreenshotSettings
    {

        public int width;
        public int height;
        public Camera camera;
        public bool useAlpha;
    }

    public class RenderScreenshot : ScriptableWizard
    {
        public RenderScreenshotSettings settings;

        private static RenderScreenshotSettings _lastSettings;

        //public bool ScreenShot;

        [MenuItem( "Custom/Render Screenshot %g" )]
        public static void CreateWizard()
        {
            RenderScreenshot rs =  ScriptableWizard.DisplayWizard<RenderScreenshot>("Render screenshot", "Render");
            rs.settings = _lastSettings;
            if ( rs.settings.camera != null )
                return;
            rs.settings.camera = Camera.main;
            if ( Selection.activeGameObject != null ) {
                Camera c = Selection.activeGameObject.GetComponent<Camera>();
                if ( c != null ) {
                    rs.settings.camera = c;
                }
            }
        }

        void OnWizardCreate()
        {
            RenderTexture rt = new RenderTexture(settings.width, settings.height, 32);
            rt.antiAliasing = 8;
            settings.camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(settings.width, settings.height, settings.useAlpha ? TextureFormat.ARGB32 : TextureFormat.RGB24, false);
            settings.camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels( new Rect( 0, 0, settings.width, settings.height ), 0, 0 );

            RenderTexture.active = null;
            //DestroyImmediate(rt);
            var bytes = screenShot.EncodeToPNG();

            var directory = "Assets/Screenshots";
            if ( !System.IO.Directory.Exists( directory ) )
                System.IO.Directory.CreateDirectory( directory );
            System.IO.File.WriteAllBytes( System.IO.Path.Combine( directory, "renderScreenshot.png" ), bytes );
            settings.camera.targetTexture = null;
            _lastSettings = settings;
        }

    }
}
