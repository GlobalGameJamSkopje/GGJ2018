using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain
{
    public PlainTile[,] Tiles;

    public Plain(int length , int width)
    {
        Tiles = new PlainTile[length, width];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Tiles[i,j] = new PlainTile();
            }
        }
    }
}
