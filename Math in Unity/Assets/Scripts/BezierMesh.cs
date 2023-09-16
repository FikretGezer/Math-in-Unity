using System.Collections.Generic;
using UnityEngine;

public class BezierMesh : MonoBehaviour
{
    Mesh mesh;

    [Range(0, 1)] public float tTest;

    public Vector3 profilePtA = new Vector3(0,0,-1);
    public Vector3 profilePtB = new Vector3(0,0,+1);

    public int segmentCount;
    public int VertexCount => 2 * (segmentCount + 1);

    private void OnValidate() {
        segmentCount = Mathf.Max(1, segmentCount);
    }

    void OnDrawGizmos()
    {
        if(mesh == null)
        {
            mesh = new Mesh();
            mesh.MarkDynamic();
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        Gizmos.matrix = transform.localToWorldMatrix;

        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < segmentCount + 1; i++)
        {
            float t = i / (float)segmentCount;
            Matrix4x4 mtx = GetPoint(i);
            vertices.Add(mtx.MultiplyPoint3x4(profilePtA));
            vertices.Add(mtx.MultiplyPoint3x4(profilePtB));
            Vector3 normal = mtx.GetColumn(1);
            normals.Add(normal);
            normals.Add(normal);
        }

        for (int s = 0; s < segmentCount; s++)
        {
            int root = s * 2;
            int neighbor = root + 1;
            int next = root + 2;
            int nextNeighbor = root + 3;

            triangles.Add(root);
            triangles.Add(neighbor);
            triangles.Add(nextNeighbor);

            triangles.Add(nextNeighbor);
            triangles.Add(next);
            triangles.Add(root);
        }

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);
    }   

    Vector3 p0 => transform.GetChild(0).localPosition;
    Vector3 p1 => transform.GetChild(1).localPosition;
    Vector3 p2 => transform.GetChild(2).localPosition;
    Vector3 p3 => transform.GetChild(3).localPosition;

    Matrix4x4 GetPoint(float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 origin = Vector3.Lerp(d, e, t);
        Vector3 tangent = (e - d).normalized;
        Vector3 normal = Vector3.zero;
        normal.x = -tangent.y;
        normal.y = tangent.x;

        return new Matrix4x4(
            tangent,
            normal,
            Vector3.forward,
            Vec4(origin, 1)
        );

        Vector4 Vec4(Vector3 v3, float w = 0) => new Vector4(v3.x, v3.y, v3.z, w);
    }
}
