using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine.Assertions;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class HexagonCell : MonoBehaviour
{
    public Players Player = Players.A;
    public bool Outline = true; //TODO

    private static readonly Dictionary<Players, Color> PlayerColors = new Dictionary<Players, Color>()
    {
        {Players.A, Color.yellow},
        {Players.B, Color.blue}
    };

    private Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();

    private int col;
    private int row;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
    }


    // Use this for initialization
    void Start()
    {
        Redraw();
        GetComponent<MeshRenderer>().material.color = PlayerColors[Player];
        //TODO
        if (Outline)
        {

        }
    }

    public void Redraw()
    {
        vertices.Clear();
        triangles.Clear();
        Triangulate();
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y);
    }

    public void SetGridPosition(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    private void Triangulate()
    {
        //triangulate hexagon
        var center = transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + GetComponentInParent<HexagonGrid>().corners[i],
                center + GetComponentInParent<HexagonGrid>().corners[i + 1]
            );
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
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