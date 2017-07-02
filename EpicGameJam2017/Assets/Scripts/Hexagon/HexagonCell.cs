using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine.Assertions;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class HexagonCell : MonoBehaviour
{
    public Players? Player = null;
    public bool IsCheesed = false;
    public bool Outline = true; //TODO
    public int col;
    public int row;

    private Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private PolygonCollider2D collider2D;
    private HexagonGrid grid;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        collider2D = GetComponent<PolygonCollider2D>();
        grid = GetComponentInParent<HexagonGrid>();
    }


    // Use this for initialization
    void Start()
    {
        Redraw();
    }

    private void Update()
    {
        var color = Constants.DefaultHexagonColor;

        if(IsCheesed)
        {
            color = Constants.CheeseColor;
        }
        else if(Player.HasValue)
        {
            color = Constants.PlayerColors[Player.Value];
        }

        GetComponent<MeshRenderer>().material.color = color;
    }

    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps)
        {
            var shell = ps.gameObject.GetComponentInParent<Shell>();

            if(shell.ShellType == ShellType.Tomato)
            {
                Player = ps.gameObject.GetComponentInParent<Shell>().Player;
                IsCheesed = false;
            }
            else if(shell.ShellType == ShellType.Cheese)
            {
                Player = null;
                IsCheesed = true;
            }
            //print("shot by Player" + Player);
        }
    }


    public void Redraw()
    {
        vertices.Clear();
        triangles.Clear();
        collider2D.pathCount = 0;
        collider2D.CreatePrimitive(6, new Vector2(grid.HexCellOuterRadius, grid.HexCellOuterRadius));
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
        var center = Vector2.zero;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + grid.corners[i],
                center + grid.corners[i + 1]
            );
            //collider2D.SetPath(i,new []{corners[i],corners[i+1]});
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