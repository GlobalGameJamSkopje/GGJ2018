using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain
{


    private int _length = 12;
    private int _width = 6;

    public PlainTile[,] Tiles;


    public Plain()
    {
        Tiles = new PlainTile[_length, _width];

        for (int i = 0; i < _length; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Tiles[i,j] = new PlainTile();
            }
        }
    }
}
