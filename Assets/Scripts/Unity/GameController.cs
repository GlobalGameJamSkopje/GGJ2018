using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public Text RedResourcesText, GreenResourcesText, BlueResourcesText;
    public Text Quest1Text, Quest2Text, Quest3Text, Quest4Text, Quest5Text;

    public Image FirstSideQuestHoldImage, SecondSideQuestHoldImage;
    public Text FirstSideQuestText, SecondSideQuestText;

    private WorldCreator _worldCreator;
    private World _world;
    private QuestItemSolver _questSolver;

    private ViewTile _p1PositionTile;
    private ViewTile _p2PositionTile;

    private PlayerIndex _currentPlayer;

    void Awake()
    {
        _world = new World();
        _questSolver = new QuestItemSolver();
        _worldCreator = FindObjectOfType<WorldCreator>();
    }

    void Start()
    {
        Player1Turn();

        _worldCreator.GenerateWorld(_world);

        _p1PositionTile = _worldCreator.ViewTilesMultiArrayPlayerOne[0, 0];
        _p2PositionTile = _worldCreator.ViewTilesMultiArrayPlayerTwo[5, 0];
        Player1.transform.position = _p1PositionTile.transform.position;
        Player2.transform.position = _p2PositionTile.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (_currentPlayer == PlayerIndex.P1)
                    MoveP1(hit.collider.gameObject.GetComponent<ViewTile>());
                else
                    MoveP2(hit.collider.gameObject.GetComponent<ViewTile>());

            }
        }
    }

    public void NextTurn()
    {
        if (_currentPlayer == PlayerIndex.P1) Player2Turn();
        else Player1Turn();
    }

    public void Build()
    {
        if (_currentPlayer == PlayerIndex.P1)
            BuildP1(_p1PositionTile);
        else
            BuildP2(_p2PositionTile);
    }

    public void Dig()
    {
        if (_currentPlayer == PlayerIndex.P1)
            DigP1(_p1PositionTile);
        else
            DigP2(_p2PositionTile);
    }

    public void HoldFirstSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            _world.P1SideQuest.ToggleHoldOnQuest(_world.P1SideQuest.SideQuests[0]);
            //TODO: change sprite
        }
        else
        {
            _world.P2SideQuest.ToggleHoldOnQuest(_world.P2SideQuest.SideQuests[0]);
            //TODO: change sprite
        }
    }

    public void HoldSecondSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            _world.P1SideQuest.ToggleHoldOnQuest(_world.P1SideQuest.SideQuests[1]);
            //TODO:change sprite
        }
        else
        {
            _world.P2SideQuest.ToggleHoldOnQuest(_world.P2SideQuest.SideQuests[1]);
            //TODO: change sprite
        }
    }

    public void CompleteFirstSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            var quest = _world.P1SideQuest.SideQuests[0];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P1SideQuest.CompleteQuest(quest);
                _world.P1Action.GetSideQuestReward(quest);
                //TODO: refresh actions UI
            }
        }
        else
        {
            var quest = _world.P2SideQuest.SideQuests[0];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P2SideQuest.CompleteQuest(quest);
                _world.P2Action.GetSideQuestReward(quest);
                //TODO: refresh actions UI
            }
        }
    }

    public void CompleteSecondSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            var quest = _world.P1SideQuest.SideQuests[1];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P1SideQuest.CompleteQuest(quest);
                _world.P1Action.GetSideQuestReward(quest);
                //TODO: refresh actions UI
            }
        }
        else
        {
            var quest = _world.P2SideQuest.SideQuests[1];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P2SideQuest.CompleteQuest(quest);
                _world.P2Action.GetSideQuestReward(quest);
                //TODO: refresh actions UI
            }
        }
    }


    private void DigP1(ViewTile tile)
    {
        if (!_world.P1Action.CanUseAction(PlayerActionType.Dig)) return;

        var tileP1 = _world.P1Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];
        var tileP2 = _world.P2Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];

        switch (tileP1.TileType)
        {
            case TileType.Mine:
                break;
            case TileType.Field:
                tileP1.Dig();
                tileP2.Raise();
                _worldCreator.MakeMineP1(tile);
                _worldCreator.MakeMountainP2(tile);
                _world.P1Action.UseAction(PlayerActionType.Dig);
                if (tileP2.TowerActive)
                {
                    tileP2.DestoryTower();
                    _world.P2Resources.RemoveResource(tileP2.ResourceType);
                }

                break;
            case TileType.Mountain:
                tileP1.Dig();
                _world.P1Action.UseAction(PlayerActionType.Dig);
                if (tileP2.TileType == TileType.Mine)
                {
                    tileP2.Raise();
                    if (tileP2.TowerActive) _world.P2Resources.RemoveResource(tileP2.ResourceType);
                    _worldCreator.MakePlainP2(tile);
                    _worldCreator.ViewTilesMultiArrayPlayerTwo[(int)tile.worldPosition.x, (int)tile.worldPosition.y].RemoveHoleSprite();
                }
                _worldCreator.MakePlainP1(tile);
                tile.RemoveMountain();

                break;
            default:
                break;
        }
    }

    private void DigP2(ViewTile tile)
    {
        if (!_world.P2Action.CanUseAction(PlayerActionType.Dig)) return;

        var tileP1 = _world.P1Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];
        var tileP2 = _world.P2Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];

        switch (tileP2.TileType)
        {
            case TileType.Mine:
                break;
            case TileType.Field:
                tileP2.Dig();
                tileP1.Raise();
                _worldCreator.MakeMineP2(tile);
                _worldCreator.MakeMountainP1(tile);
                _world.P2Action.UseAction(PlayerActionType.Dig);
                if (tileP1.TowerActive)
                {
                    tileP1.DestoryTower();
                    _world.P1Resources.RemoveResource(tileP1.ResourceType);
                }

                break;
            case TileType.Mountain:
                tileP2.Dig();
                _world.P2Action.UseAction(PlayerActionType.Dig);
                if (tileP1.TileType == TileType.Mine)
                {
                    tileP1.Raise();
                    if (tileP1.TowerActive) _world.P1Resources.RemoveResource(tileP1.ResourceType);
                    _worldCreator.MakePlainP1(tile);
                    tile.RemoveHoleSprite();
                    _worldCreator.ViewTilesMultiArrayPlayerOne[(int)tile.worldPosition.x, (int)tile.worldPosition.y].RemoveHoleSprite();
                }
                _worldCreator.MakePlainP2(tile);
                tile.RemoveMountain();

                break;
            default:
                break;
        }
    }

    private void BuildP1(ViewTile tile)
    {
        var tileP1 = _world.P1Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];

        if (!_world.P1Action.CanUseAction(PlayerActionType.Build) || tileP1.TowerActive) return;

        switch (tileP1.TileType)
        {
            case TileType.Mine:
                tileP1.BuildTower();
                _worldCreator.MakeTowerP1(tile);
                tile.ActivateTower();
                _world.P1Resources.AddResource(tileP1.ResourceType, TileType.Mine);
                _world.P1Action.UseAction(PlayerActionType.Build);

                break;
            case TileType.Field:
                tileP1.BuildTower();
                _worldCreator.MakeTowerP1(tile);
                tile.ActivateTower();
                _world.P1Resources.AddResource(tileP1.ResourceType, TileType.Field);
                _world.P1Action.UseAction(PlayerActionType.Build);
                break;
            case TileType.Mountain:
                break;
        }

        RefreshUIP1();
    }

    private void BuildP2(ViewTile tile)
    {
        var tileP2 = _world.P2Plain.Tiles[(int)tile.worldPosition.x, (int)tile.worldPosition.y];

        if (!_world.P2Action.CanUseAction(PlayerActionType.Build) || tileP2.TowerActive) return;

        switch (tileP2.TileType)
        {
            case TileType.Mine:
                tileP2.BuildTower();
                _worldCreator.MakeTowerP2(tile);
                tile.ActivateTower();
                _world.P2Resources.AddResource(tileP2.ResourceType, TileType.Mine);
                _world.P2Action.UseAction(PlayerActionType.Build);
                break;
            case TileType.Field:
                tileP2.BuildTower();
                _worldCreator.MakeTowerP2(tile);
                tile.ActivateTower();
                _world.P2Resources.AddResource(tileP2.ResourceType, TileType.Field);
                _world.P2Action.UseAction(PlayerActionType.Build);
                break;
            case TileType.Mountain:
                break;
        }

        RefreshUIP2();
    }

    private void MoveP1(ViewTile tile)
    {
        if (!_world.P1Action.CanUseAction(PlayerActionType.Move)) return;

        var movesOverX = Math.Abs(_p1PositionTile.worldPosition.x - tile.worldPosition.x) == 1 && Math.Abs(_p1PositionTile.worldPosition.y - tile.worldPosition.y) == 0;
        var movesOverY = Math.Abs(_p1PositionTile.worldPosition.x - tile.worldPosition.x) == 0 && Math.Abs(_p1PositionTile.worldPosition.y - tile.worldPosition.y) == 1;

        if (movesOverX || movesOverY)
        {
            _world.P1Action.UseAction(PlayerActionType.Move);
            _p1PositionTile = tile;
            Player1.transform.position = tile.transform.position;
        }
    }

    private void MoveP2(ViewTile tile)
    {
        if (!_world.P2Action.CanUseAction(PlayerActionType.Move)) return;

        var movesOverX = Math.Abs(_p2PositionTile.worldPosition.x - tile.worldPosition.x) == 1 && Math.Abs(_p2PositionTile.worldPosition.y - tile.worldPosition.y) == 0;
        var movesOverY = Math.Abs(_p2PositionTile.worldPosition.x - tile.worldPosition.x) == 0 && Math.Abs(_p2PositionTile.worldPosition.y - tile.worldPosition.y) == 1;

        if (movesOverX || movesOverY)
        {
            _world.P2Action.UseAction(PlayerActionType.Move);
            _p2PositionTile = tile;
            Player2.transform.position = tile.transform.position;
        }
    }

    private void Player1Turn()
    {
        _world.P1SideQuest.RefillSideQuests();
        RefreshUIP1();
        Camera.main.transform.position = new Vector3(0, 0, -10);
        _world.P1Action.NewTurn();
        _currentPlayer = PlayerIndex.P1;
    }

    private void Player2Turn()
    {
        _world.P2SideQuest.RefillSideQuests();
        RefreshUIP2();
        Camera.main.transform.position = new Vector3(20, 0, -10);
        _world.P2Action.NewTurn();
        _currentPlayer = PlayerIndex.P2;

    }

    private void RefreshUIP1()
    {
        RedResourcesText.text = "x " + _world.P1Resources.NumberOfRedResources;
        GreenResourcesText.text = "x " + _world.P1Resources.NumberOfGreenResources;
        BlueResourcesText.text = "x " + _world.P1Resources.NumberOfBlueResources;

        FirstSideQuestText.text = _world.P1SideQuest.SideQuests[0].ToString();
        SecondSideQuestText.text = _world.P1SideQuest.SideQuests[1].ToString();


        Quest1Text.text = "red : " + _world.Quests[0].RequiredRedResources.ToString() + " : " + _world.P1Resources.NumberOfRedResources + "\n"
            + "green : " + _world.Quests[0].RequiredGreenResources.ToString() + " : " + _world.P1Resources.NumberOfGreenResources + "\n"
            + "blue : " + _world.Quests[0].RequiredBlueResources.ToString() + " : " + _world.P1Resources.NumberOfBlueResources;

        Quest2Text.text = "red : " + _world.Quests[1].RequiredRedResources.ToString() + " : " + _world.P1Resources.NumberOfRedResources + "\n"
                          + "green : " + _world.Quests[1].RequiredGreenResources.ToString() + " : " + _world.P1Resources.NumberOfGreenResources + "\n"
                          + "blue : " + _world.Quests[1].RequiredBlueResources.ToString() + " : " + _world.P1Resources.NumberOfBlueResources;

        Quest3Text.text = "red : " + _world.Quests[2].RequiredRedResources.ToString() + " : " + _world.P1Resources.NumberOfRedResources + "\n"
                          + "green : " + _world.Quests[2].RequiredGreenResources.ToString() + " : " + _world.P1Resources.NumberOfGreenResources + "\n"
                          + "blue : " + _world.Quests[2].RequiredBlueResources.ToString() + " : " + _world.P1Resources.NumberOfBlueResources;

        Quest4Text.text = "red : " + _world.Quests[3].RequiredRedResources.ToString() + " : " + _world.P1Resources.NumberOfRedResources + "\n"
                          + "green : " + _world.Quests[3].RequiredGreenResources.ToString() + " : " + _world.P1Resources.NumberOfGreenResources + "\n"
                          + "blue : " + _world.Quests[3].RequiredBlueResources.ToString() + " : " + _world.P1Resources.NumberOfBlueResources;

        Quest5Text.text = "red : " + _world.Quests[4].RequiredRedResources.ToString() + " : " + _world.P1Resources.NumberOfRedResources + "\n"
                          + "green : " + _world.Quests[4].RequiredGreenResources.ToString() + " : " + _world.P1Resources.NumberOfGreenResources + "\n"
                          + "blue : " + _world.Quests[4].RequiredBlueResources.ToString() + " : " + _world.P1Resources.NumberOfBlueResources;
    }

    private void RefreshUIP2()
    {
        RedResourcesText.text = "x " + _world.P2Resources.NumberOfRedResources;
        GreenResourcesText.text = "x " + _world.P2Resources.NumberOfGreenResources;
        BlueResourcesText.text = "x " + _world.P2Resources.NumberOfBlueResources;

        FirstSideQuestText.text = _world.P2SideQuest.SideQuests[0].ToString();
        SecondSideQuestText.text = _world.P2SideQuest.SideQuests[1].ToString();
    }

}
