using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class World
{
    private const int Lenght = 6;
    private const int Widht = 5;
    private const int MaxResources = Lenght * Widht;

    public Plain PlayerOne { get; private set; }
    public Plain PlayerTwo { get; private set; }

    private readonly IQuestCreator _questCreator;

    public World()
    {
        _questCreator = new QuestCreator();

        PlayerOne = new Plain(Lenght, Widht);
        PlayerTwo = new Plain(Lenght, Widht);

        var resourceTypes = GenerateResourceTypes();

        _questCreator.CreateQuests(
            resourceTypes.Count(x => x == ResourceType.Red),
            resourceTypes.Count(x => x == ResourceType.Green),
            resourceTypes.Count(x => x == ResourceType.Blue));

        GenerateResourceMap(resourceTypes);
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        for (var i = 0; i < Lenght; i++)
        {
            for (var j = 0; j < Widht; j++)
            {
                var tileTypeP1 = RandomTileType();
                var tileTypeP2 = RandomTileType();

                if (tileTypeP1 == TileType.Mine)
                    tileTypeP2 = TileType.Mountain;

                if (tileTypeP2 == TileType.Mine)
                    tileTypeP1 = TileType.Mountain;

                PlayerOne.Tiles[i, j].SetTileType(tileTypeP1);
                PlayerTwo.Tiles[i, j].SetTileType(tileTypeP2);
            }
        }
    }

    private void GenerateResourceMap(List<ResourceType> resourceTypes)
    {
        for (var i = 0; i < Lenght; i++)
        {
            for (var j = 0; j < Widht; j++)
            {
                var resourceType = GetAndRemoveRandomItem(resourceTypes);

                PlayerOne.Tiles[i, j].SetRecourceType(resourceType);
                PlayerTwo.Tiles[i, j].SetRecourceType(resourceType);
            }
        }
    }

    private List<ResourceType> GenerateResourceTypes()
    {
        var result = new List<ResourceType>();

        var red = UnityEngine.Random.Range(8, 14);
        var green = UnityEngine.Random.Range(8, MaxResources - red - 8);
        var blue = MaxResources - red - green;

        for (var i = 0; i < red; i++)
            result.Add(ResourceType.Red);

        for (var i = 0; i < green; i++)
            result.Add(ResourceType.Green);

        for (var i = 0; i < blue; i++)
            result.Add(ResourceType.Blue);

        return result;
    }

    private ResourceType GetAndRemoveRandomItem(List<ResourceType> resourceTypes)
    {
        var index = UnityEngine.Random.Range(0, resourceTypes.Count);
        var result = resourceTypes[index];
        resourceTypes.RemoveAt(index);
        return result;
    }

    private TileType RandomTileType()
    {
        var values = Enum.GetValues(typeof(TileType));
        return (TileType)values.GetValue(new Random().Next(values.Length));
    }
}
