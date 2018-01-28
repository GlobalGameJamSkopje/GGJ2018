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
    public Sprite[] TowerSpritesP1;
    public Sprite[] PlainSpritesP2;
    public Sprite[] MountainSpritesP2;
    public Sprite[] MountainHolePlainSpritesP2;
    public Sprite[] HoleSpritesP2;
    public Sprite[] TowerSpritesP2;
    public Sprite red, green, blue;
    //public Sprite towerSprite;

    public ViewTile[] ViewTilesPlayerOne;
    public ViewTile[] ViewTilesPlayerTwo;

    public ViewTile[,] ViewTilesMultiArrayPlayerOne = new ViewTile[6, 5];
    public ViewTile[,] ViewTilesMultiArrayPlayerTwo = new ViewTile[6, 5];

    [HideInInspector]
    public World World;

    public void GenerateWorld(World world)
    {
        World = world;

        FillViewTilesMultiArray();

        GenerateMap();
    }

    public void MakeMineP1(ViewTile tile)
    {
        MakeMineP1((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }
    public void MakePlainP1(ViewTile tile)
    {
        MakePlainP1((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }
    public void MakeMountainP1(ViewTile tile)
    {
        MakeMountainP1((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }

    public void MakeTowerP1(ViewTile tile)
    {
        switch (World.P1Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y].ResourceType)
        {
            case ResourceType.Red:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP1[0];
                break;
            case ResourceType.Green:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP1[1];
                break;
            case ResourceType.Blue:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP1[2];
                break;
        }
    }

    public void MakeMineP2(ViewTile tile)
    {
        MakeMineP2((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }
    public void MakePlainP2(ViewTile tile)
    {
        MakePlainP2((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }
    public void MakeMountainP2(ViewTile tile)
    {
        MakeMountainP2((int)tile.worldPosition.x, (int)tile.worldPosition.y);
    }

    public void MakeTowerP2(ViewTile tile)
    {
        switch (World.P2Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y].ResourceType)
        {
            case ResourceType.Red:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP2[0];
                break;
            case ResourceType.Green:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP2[1];
                break;
            case ResourceType.Blue:
                tile.TowerSpriteRenderer.sprite = TowerSpritesP2[2];
                break;
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
                        ViewTilesMultiArrayPlayerOne[i, j] = viewTile;
                        break;
                    }

                }
                foreach (var viewTile in ViewTilesPlayerTwo)
                {
                    if (viewTile.worldPosition.x == i && viewTile.worldPosition.y == j)
                    {
                        ViewTilesMultiArrayPlayerTwo[i, j] = viewTile;
                        break;
                    }

                }
            }
        }

    }

    private void GenerateMap()
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

    private void MakeMineP1(int i, int j)
    {
        ViewTilesMultiArrayPlayerOne[i, j]
            .ChangePlainSprite(MountainHolePlainSpritesP1[UnityEngine.Random.Range(0, MountainHolePlainSpritesP1.Length)]);
        ViewTilesMultiArrayPlayerOne[i, j]
            .ChangeHoleSprite(HoleSpritesP1[UnityEngine.Random.Range(0, HoleSpritesP1.Length)]);
    }
    private void MakePlainP1(int i, int j)
    {
        ViewTilesMultiArrayPlayerOne[i, j]
            .ChangePlainSprite(PlainSpritesP1[UnityEngine.Random.Range(0, PlainSpritesP1.Length)]);
    }
    private void MakeMountainP1(int i, int j)
    {
        ViewTilesMultiArrayPlayerOne[i, j]
            .ChangePlainSprite(MountainHolePlainSpritesP1[UnityEngine.Random.Range(0, MountainHolePlainSpritesP1.Length)]);
        ViewTilesMultiArrayPlayerOne[i, j]
            .ChangeMountainSprite(MountainSpritesP1[UnityEngine.Random.Range(0, MountainSpritesP1.Length)]);
    }

    private void MakeMineP2(int i, int j)
    {
        ViewTilesMultiArrayPlayerTwo[i, j]
            .ChangePlainSprite(MountainHolePlainSpritesP2[UnityEngine.Random.Range(0, MountainHolePlainSpritesP2.Length)]);
        ViewTilesMultiArrayPlayerTwo[i, j]
            .ChangeHoleSprite(HoleSpritesP2[UnityEngine.Random.Range(0, HoleSpritesP2.Length)]);
    }
    private void MakePlainP2(int i, int j)
    {
        ViewTilesMultiArrayPlayerTwo[i, j]
            .ChangePlainSprite(PlainSpritesP2[UnityEngine.Random.Range(0, PlainSpritesP2.Length)]);
    }
    private void MakeMountainP2(int i, int j)
    {
        ViewTilesMultiArrayPlayerTwo[i, j]
            .ChangePlainSprite(MountainHolePlainSpritesP2[UnityEngine.Random.Range(0, MountainHolePlainSpritesP2.Length)]);
        ViewTilesMultiArrayPlayerTwo[i, j]
            .ChangeMountainSprite(MountainSpritesP2[UnityEngine.Random.Range(0, MountainSpritesP2.Length)]);
    }

    private void GenerateMapP1(int i, int j)
    {
        if (World.P1Plain.Tiles[i, j].ResourceType == ResourceType.Red) ViewTilesMultiArrayPlayerOne[i, j].SetResourceType(red);
        else if (World.P1Plain.Tiles[i, j].ResourceType == ResourceType.Green) ViewTilesMultiArrayPlayerOne[i, j].SetResourceType(green);
        else ViewTilesMultiArrayPlayerOne[i, j].SetResourceType(blue);

        MakePlainP1(i, j);

        if (World.P1Plain.Tiles[i, j].TileType == TileType.Mountain) MakeMountainP1(i, j);
        else if (World.P1Plain.Tiles[i, j].TileType == TileType.Mine) MakeMineP1(i, j);
    }
    private void GenerateMapP2(int i, int j)
    {
        if (World.P2Plain.Tiles[i, j].ResourceType == ResourceType.Red) ViewTilesMultiArrayPlayerTwo[i, j].SetResourceType(red);
        else if (World.P2Plain.Tiles[i, j].ResourceType == ResourceType.Green) ViewTilesMultiArrayPlayerTwo[i, j].SetResourceType(green);
        else ViewTilesMultiArrayPlayerTwo[i, j].SetResourceType(blue);

        MakePlainP2(i, j);

        if (World.P2Plain.Tiles[i, j].TileType == TileType.Mountain) MakeMountainP2(i, j);
        else if (World.P2Plain.Tiles[i, j].TileType == TileType.Mine) MakeMineP2(i, j);
    }
}
