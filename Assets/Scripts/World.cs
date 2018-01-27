using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class World
{
    public const int Length = 6;
    public const int Width = 5;
    private const int MaxResources = Length * Width;

    public List<QuestItem> Quests { get; private set; }
    public Plain P1Plain { get; private set; }
    public Plain P2Plain { get; private set; }

    public PlayerSideQuest P1SideQuest { get; private set; }
    public PlayerSideQuest P2SideQuest { get; private set; }

    public PlayerAction P1Action { get; private set; }
    public PlayerAction P2Action { get; private set; }

    public PlayerResources P1Resources { get; private set; }
    public PlayerResources P2Resources { get; private set; }

    private readonly IQuestCreator _questCreator;
    private readonly Random _random = new Random();

    public World()
    {
        _questCreator = new QuestCreator();

        P1Plain = new Plain(Length, Width);
        P2Plain = new Plain(Length, Width);

        P1Resources = new PlayerResources(0, 0, 0);
        P2Resources = new PlayerResources(0, 0, 0);

        P1SideQuest = new PlayerSideQuest();
        P2SideQuest = new PlayerSideQuest();

        P1Action = new PlayerAction(2, 0, 0, 0);
        P2Action = new PlayerAction(2, 0, 0, 0);

        var resourceTypes = GenerateResourceTypes();

        Quests = _questCreator.CreateQuests(
            resourceTypes.Count(x => x == ResourceType.Red),
            resourceTypes.Count(x => x == ResourceType.Green),
            resourceTypes.Count(x => x == ResourceType.Blue));

        GenerateResourceMap(resourceTypes);
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        for (var i = 0; i < Length; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                var tileTypeP1 = RandomTileType();
                var tileTypeP2 = RandomTileType();

                if (tileTypeP1 == TileType.Mine)
                    tileTypeP2 = TileType.Mountain;

                if (tileTypeP2 == TileType.Mine)
                    tileTypeP1 = TileType.Mountain;

                P1Plain.Tiles[i, j].SetTileType(tileTypeP1);
                P2Plain.Tiles[i, j].SetTileType(tileTypeP2);
            }
        }
    }

    private void GenerateResourceMap(List<ResourceType> resourceTypes)
    {
        for (var i = 0; i < Length; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                var resourceType = GetAndRemoveRandomItem(resourceTypes);

                P1Plain.Tiles[i, j].SetRecourceType(resourceType);
                P2Plain.Tiles[i, j].SetRecourceType(resourceType);
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

        return (TileType)values.GetValue(_random.Next(values.Length));
    }
}
