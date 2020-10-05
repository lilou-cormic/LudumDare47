using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foliage : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] Sprite SpringSprite = null;

    [SerializeField] Sprite SummerSprite = null;

    [SerializeField] Sprite AutumnSprite = null;

    [SerializeField] Sprite WinterSprite = null;

    private void Awake()
    {
        GameManager.CurrentSeasonChanged += GameManager_CurrentSeasonChanged;
    }

    private void OnDestroy()
    {
        GameManager.CurrentSeasonChanged -= GameManager_CurrentSeasonChanged;
    }

    private void SetSprite()
    {
        switch (GameManager.CurrentSeason)
        {
            case Season.Spring:
                SpriteRenderer.sprite = SpringSprite;
                break;

            case Season.Summer:
                SpriteRenderer.sprite = SummerSprite;
                break;

            case Season.Autumn:
                SpriteRenderer.sprite = AutumnSprite;
                break;

            case Season.Winter:
                SpriteRenderer.sprite = WinterSprite;
                break;
        }

        GetComponentInChildren<Obstacle>()?.gameObject.SetActive(SpriteRenderer.sprite != null);
    }

    private void GameManager_CurrentSeasonChanged()
    {
        SetSprite();
    }
}
