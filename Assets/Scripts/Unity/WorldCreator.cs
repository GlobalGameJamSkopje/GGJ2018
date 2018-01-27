using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    public Sprite[] PlainSpritesP1;
    public Sprite[] MountainSpritesP1;
    public Sprite[] MountainHolePlainSpritesP1;
    public Sprite[] HoleSpritesP1;
    public Sprite[] PlainSpritesP2;
    public Sprite[] MountainSpritesP2;
    public Sprite[] MountainHolePlainSpritesP2;
    public Sprite[] HoleSpritesP2;
    public Sprite red, green, blue;
    //public Sprite towerSprite;

    public ViewTile[] ViewTilesPlayerOne;
    public ViewTile[] ViewTilesPlayerTwo;

    private ViewTile[,] _viewTilesMultiArrayPlayerOne = new ViewTile[6, 5];
    private ViewTile[,] _viewTilesMultiArrayPlayerTwo = new ViewTile[6, 5];

    [HideInInspector]
    public World World;

    void Start()
    {
        World = new World();

        FillViewTilesMultiArray();

        GenerateMap();
    }

    void GenerateMap()
    {
        for (int i = 0; i < World.Length; i++)
        {
            for (int j = 0; j < World.Width; j++)
            {
                GenerateMapP1(i, j);
                GenerateMapP2(i, j);
            }
        }
    }

    private void GenerateMapP1(int i, int j)
    {
        if (World.P1Plain.Tiles[i, j].ResourceType == ResourceType.Red)
        {
            _viewTilesMultiArrayPlayerOne[i, j].SetResourceType(red);
        }
        else if (World.P1Plain.Tiles[i, j].ResourceType == ResourceType.Green)
        {
            _viewTilesMultiArrayPlayerOne[i, j].SetResourceType(green);
        }
        else
        {
            _viewTilesMultiArrayPlayerOne[i, j].SetResourceType(blue);
        }

        _viewTilesMultiArrayPlayerOne[i, j]
            .ChangePlainSprite(PlainSpritesP1[UnityEngine.Random.Range(0, PlainSpritesP1.Length)]);

        if (World.P1Plain.Tiles[i, j].TileType == TileType.Mountain)
        {
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangePlainSprite(MountainHolePlainSpritesP1[UnityEngine.Random.Range(0, MountainHolePlainSpritesP1.Length)]);
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangeMountainSprite(MountainSpritesP1[UnityEngine.Random.Range(0, MountainSpritesP1.Length)]);
        }
        else if (World.P1Plain.Tiles[i, j].TileType == TileType.Mine)
        {
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangePlainSprite(MountainHolePlainSpritesP1[UnityEngine.Random.Range(0, MountainHolePlainSpritesP1.Length)]);
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangeHoleSprite(HoleSpritesP1[UnityEngine.Random.Range(0, HoleSpritesP1.Length)]);
        }
    }
    private void GenerateMapP2(int i, int j)
    {
        if (World.P2Plain.Tiles[i, j].ResourceType == ResourceType.Red)
        {
            _viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(red);
        }
        else if (World.P2Plain.Tiles[i, j].ResourceType == ResourceType.Green)
        {
            _viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(green);
        }
        else
        {
            _viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(blue);
        }

        _viewTilesMultiArrayPlayerTwo[i, j]
            .ChangePlainSprite(PlainSpritesP2[UnityEngine.Random.Range(0, PlainSpritesP2.Length)]);

        if (World.P2Plain.Tiles[i, j].TileType == TileType.Mountain)
        {
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangePlainSprite(MountainHolePlainSpritesP2[UnityEngine.Random.Range(0, MountainHolePlainSpritesP2.Length)]);
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangeMountainSprite(MountainSpritesP2[UnityEngine.Random.Range(0, MountainSpritesP2.Length)]);
        }
        else if (World.P2Plain.Tiles[i, j].TileType == TileType.Mine)
        {
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangePlainSprite(MountainHolePlainSpritesP2[UnityEngine.Random.Range(0, MountainHolePlainSpritesP2.Length)]);
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangeHoleSprite(HoleSpritesP2[UnityEngine.Random.Range(0, HoleSpritesP2.Length)]);
        }
    }

    public void FillViewTilesMultiArray()
    {
        for (int i = 0; i < World.Length; i++)
        {
            for (int j = 0; j < World.Width; j++)
            {
                foreach (var viewTile in ViewTilesPlayerOne)
                {
                    if (viewTile.worldPosition.x == i && viewTile.worldPosition.y == j)
                    {
                        _viewTilesMultiArrayPlayerOne[i, j] = viewTile;
                        break;
                    }

                }
                foreach (var viewTile in ViewTilesPlayerTwo)
                {
                    if (viewTile.worldPosition.x == i && viewTile.worldPosition.y == j)
                    {
                        _viewTilesMultiArrayPlayerTwo[i, j] = viewTile;
                        break;
                    }

                }
            }
        }

    }
}
