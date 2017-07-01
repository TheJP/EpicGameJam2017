using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexagonGrid : MonoBehaviour
{

    public int Height = 10;
    public int Width = 10;
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
        CreateGrid(Width, Height);
    }

    void CreateGrid(int width, int height)
    {
        var cellwidth = HexagonCell.innerRadius * 2 / 2; //this should be 2*r ?!
        var cellheight = HexagonCell.outerRadius * 1.5f / 2;

        var ncols = Mathf.RoundToInt(width / cellwidth);
        var nrows = Mathf.RoundToInt(height / cellheight);

        //center is at 0/0 => tiles start at  - ncols / 2 * cellwidth

        var offsetX = -ncols * cellwidth / 2;
        var offsetY = -nrows * cellheight / 2;


        List<GameObject> hexagons = new List<GameObject>(ncols * nrows);
        for (var i = 0; i < nrows; i++)
        {
            for (var j = 0; j < ncols; j++)
            {
                var x = offsetX + j * cellwidth + (i % 2) * cellwidth / 2f;
                var y = offsetY + i * cellheight;

                //place hexagon at x,y
                var hexcell = Instantiate(hexcellGameObject, transform);
                hexcell.GetComponent<HexagonCell>().setPosition(x, y);
                hexagons.Add(hexcell);
            }
        }


    }
}
