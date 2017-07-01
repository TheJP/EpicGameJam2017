using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine.Assertions;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class HexagonCell : MonoBehaviour
{
    public const float outer2inner = 0.866025404f; //srt(3)/2
    public const float outerRadius = 3f;
    public const float innerRadius = outerRadius * outer2inner;

    public Color Color = Color.blue;
    public bool Outline = true;

    private Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();

    private static readonly Vector3[] corners =
    {
        new Vector3(0, outerRadius),
        new Vector3(innerRadius, outerRadius * 0.5f),
        new Vector3(innerRadius, outerRadius * -0.5f),
        new Vector3(0, -outerRadius),
        new Vector3(-innerRadius, outerRadius * -0.5f),
        new Vector3(-innerRadius, outerRadius * 0.5f),
        //repeat first corner for lazy bum drawing
        new Vector3(0, outerRadius),
    };

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
    }


    // Use this for initialization
    void Start()
    {
        Triangulate();
        GetComponent<MeshRenderer>().material.color = Color;
        //TODO
        if (Outline)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setPosition(float x, float y)
    {
        transform.position = new Vector3(x, y);
    }

    private void Triangulate()
    {
        //triangulate hexagon
        var center = transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + corners[i],
                center + corners[i + 1]
            );
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}