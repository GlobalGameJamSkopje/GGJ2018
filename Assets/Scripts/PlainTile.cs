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
    }

    public void SetRecourceType(ResourceType resourceType)
    {
        ResourceType = resourceType;
    }

    public void SetTileType(TileType tileType)
    {
        TileType = tileType;
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
