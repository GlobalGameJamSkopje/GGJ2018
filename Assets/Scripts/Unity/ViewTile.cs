using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTile : MonoBehaviour
{

    public Vector2 worldPosition;

    public SpriteRenderer holeSpriteRenderer;
    public SpriteRenderer mountainSpriteRenderer;
    public SpriteRenderer belowCrystalSpriteRenderer;
    public SpriteRenderer aboveCrystalSpriteRenderer;

    public GameObject TowerGameObject;

    private SpriteRenderer plainSpriteRenderer;

    void Awake()
    {
        plainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangePlainSprite(Sprite sprite)
    {
        plainSpriteRenderer.sprite = sprite;
        aboveCrystalSpriteRenderer.gameObject.SetActive(true);
    }

    public void ChangeHoleSprite(Sprite sprite)
    {
        holeSpriteRenderer.gameObject.SetActive(true);
        holeSpriteRenderer.sprite = sprite;
    }

    public void RemoveHoleSprite()
    {
        holeSpriteRenderer.gameObject.SetActive(false);
    }

    public void ChangeMountainSprite(Sprite sprite)
    {
        aboveCrystalSpriteRenderer.gameObject.SetActive(false);
        mountainSpriteRenderer.gameObject.SetActive(true);
        mountainSpriteRenderer.sprite = sprite;
        DestroyTower();
    }

    public void RemoveMountain()
    {
        aboveCrystalSpriteRenderer.gameObject.SetActive(true);
        mountainSpriteRenderer.gameObject.SetActive(false);
    }

    public void SetResourceType(Sprite spriteAbove, Sprite spriteBelow)
    {
        aboveCrystalSpriteRenderer.sprite = spriteAbove;
        belowCrystalSpriteRenderer.sprite = spriteBelow;
    }

    public void ActivateTower()
    {
        TowerGameObject.SetActive(true);
    }

    public void DestroyTower()
    {
        TowerGameObject.SetActive(false);
    }
}
