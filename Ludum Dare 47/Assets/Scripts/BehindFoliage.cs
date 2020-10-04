using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindFoliage : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    private int OriginalSortingLayer;

    private void Start()
    {
        OriginalSortingLayer = SpriteRenderer.sortingLayerID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.FoliageLayer)
            SpriteRenderer.sortingLayerName = "BehindFoliage";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.FoliageLayer)
            SpriteRenderer.sortingLayerID = OriginalSortingLayer;
    }
}
