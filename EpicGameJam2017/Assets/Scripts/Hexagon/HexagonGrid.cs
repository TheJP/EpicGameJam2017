using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexagonGrid : MonoBehaviour
{

    public int Height = 10;
    public int Width = 10;
    public HexagonCell HexagonCell;
    private GameObject hexcellGameObject;

    void Awake()
    {
        hexcellGameObject = HexagonCell.gameObject;
    }

    // Use this for initialization
    void Start () {
        CreateGrid(Width,Height);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    void CreateGrid(int width, int height)
    {
        var cellwidth = HexagonCell.outerRadius;
        var cellheight = HexagonCell.innerRadius * 1.5f / 2;

        var ncols = (int)(width / cellwidth)+1;
        var nrows = (int)(height / cellheight)+1;

        //center is at 0/0 => tiles start at  - ncols / 2 * cellwidth
  
        var offsetX = -ncols * cellwidth / 2f + cellwidth/2f;
        var offsetY = -nrows / 2f * cellheight + cellheight/2f; 


        List<GameObject> hexagons = new List<GameObject>(ncols*nrows);
        for (var i = 0; i < nrows; i++)
        {
            for (var j = 0; j < ncols; j++)
            {
                var x = offsetX + i * cellwidth + (j%2)*cellwidth/2f;
                var y = offsetY + j * cellheight;
            
                //place hexagon at x,y
                var hexcell = Instantiate(hexcellGameObject);
                hexcell.GetComponent<HexagonCell>().setPosition(x,y);
                hexagons.Add(hexcell);
            }
        }
        

    }
}
