using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathLessonProceduralPiece : MonoBehaviour
{
    Mesh mesh;
    [ContextMenu("Generate Mesh")]
    void GenerateMesh()
    {
        if(mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        List<Vector3> vertices = new List<Vector3>()
        {
            new Vector3(-1, 0, -1),//0
            new Vector3(-1.5f, 0, 0.5f),//1
            new Vector3(0, 0, 1.5f),//2
            new Vector3(+1.5f, 0, 0.5f),//3
            new Vector3(+1, 0, -1)//4
        };
        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up
        };
        List<int> triangles = new List<int>() {0,1,2,2,3,4,4,0,2};

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
    }
}
