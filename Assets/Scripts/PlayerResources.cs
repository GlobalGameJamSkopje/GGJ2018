using System;

public class PlayerResources : IPlayerResources
{
    public int NumberOfRedResources { get; private set; }
    public int NumberOfGreenResources { get; private set; }
    public int NumberOfBlueResources { get; private set; }

    public PlayerResources(int numberOfRedResources, int numberOfGreenResources, int numberOfBlueResources)
    {
        NumberOfRedResources = numberOfRedResources;
        NumberOfGreenResources = numberOfGreenResources;
        NumberOfBlueResources = numberOfBlueResources;
    }

    public void AddResource(ResourceType resourceType, TileType tileType)
    {
        var tileValue = GetValue(tileType);

        switch (resourceType)
        {
            case ResourceType.Red:
                NumberOfRedResources += tileValue;
                break;
            case ResourceType.Green:
                NumberOfGreenResources += tileValue;
                break;
            case ResourceType.Blue:
                NumberOfBlueResources += tileValue;
                break;
            default:
                throw new ArgumentOutOfRangeException("resourceType", resourceType, null);
        }
    }

    public void RemoveResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Red:
                if (NumberOfRedResources > 0)
                    NumberOfRedResources--;
                break;
            case ResourceType.Green:
                if (NumberOfGreenResources > 0)
                    NumberOfGreenResources--;
                break;
            case ResourceType.Blue:
                if (NumberOfBlueResources > 0)
                    NumberOfBlueResources--;
                break;
            default:
                throw new ArgumentOutOfRangeException("resourceType", resourceType, null);
        }
    }

    private int GetValue(TileType tileType)
    {
        var result = tileType == TileType.Mine
            ? 2
            : tileType == TileType.Field
                ? 1
                : 0;

        return result;
    }
}