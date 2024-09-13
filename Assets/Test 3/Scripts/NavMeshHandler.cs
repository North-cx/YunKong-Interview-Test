using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHandler: MonoBehaviour
{
    public void PrintNavMeshData()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        Vector3[] vertices = triangulation.vertices;
        int[] indices = triangulation.indices;

        // ���NavMesh����
        Debug.Log("Vertices: " + vertices.Length);
        Debug.Log("Indices: " + indices.Length);
    }
}
