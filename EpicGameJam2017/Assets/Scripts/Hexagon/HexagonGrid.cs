using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class HexagonGrid : MonoBehaviour
{
    public int Radius;

    //public float HexCellOuterRadius = 3f; //TODO configurable via editor?
    public HexagonCell HexagonCell;

    private GameObject hexcellGameObject;

    void Awake()
    {
        hexcellGameObject = HexagonCell.gameObject;
    }

    // Use this for initialization
    void Start()
    {
        CreateGrid(Radius);
    }

    void CreateGrid(int radius)
    {
        var cellwidth = HexagonCell.innerRadius * 2 / 2; //this should be 2*r ?!
        var cellheight = HexagonCell.outerRadius * 1.5f / 2;

        var diam = radius * 2;

        List<GameObject> hexagons = new List<GameObject>(diam * diam);
        for (var row = 0; row < diam; row++)
        {
            for (var col = 0; col < diam; col++)
            {
                //offset from center + pos
                var x = -Radius * cellwidth + cellwidth/2 + col * cellwidth + (row % 2) * cellwidth / 2f;
                var y = -Radius * cellheight  + row * cellheight;

                //make sure cell is fully within circle (0,Radius)
                //var xx = Mathf.Abs(x) + HexagonCell.innerRadius;
                //var yy = Mathf.Abs(y) + HexagonCell.outerRadius;
                //if (xx*xx+yy*yy > Radius * Radius) continue;
                if(x*x +y*y > Radius*Radius) continue; //center not within circle

                var hexcell = Instantiate(hexcellGameObject, transform);
                hexcell.GetComponent<HexagonCell>().SetPosition(x, y);
                hexcell.GetComponent<HexagonCell>().SetGridPosition(col, row);
                hexagons.Add(hexcell);
            }
        }
        Assert.IsTrue(hexagons.Exists(g => g.transform.position == Vector3.zero));
    }
}