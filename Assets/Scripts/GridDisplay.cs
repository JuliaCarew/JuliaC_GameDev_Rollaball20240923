using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    private int Height = 3;
    private int Weight = 3;
    private float GridSpaceSize = 5f;

    private GameObject[,] gameGrid;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    private void CreateGrid()
    {
        
    }
}
