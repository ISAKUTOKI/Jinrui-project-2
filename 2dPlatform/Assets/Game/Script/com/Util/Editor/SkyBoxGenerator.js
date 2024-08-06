#pragma strict
import System.Collections.Generic;

class SkyBoxGenerator extends ScriptableWizard
{
    var renderFromPosition : Transform;
    
    var screenSize = 1024;
    
    var farClipPlane : float = 10000f;
    
    var nearClipPlane : float = 0.01f;
 
    var skyBoxImage = new List.<String> (["frontImage", "rightImage", "backImage", "leftImage", "upImage", "downImage"]);
 
    var skyDirection = new List.<Vector3> ([Vector3 (0,0,0), Vector3 (0,-90,0), Vector3 (0,180,0), Vector3 (0,90,0), Vector3 (-90,0,0), Vector3 (90,0,0)]);
    
    var skyboxMaterial : Material;
    
    var backgroundColor = Color.black;
 
 
    function OnWizardUpdate()
    {
        helpString = "Select transform to render from";
        isValid = (renderFromPosition != null);
    }
 
    function OnWizardCreate()
    {
        var go = new GameObject ("SkyboxCamera", Camera);
 
        go.GetComponent.<Camera>().backgroundColor = backgroundColor;
        go.GetComponent.<Camera>().clearFlags = CameraClearFlags.Skybox;
        go.GetComponent.<Camera>().fieldOfView = 90;    
        go.GetComponent.<Camera>().aspect = 1.0;
        go.GetComponent.<Camera>().farClipPlane = farClipPlane;
        go.GetComponent.<Camera>().nearClipPlane = nearClipPlane;
        
        if (skyboxMaterial != null){
        	var sb : Skybox = go.AddComponent(Skybox);
        	sb.material = skyboxMaterial;
        }
 
        go.transform.position = renderFromPosition.position;
 
        if (renderFromPosition.GetComponent.<Renderer>())
        {
            go.transform.position = renderFromPosition.GetComponent.<Renderer>().bounds.center;
        }
 
        go.transform.rotation = Quaternion.identity;
 
        for (var orientation = 0; orientation < skyDirection.Count ; orientation++)
        {
            renderSkyImage(orientation, go);
        }
 
        DestroyImmediate (go);
    }
 
    @MenuItem("Custom/Render Skybox")
    static function RenderSkyBox()
    {
        ScriptableWizard.DisplayWizard ("Render SkyBox", SkyBoxGenerator, "Render!");
    }
 
    function renderSkyImage(orientation : int, go : GameObject)
    {
		go.transform.eulerAngles = skyDirection[orientation];
		
		var rt = new RenderTexture (screenSize, screenSize, 24);
		rt.antiAliasing =  8;
		go.GetComponent.<Camera>().targetTexture = rt;
		var screenShot = new Texture2D (screenSize, screenSize, TextureFormat.RGB24, false);
		go.GetComponent.<Camera>().Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels (Rect (0, 0, screenSize, screenSize), 0, 0); 
	 
		RenderTexture.active = null;
		DestroyImmediate (rt);
		var bytes = screenShot.EncodeToPNG(); 
	 
		var directory = "Assets/Skyboxes";
		if (!System.IO.Directory.Exists(directory))
		System.IO.Directory.CreateDirectory(directory);
		System.IO.File.WriteAllBytes (System.IO.Path.Combine(directory, skyBoxImage[orientation] + ".png"), bytes);   
    }
}