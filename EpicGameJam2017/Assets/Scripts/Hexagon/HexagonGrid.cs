using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Assertions;


public class HexagonGrid : MonoBehaviour
{
    public int Radius;

    public float HexCellOuterRadius = 3f; //TODO configurable via editor?
    public const float Outer2inner = 0.866025404f; //srt(3)/2
    public float HexCellInnerRadius { get; private set; }

    public HexagonCell HexagonCell;

    private List<GameObject> hexagons = new List<GameObject>();
    private GameObject hexcellGameObject;
    private bool mustRedraw = false;

    public Vector2[] corners { get; private set; }

    void OnValidate()
    {
        mustRedraw = true;
    }

    void Awake()
    {
        hexcellGameObject = HexagonCell.gameObject;
        UpdateHexagonCellSize();
        CreateGrid(Radius);
    }

    // Use this for initialization
    void Start()
    {
        mustRedraw = true;
    }

    void Update()
    {
        if (mustRedraw)
        {
            UpdateHexagonCellSize();
            CreateGrid(Radius);
            mustRedraw = false;
        }
    }

    void CreateGrid(int radius)
    {
        hexagons.ForEach(Destroy);
        hexagons.Clear();

        var cellwidth = HexCellInnerRadius * 2;
        var cellheight = HexCellOuterRadius * 1.5f;

        var y = 0f;
        var row = 0;
        while (y < radius)
        {

            var col = row % 2 == 1 ? 1 : 0;
            var x = row % 2 == 1 ? cellwidth / 2 : 0f;
            while (x < radius)
            {

                if (x * x + y * y > radius * radius) break;

                mkHexCell(x, y, row, col);

                //floating point compare is fine because var is initialized to 0
                if (x != 0) mkHexCell(-x, y, row, -col);
                if (y != 0) mkHexCell(x, -y, -row, col);
                if (x != 0 && y != 0) mkHexCell(-x, -y, -row, -col);

                x += cellwidth;
                col++;
            }
            y += cellheight;
            row++;

        }
    }

    private void mkHexCell(float x, float y, int row, int col)
    {
        var hexcell = Instantiate(hexcellGameObject, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
        hexcell.GetComponent<HexagonCell>().SetGridPosition(col, row);
        hexagons.Add(hexcell);
    }

    private void UpdateHexagonCellSize()
    {
        HexCellInnerRadius = HexCellOuterRadius * Outer2inner;
        corners = new[]
        {
            new Vector2(0, HexCellOuterRadius),
            new Vector2(HexCellInnerRadius, HexCellOuterRadius * 0.5f),
            new Vector2(HexCellInnerRadius, HexCellOuterRadius * -0.5f),
            new Vector2(0, -HexCellOuterRadius),
            new Vector2(-HexCellInnerRadius, HexCellOuterRadius * -0.5f),
            new Vector2(-HexCellInnerRadius, HexCellOuterRadius * 0.5f),
            //repeat first corner for lazy bum drawing
            new Vector2(0, HexCellOuterRadius),
        };
    }
}