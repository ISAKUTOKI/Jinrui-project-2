using UnityEngine;
using UnityEditor;

namespace com
{
    public static class PreviewRenderUtilityExt
    {
        public static void Draw( PreviewRenderUtility previewUtility, GameObject go, Vector3 localPos )
        {
            foreach ( var mr in go.GetComponentsInChildren<MeshRenderer>( false ) ) {
                var mf = mr.GetComponent<MeshFilter>();
                if ( mf == null )
                    continue;
                previewUtility.DrawMesh(
                    mf.sharedMesh,
                    Matrix4x4.TRS( localPos - go.transform.position, Quaternion.identity, Vector3.one ) *
                    mr.transform.localToWorldMatrix,
                    mr.sharedMaterial, 0 );
            }
        }
    }
}
