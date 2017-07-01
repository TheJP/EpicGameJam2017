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
    private float hexCellInnerRadius;

    public HexagonCell HexagonCell;

    private List<GameObject> hexagons = new List<GameObject>();
    private GameObject hexcellGameObject;
    private bool mustRedraw = false;

    public Vector3[] corners
    {
        get; private set;
    }

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

        var cellwidth = hexCellInnerRadius * 2;
        var cellheight = HexCellOuterRadius * 1.5f;

        var diam = radius * 2;

        var nrows = Mathf.RoundToInt((diam + cellheight / 2) / cellheight);
        var ncols = Mathf.RoundToInt((diam + cellwidth / 2) / cellwidth);



        if ((ncols / 2) * cellwidth > Radius) ncols -= 2;
        if ((nrows / 2) * cellheight > Radius) nrows -= 2;



        var offsetX = -ncols * cellwidth / 2f - (1 - ncols % 2) * cellwidth / 2;
        var offsetY = -nrows * cellheight / 2f + cellheight / 2;

        print("col,rows = (" + ncols + "," + nrows + ")");

        //for (var row = 0; row < nrows; row++)
        //{
        //    for (var col = 0; col < ncols; col++)
        //    {

        //        //every second row is additionally offset by half to the right
        //        var x = offsetX + col * cellwidth + (row % 2) * cellwidth / 2f;
        //        var y = offsetY + row * cellheight;


        //        //make sure cell is fully within circle (0,Radius)
        //        //var xx = Mathf.Abs(x) + hexCellInnerRadius;
        //        //var yy = Mathf.Abs(y) + HexCellOuterRadius;
        //        //if (xx*xx+yy*yy >= Radius * Radius) continue;
        //        if (x * x + y * y > Radius * Radius) continue; //center not within circle

        //        var hexcell = Instantiate(hexcellGameObject, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
        //        hexagons.Add(hexcell);

        //    }
        //}

        var dist = 0;
        var y = 0f;
        var row = 0;
        while (y < radius)
        {

            var col = row % 2 == 1 ? 1 : 0;
            var x = row % 2 == 1 ? cellwidth / 2 : 0f;
            while (x < radius)
            {

                if(x*x + y * y > radius * radius) break;
                
                mkHexCell(x,y,row,col);

                //floating point compare is fine because var is initialized to 0
                if (x != 0) mkHexCell(-x,y,row,-col);
                if (y != 0)mkHexCell(x,-y,-row,col);
                if(x!=0 && y!=0) mkHexCell(-x,-y,-row,-col);

                x += cellwidth;
                col++;
            }
            y += cellheight;
            row++;

        }
        Assert.IsTrue(hexagons.Exists(g => g.transform.localPosition == Vector3.zero));
    }

    private void mkHexCell(float x, float y, int row, int col)
    {
        var hexcell = Instantiate(hexcellGameObject, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
        hexcell.GetComponent<HexagonCell>().SetGridPosition(col,row);
        hexagons.Add(hexcell);
    }

    private void UpdateHexagonCellSize()
    {
        hexCellInnerRadius = HexCellOuterRadius * Outer2inner;
        corners = new[]
        {
            new Vector3(0, HexCellOuterRadius),
            new Vector3(hexCellInnerRadius, HexCellOuterRadius * 0.5f),
            new Vector3(hexCellInnerRadius, HexCellOuterRadius * -0.5f),
            new Vector3(0, -HexCellOuterRadius),
            new Vector3(-hexCellInnerRadius, HexCellOuterRadius * -0.5f),
            new Vector3(-hexCellInnerRadius, HexCellOuterRadius * 0.5f),
            //repeat first corner for lazy bum drawing
            new Vector3(0, HexCellOuterRadius),
        };
    }
}