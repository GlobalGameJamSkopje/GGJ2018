using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class PlainTile
{

    public TileType TileType { get; private set; }
    public ResourceType ResourceType { get; private set; }

    public bool TowerActive { get; private set; }
    

    public PlainTile()
    {
        TowerActive = false;
        TileType = RandomTileType();
        ResourceType = RandomResourceType();
    }

    private TileType RandomTileType()
    {
        var v = Enum.GetValues(typeof(TileType));
        return (TileType)v.GetValue(new Random().Next(v.Length));
    }

    private ResourceType RandomResourceType()
    {
        var v = Enum.GetValues(typeof(ResourceType));
        return (ResourceType)v.GetValue(new Random().Next(v.Length));
    }

    public void BuildTower()
    {
        TowerActive = true;
    }

    public void DestoryTower()
    {
        TowerActive = false;
    }

    public void Raise()
    {
        TileType = TileType == TileType.Mine ? TileType.Field : TileType.Mountain;
    }

    public void Dig()
    {
        TileType = TileType == TileType.Mountain ? TileType.Field : TileType.Mine;
    }
}
