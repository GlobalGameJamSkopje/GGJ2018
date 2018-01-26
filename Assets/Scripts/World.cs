using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class World
{
    private int _lenght = 6;
    private int _widht = 5;

    public Plain PlayerOne { get; private set; }
    public Plain PlayerTwo { get; private set; }

    public int Red { get; private set; }
    public int Green { get; private set; }
    public int Blue { get; private set; }

    private List<ResourceType> resourceTypesList;

    public World()
    {
        PlayerOne = new Plain(_lenght, _widht);
        PlayerTwo = new Plain(_lenght, _widht);

        MaxResourceTypes();
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

    private void MaxResourceTypes()
    {
        resourceTypesList = new List<ResourceType>();

        var maxResources = _lenght * _widht;
        Red = UnityEngine.Random.Range(8, 14);
        Green = UnityEngine.Random.Range(8, maxResources - Red - 8);
        Blue = maxResources - Red - Green;

        for (int i = 0; i < Red; i++)
        {
            resourceTypesList.Add(ResourceType.Red);
        }

        for (int i = 0; i < Green; i++)
        {
            resourceTypesList.Add(ResourceType.Green);
        }

        for (int i = 0; i < Blue; i++)
        {
            resourceTypesList.Add(ResourceType.Blue);
        }
    }

    private ResourceType RandomResourceType()
    {
        var index = UnityEngine.Random.Range(0, resourceTypesList.Count);
        var tempResourceType = resourceTypesList[index];
        resourceTypesList.RemoveAt(index);
        return tempResourceType;
    }

    private TileType RandomTileType()
    {
        var v = Enum.GetValues(typeof(TileType));
        return (TileType)v.GetValue(new Random().Next(v.Length));
    }
}
