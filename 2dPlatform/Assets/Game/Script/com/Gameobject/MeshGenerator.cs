using UnityEngine;
using System.Collections;
namespace com
{
    [RequireComponent(typeof(MeshFilter))]
    public class MeshGenerator : MonoBehaviour
    {
        public float sizeX;
        public float sizeZ;
        protected Mesh mesh;
        protected Vector3[] vertices;
        protected int[] triangles;
        protected MeshFilter meshFilter;
        //triangles(its vertices) are clock wise
        public int divX;
        public int divZ;
        private void Start()
        {
            mesh = new Mesh();
            meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            divX = Mathf.Clamp(divX, 1, 100);
            divZ = Mathf.Clamp(divZ, 1, 100);

            CreateShape();
            UpdateMesh();
        }

        void CreateShape()
        {
            vertices = new Vector3[(divX + 1) * (divZ + 1)];
            float sizeDivX = sizeX / divX;
            float sizeDivZ = sizeZ / divZ;
            float halfX = sizeX * 0.5f;
            float halfZ = sizeZ * 0.5f;
            for (int i = 0, y = 0; y <= divZ; y++)
            {
                for (int x = 0; x <= divX; x++, i++)
                {
                    vertices[i] = new Vector3(x * sizeDivX - halfX, 0, y * sizeDivZ - halfZ);
                }
            }

            triangles = new int[divX * divZ * 6];
            for (int ti = 0, vi = 0, y = 0; y < divZ; y++, vi++)
            {
                for (int x = 0; x < divX; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + divX + 1;
                    triangles[ti + 5] = vi + divX + 2;
                }
            }
        }

        void CreateShape_tuto()
        {
            vertices = new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(0,0,1),
                new Vector3(1,0,0),
                new Vector3(1,0,1),
            };

            triangles = new int[]
            {
                0,1,2,
               2,1,3,
            };
        }

        void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;

            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
            }
            mesh.uv = uvs;


            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            //mesh.RecalculateTangents();
            //mesh.RecalculateUVDistributionMetrics();
        }
    }
}