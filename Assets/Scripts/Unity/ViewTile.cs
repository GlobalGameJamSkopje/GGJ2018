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

    public SpriteRenderer TowerSpriteRenderer;

    private SpriteRenderer plainSpriteRenderer;

    void Awake()
    {
        plainSpriteRenderer = GetComponent<SpriteRenderer>();
        TowerSpriteRenderer = gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
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

    public void SetResourceType(Sprite sprite)
    {
        aboveCrystalSpriteRenderer.sprite = sprite;
        belowCrystalSpriteRenderer.sprite = sprite;
    }

    public void ActivateTower()
    {

        TowerSpriteRenderer.gameObject.SetActive(true);

    }

    public void DestroyTower()
    {
        TowerSpriteRenderer.gameObject.SetActive(false);
    }
}
