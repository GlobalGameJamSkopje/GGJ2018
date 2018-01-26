public class PlayerResources : IPlayerResources
{
    public int NumberOfRedResources { get; set; }
    public int NumberOfGreenResources { get; set; }
    public int NumberOfBlueResources { get; set; }

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
                break;
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