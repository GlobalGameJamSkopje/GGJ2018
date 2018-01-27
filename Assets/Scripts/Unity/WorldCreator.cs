using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    public Sprite[] PlainSprites;
    public Sprite[] MountainSprites;
    public Sprite[] HoleSprites;
    public Sprite redBelow, redAbove, greenBelow, greenAbove, blueBelow, blueAbove;
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
        if (World.PlayerOne.Tiles[i, j].ResourceType == ResourceType.Red)
        {
            _viewTilesMultiArrayPlayerOne[i, j].SetResourceType(redAbove, redBelow);
        }
        else if (World.PlayerOne.Tiles[i, j].ResourceType == ResourceType.Green)
        {
            //_viewTilesMultiArrayPlayerOne[i, j].SetResourceType(greenAbove, greenBelow);
            _viewTilesMultiArrayPlayerOne[i, j].aboveCrystalSpriteRenderer.color = Color.green;
            _viewTilesMultiArrayPlayerOne[i, j].belowCrystalSpriteRenderer.color = Color.green;
        }
        else
        {
            //_viewTilesMultiArrayPlayerOne[i, j].SetResourceType(blueAbove, blueBelow);
            _viewTilesMultiArrayPlayerOne[i, j].aboveCrystalSpriteRenderer.color = Color.blue;
            _viewTilesMultiArrayPlayerOne[i, j].belowCrystalSpriteRenderer.color = Color.blue;
        }

        _viewTilesMultiArrayPlayerOne[i, j]
            .ChangePlainSprite(PlainSprites[UnityEngine.Random.Range(0, PlainSprites.Length)]);

        if (World.PlayerOne.Tiles[i, j].TileType == TileType.Mountain)
        {
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangeMountainSprite(MountainSprites[UnityEngine.Random.Range(0, MountainSprites.Length)]);
        }
        else if (World.PlayerOne.Tiles[i, j].TileType == TileType.Mine)
        {
            _viewTilesMultiArrayPlayerOne[i, j]
                .ChangeHoleSprite(HoleSprites[UnityEngine.Random.Range(0, HoleSprites.Length)]);
        }
    }
    private void GenerateMapP2(int i, int j)
    {
        if (World.PlayerTwo.Tiles[i, j].ResourceType == ResourceType.Red)
        {
            _viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(redAbove, redBelow);
        }
        else if (World.PlayerTwo.Tiles[i, j].ResourceType == ResourceType.Green)
        {
            //_viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(greenAbove, greenBelow);
            _viewTilesMultiArrayPlayerTwo[i, j].aboveCrystalSpriteRenderer.color = Color.green;
            _viewTilesMultiArrayPlayerTwo[i, j].belowCrystalSpriteRenderer.color = Color.green;
        }
        else
        {
            //_viewTilesMultiArrayPlayerTwo[i, j].SetResourceType(blueAbove, blueBelow);
            _viewTilesMultiArrayPlayerTwo[i, j].aboveCrystalSpriteRenderer.color = Color.blue;
            _viewTilesMultiArrayPlayerTwo[i, j].belowCrystalSpriteRenderer.color = Color.blue;
        }

        _viewTilesMultiArrayPlayerTwo[i, j]
            .ChangePlainSprite(PlainSprites[UnityEngine.Random.Range(0, PlainSprites.Length)]);

        if (World.PlayerTwo.Tiles[i, j].TileType == TileType.Mountain)
        {
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangeMountainSprite(MountainSprites[UnityEngine.Random.Range(0, MountainSprites.Length)]);
        }
        else if (World.PlayerTwo.Tiles[i, j].TileType == TileType.Mine)
        {
            _viewTilesMultiArrayPlayerTwo[i, j]
                .ChangeHoleSprite(HoleSprites[UnityEngine.Random.Range(0, HoleSprites.Length)]);
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
