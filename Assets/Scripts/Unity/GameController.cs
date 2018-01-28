using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool _canCastRay = true;

    public GameObject Player1;
    public GameObject Player2;

    public Text RedResourcesText, GreenResourcesText, BlueResourcesText;

    public Image FirstSideQuestHoldImage, SecondSideQuestHoldImage;
    public Text FirstSideQuestText, SecondSideQuestText;
    public Sprite HoldSprite, UnholdSprite;

    public GameObject PlayerOneTurnCanvas, PlayerTwoTurnCanvas;
    public GameObject PlayerOneWinCanvas, PlayerTwoWinCanvas;


    private WorldCreator _worldCreator;
    private World _world;
    private QuestItemSolver _questSolver;

    private ViewTile _p1PositionTile;
    private ViewTile _p2PositionTile;

    [Header("Quests")] public Color32 DefaultGemColor;

    [Header("Quest1")] [Space] public Image Q1RedGem1;
    public Image Q1RedGem2, Q1RedGem3;
    public Image Q1GreenGem1, Q1GreenGem2, Q1GreenGem3;
    public Image Q1BlueGem1, Q1BlueGem2, Q1BlueGem3;

    [Header("Quest2")] [Space] public Image Q2RedGem1;
    public Image Q2RedGem2, Q2RedGem3;
    public Image Q2GreenGem1, Q2GreenGem2, Q2GreenGem3;
    public Image Q2BlueGem1, Q2BlueGem2, Q2BlueGem3;

    [Header("Quest3")] [Space] public Image Q3RedGem1;
    public Image Q3RedGem2, Q3RedGem3;
    public Image Q3GreenGem1, Q3GreenGem2, Q3GreenGem3;
    public Image Q3BlueGem1, Q3BlueGem2, Q3BlueGem3;

    [Header("Quest4")] [Space] public Image Q4RedGem1;
    public Image Q4RedGem2, Q4RedGem3;
    public Image Q4GreenGem1, Q4GreenGem2, Q4GreenGem3;
    public Image Q4BlueGem1, Q4BlueGem2, Q4BlueGem3;

    [Header("Quest5")] [Space] public Image Q5RedGem1;
    public Image Q5RedGem2, Q5RedGem3;
    public Image Q5GreenGem1, Q5GreenGem2, Q5GreenGem3;
    public Image Q5BlueGem1, Q5BlueGem2, Q5BlueGem3;



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
        if (Input.GetMouseButtonDown(0) && _canCastRay)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<ViewTile>())
                {
                    if (_currentPlayer == PlayerIndex.P1)
                        MoveP1(hit.collider.gameObject.GetComponent<ViewTile>());
                    else
                        MoveP2(hit.collider.gameObject.GetComponent<ViewTile>());
                }
            }
        }
    }

    public void NextTurn()
    {
        _canCastRay = false;
        if (_currentPlayer == PlayerIndex.P1)
        {
            Player2Turn();
            if (_questSolver.CanBeSolved(_world.Quests, _world.P1Resources))
            {
                PlayerOneWinCanvas.SetActive(true);
            }
            else
            {
                PlayerTwoTurnCanvas.SetActive(true);
            } 
        }
        else
        {
            Player1Turn();
            if (_questSolver.CanBeSolved(_world.Quests, _world.P2Resources))
            {
                PlayerTwoWinCanvas.SetActive(true);
            }
            else
            {
                PlayerOneTurnCanvas.SetActive(true);
            }      
        }
    }

    public void CloseNextTurnPanels()
    {
        _canCastRay = true;
        PlayerOneTurnCanvas.SetActive(false);
        PlayerTwoTurnCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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

            if (_world.P1SideQuest.SideQuests[0].Hold)
            {
                FirstSideQuestHoldImage.sprite = HoldSprite;
            }
            else
            {
                FirstSideQuestHoldImage.sprite = UnholdSprite;
            }
        }
        else
        {
            _world.P2SideQuest.ToggleHoldOnQuest(_world.P2SideQuest.SideQuests[0]);

            if (_world.P2SideQuest.SideQuests[0].Hold)
            {
                FirstSideQuestHoldImage.sprite = HoldSprite;
            }
            else
            {
                FirstSideQuestHoldImage.sprite = UnholdSprite;
            }
        }
    }

    public void HoldSecondSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            _world.P1SideQuest.ToggleHoldOnQuest(_world.P1SideQuest.SideQuests[1]);

            if (_world.P1SideQuest.SideQuests[1].Hold)
            {
                SecondSideQuestHoldImage.sprite = HoldSprite;
            }
            else
            {
                SecondSideQuestHoldImage.sprite = UnholdSprite;
            }
        }
        else
        {
            _world.P2SideQuest.ToggleHoldOnQuest(_world.P2SideQuest.SideQuests[1]);

            if (_world.P2SideQuest.SideQuests[1].Hold)
            {
                SecondSideQuestHoldImage.sprite = HoldSprite;
            }
            else
            {
                SecondSideQuestHoldImage.sprite = UnholdSprite;
            }
        }
    }

    public void CompleteFirstSideQuest()
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            var quest = _world.P1SideQuest.SideQuests[0];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P1Action.GetSideQuestReward(quest);
                _world.P1SideQuest.CompleteQuest(quest);
                //TODO: refresh actions UI
            }
        }
        else
        {
            var quest = _world.P2SideQuest.SideQuests[0];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P2Action.GetSideQuestReward(quest);
                _world.P2SideQuest.CompleteQuest(quest);
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
                _world.P1Action.GetSideQuestReward(quest);
                _world.P1SideQuest.CompleteQuest(quest);
                //TODO: refresh actions UI
            }
        }
        else
        {
            var quest = _world.P2SideQuest.SideQuests[1];
            if (_questSolver.CanBeSolved(quest, _world.P1Resources))
            {
                _world.P2Action.GetSideQuestReward(quest);
                _world.P2SideQuest.CompleteQuest(quest);
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
        _currentPlayer = PlayerIndex.P1;

        _world.P1SideQuest.RefillSideQuests();
        Camera.main.transform.position = new Vector3(0, 0, -10);
        _world.P1Action.NewTurn();

        RefreshUIP1();
    }

    private void Player2Turn()
    {
        _currentPlayer = PlayerIndex.P2;

        _world.P2SideQuest.RefillSideQuests();
        Camera.main.transform.position = new Vector3(20, 0, -10);
        _world.P2Action.NewTurn();

        RefreshUIP2();

    }

    private void RefreshUIP1()
    {
        RedResourcesText.text = "x " + _world.P1Resources.NumberOfRedResources;
        GreenResourcesText.text = "x " + _world.P1Resources.NumberOfGreenResources;
        BlueResourcesText.text = "x " + _world.P1Resources.NumberOfBlueResources;

        if (_world.P1SideQuest.SideQuests[0].Hold)
        {
            FirstSideQuestHoldImage.sprite = HoldSprite;
        }
        else
        {
            FirstSideQuestHoldImage.sprite = UnholdSprite;
        }

        if (_world.P1SideQuest.SideQuests[1].Hold)
        {
            SecondSideQuestHoldImage.sprite = HoldSprite;
        }
        else
        {
            SecondSideQuestHoldImage.sprite = UnholdSprite;
        }

        FirstSideQuestText.text = _world.P1SideQuest.SideQuests[0].ToString();
        SecondSideQuestText.text = _world.P1SideQuest.SideQuests[1].ToString();

        RefreshQuestsUI();
    }

    private void RefreshUIP2()
    {
        RedResourcesText.text = "x " + _world.P2Resources.NumberOfRedResources;
        GreenResourcesText.text = "x " + _world.P2Resources.NumberOfGreenResources;
        BlueResourcesText.text = "x " + _world.P2Resources.NumberOfBlueResources;

        if (_world.P2SideQuest.SideQuests[0].Hold)
        {
            FirstSideQuestHoldImage.sprite = HoldSprite;
        }
        else
        {
            FirstSideQuestHoldImage.sprite = UnholdSprite;
        }

        if (_world.P2SideQuest.SideQuests[1].Hold)
        {
            SecondSideQuestHoldImage.sprite = HoldSprite;
        }
        else
        {
            SecondSideQuestHoldImage.sprite = UnholdSprite;
        }

        FirstSideQuestText.text = _world.P2SideQuest.SideQuests[0].ToString();
        SecondSideQuestText.text = _world.P2SideQuest.SideQuests[1].ToString();

        RefreshQuestsUI();
    }

    private void RefreshQuestsUI()
    {
        RefreshQuest1UI();
        RefreshQuest2UI();
        RefreshQuest3UI();
        RefreshQuest4UI();
        RefreshQuest5UI();
    }

    private void RefreshQuest1UI()
    {
        Q1RedGem1.color = DefaultGemColor;
        Q1RedGem2.color = DefaultGemColor;
        Q1RedGem3.color = DefaultGemColor;

        Q1GreenGem1.color = DefaultGemColor;
        Q1GreenGem2.color = DefaultGemColor;
        Q1GreenGem3.color = DefaultGemColor;

        Q1BlueGem1.color = DefaultGemColor;
        Q1BlueGem2.color = DefaultGemColor;
        Q1BlueGem3.color = DefaultGemColor;

        RefreshQuestImages(_world.Quests[0], 
            Q1RedGem1, Q1RedGem2, Q1RedGem3, 
            Q1GreenGem1, Q1GreenGem2, Q1GreenGem3, 
            Q1BlueGem1, Q1BlueGem2, Q1BlueGem3);
    }
    private void RefreshQuest2UI()
    {
        Q2RedGem1.color = DefaultGemColor;
        Q2RedGem2.color = DefaultGemColor;
        Q2RedGem3.color = DefaultGemColor;

        Q2GreenGem1.color = DefaultGemColor;
        Q2GreenGem2.color = DefaultGemColor;
        Q2GreenGem3.color = DefaultGemColor;

        Q2BlueGem1.color = DefaultGemColor;
        Q2BlueGem2.color = DefaultGemColor;
        Q2BlueGem3.color = DefaultGemColor;

        RefreshQuestImages(_world.Quests[1],
            Q2RedGem1, Q2RedGem2, Q2RedGem3,
            Q2GreenGem1, Q2GreenGem2, Q2GreenGem3,
            Q2BlueGem1, Q2BlueGem2, Q2BlueGem3);
    }
    private void RefreshQuest3UI()
    {
        Q3RedGem1.color = DefaultGemColor;
        Q3RedGem2.color = DefaultGemColor;
        Q3RedGem3.color = DefaultGemColor;

        Q3GreenGem1.color = DefaultGemColor;
        Q3GreenGem2.color = DefaultGemColor;
        Q3GreenGem3.color = DefaultGemColor;

        Q3BlueGem1.color = DefaultGemColor;
        Q3BlueGem2.color = DefaultGemColor;
        Q3BlueGem3.color = DefaultGemColor;

        RefreshQuestImages(_world.Quests[2],
            Q3RedGem1, Q3RedGem2, Q3RedGem3,
            Q3GreenGem1, Q3GreenGem2, Q3GreenGem3,
            Q3BlueGem1, Q3BlueGem2, Q3BlueGem3);
    }
    private void RefreshQuest4UI()
    {
        Q4RedGem1.color = DefaultGemColor;
        Q4RedGem2.color = DefaultGemColor;
        Q4RedGem3.color = DefaultGemColor;

        Q4GreenGem1.color = DefaultGemColor;
        Q4GreenGem2.color = DefaultGemColor;
        Q4GreenGem3.color = DefaultGemColor;

        Q4BlueGem1.color = DefaultGemColor;
        Q4BlueGem2.color = DefaultGemColor;
        Q4BlueGem3.color = DefaultGemColor;

        RefreshQuestImages(_world.Quests[3],
            Q4RedGem1, Q4RedGem2, Q4RedGem3,
            Q4GreenGem1, Q4GreenGem2, Q4GreenGem3,
            Q4BlueGem1, Q4BlueGem2, Q4BlueGem3);
    }
    private void RefreshQuest5UI()
    {
        Q5RedGem1.color = DefaultGemColor;
        Q5RedGem2.color = DefaultGemColor;
        Q5RedGem3.color = DefaultGemColor;

        Q5GreenGem1.color = DefaultGemColor;
        Q5GreenGem2.color = DefaultGemColor;
        Q5GreenGem3.color = DefaultGemColor;

        Q5BlueGem1.color = DefaultGemColor;
        Q5BlueGem2.color = DefaultGemColor;
        Q5BlueGem3.color = DefaultGemColor;

        RefreshQuestImages(_world.Quests[4],
            Q5RedGem1, Q5RedGem2, Q5RedGem3,
            Q5GreenGem1, Q5GreenGem2, Q5GreenGem3,
            Q5BlueGem1, Q5BlueGem2, Q5BlueGem3);
    }

    private void RefreshQuestImages(QuestItem quest, Image redGem1, Image redGem2, Image redGem3, Image greenGem1, Image greenGem2, Image greenGem3, Image blueGem1, Image blueGem2, Image blueGem3)
    {
        if (_currentPlayer == PlayerIndex.P1)
        {
            if (quest.RequiredRedResources == 1)
            {
                if (_world.P1Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
            }
            if (quest.RequiredRedResources == 2)
            {
                if (_world.P1Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
                if (_world.P1Resources.NumberOfRedResources >= 2) redGem2.color = Color.white;
            }
            if (quest.RequiredRedResources == 3)
            {
                if (_world.P1Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
                if (_world.P1Resources.NumberOfRedResources >= 2) redGem2.color = Color.white;
                if (_world.P1Resources.NumberOfRedResources >= 3) redGem3.color = Color.white;
            }

            if (quest.RequiredGreenResources == 1)
            {
                if (_world.P1Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
            }
            if (quest.RequiredGreenResources == 2)
            {
                if (_world.P1Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
                if (_world.P1Resources.NumberOfGreenResources >= 2) greenGem2.color = Color.white;
            }
            if (quest.RequiredGreenResources == 3)
            {
                if (_world.P1Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
                if (_world.P1Resources.NumberOfGreenResources >= 2) greenGem2.color = Color.white;
                if (_world.P1Resources.NumberOfGreenResources >= 3) greenGem3.color = Color.white;
            }

            if (quest.RequiredBlueResources == 1)
            {
                if (_world.P1Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
            }
            if (quest.RequiredBlueResources == 2)
            {
                if (_world.P1Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
                if (_world.P1Resources.NumberOfBlueResources >= 2) blueGem2.color = Color.white;
            }
            if (quest.RequiredBlueResources == 3)
            {
                if (_world.P1Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
                if (_world.P1Resources.NumberOfBlueResources >= 2) blueGem2.color = Color.white;
                if (_world.P1Resources.NumberOfBlueResources >= 3) blueGem3.color = Color.white;
            }
        }
        else
        {
            if (quest.RequiredRedResources == 1)
            {
                if (_world.P2Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
            }
            if (quest.RequiredRedResources == 2)
            {
                if (_world.P2Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
                if (_world.P2Resources.NumberOfRedResources >= 2) redGem2.color = Color.white;
            }
            if (quest.RequiredRedResources == 3)
            {
                if (_world.P2Resources.NumberOfRedResources >= 1) redGem1.color = Color.white;
                if (_world.P2Resources.NumberOfRedResources >= 2) redGem2.color = Color.white;
                if (_world.P2Resources.NumberOfRedResources >= 3) redGem3.color = Color.white;
            }

            if (quest.RequiredGreenResources == 1)
            {
                if (_world.P2Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
            }
            if (quest.RequiredGreenResources == 2)
            {
                if (_world.P2Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
                if (_world.P2Resources.NumberOfGreenResources >= 2) greenGem2.color = Color.white;
            }
            if (quest.RequiredGreenResources == 3)
            {
                if (_world.P2Resources.NumberOfGreenResources >= 1) greenGem1.color = Color.white;
                if (_world.P2Resources.NumberOfGreenResources >= 2) greenGem2.color = Color.white;
                if (_world.P2Resources.NumberOfGreenResources >= 3) greenGem3.color = Color.white;
            }

            if (quest.RequiredBlueResources == 1)
            {
                if (_world.P2Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
            }
            if (quest.RequiredBlueResources == 2)
            {
                if (_world.P2Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
                if (_world.P2Resources.NumberOfBlueResources >= 2) blueGem2.color = Color.white;
            }
            if (quest.RequiredBlueResources == 3)
            {
                if (_world.P2Resources.NumberOfBlueResources >= 1) blueGem1.color = Color.white;
                if (_world.P2Resources.NumberOfBlueResources >= 2) blueGem2.color = Color.white;
                if (_world.P2Resources.NumberOfBlueResources >= 3) blueGem3.color = Color.white;
            }
        }
    }
}
