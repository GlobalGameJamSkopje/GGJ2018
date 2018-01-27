using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    public GameObject Plain, Mine, Mountain;

    private World World;

    void Start()
    {
        World = new World();

        for (int i = 0; i < World.Length; i++)
        {
            for (int j = 0; j < World.Width; j++)
            {
                GameObject tilePlain;
                if (World.PlayerOne.Tiles[i, j].TileType == TileType.Mountain)
                {
                    tilePlain = Instantiate(Plain, new Vector3(i, 0, j), Quaternion.identity);
                    GameObject tileMountain = Instantiate(Mountain, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (World.PlayerOne.Tiles[i, j].TileType == TileType.Mine)
                {
                    tilePlain = Instantiate(Mine, new Vector3(i, 0, j), Quaternion.identity);

                }
                else
                {
                    tilePlain = Instantiate(Plain, new Vector3(i, 0, j), Quaternion.identity);
                }
                tilePlain.name = string.Format("P1[{0}, {1}]", i, j);
            }
        }

        for (int i = 0; i < World.Length; i++)
        {
            for (int j = World.Width-1; j >= 0; j--)
            {
                GameObject tilePlain;
                if (World.PlayerTwo.Tiles[i, j].TileType == TileType.Mountain)
                {
                    tilePlain = Instantiate(Plain, new Vector3(i, 0, 12-j), Quaternion.identity);
                    GameObject tileMountain = Instantiate(Mountain, new Vector3(i, 0, 12 - j), Quaternion.identity);
                }
                else if (World.PlayerTwo.Tiles[i, j].TileType == TileType.Mine)
                {
                    tilePlain = Instantiate(Mine, new Vector3(i, 0, 12 - j), Quaternion.identity);
                }
                else
                {
                    tilePlain = Instantiate(Plain, new Vector3(i, 0, 12 - j), Quaternion.identity);
                }
                tilePlain.name = string.Format("P2[{0}, {1}]", i, j);
            }
        }

    }
}
