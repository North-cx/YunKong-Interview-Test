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

        // Êä³öNavMeshÊý¾Ý
        Debug.Log("Vertices: " + vertices.Length);
        Debug.Log("Indices: " + indices.Length);
    }
}
