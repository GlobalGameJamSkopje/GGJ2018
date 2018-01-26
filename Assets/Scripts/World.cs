using System;
using System.Collections;
using System.Collections.Generic;

public class World
{
    private int _lenght = 9;
    private int _widht = 4;

    public Plain PlayerOne { get; private set; }
    public Plain PlayerTwo { get; private set; }

    public World()
    {
        PlayerOne = new Plain(_lenght, _widht);
        PlayerTwo = new Plain(_lenght, _widht);

        GenerateResourceMap();
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        for (int i = 0; i < _lenght; i++)
        {
            for (int j = 0; j < _widht; j++)
            {
                var tileTypeP1 = RandomTileType();
                var tileTypeP2 = RandomTileType();

                if (tileTypeP1 == TileType.Mine)
                {
                    tileTypeP2 = TileType.Mountain;
                }
                if (tileTypeP2 == TileType.Mine)
                {
                    tileTypeP1 = TileType.Mountain;
                }

                PlayerOne.Tiles[i, j].SetTileType(tileTypeP1);
                PlayerTwo.Tiles[i, j].SetTileType(tileTypeP2);
            }
        }
    }

    private void GenerateResourceMap()
    {
        for (int i = 0; i < _lenght; i++)
        {
            for (int j = 0; j < _widht; j++)
            {
                var resourceType = RandomResourceType();

                PlayerOne.Tiles[i, j].SetRecourceType(resourceType);
                PlayerTwo.Tiles[i, j].SetRecourceType(resourceType);
            }
        }
    }

    private ResourceType RandomResourceType()
    {
        var v = Enum.GetValues(typeof(ResourceType));
        return (ResourceType)v.GetValue(new Random().Next(v.Length));
    }

    private TileType RandomTileType()
    {
        var v = Enum.GetValues(typeof(TileType));
        return (TileType)v.GetValue(new Random().Next(v.Length));
    }
}
