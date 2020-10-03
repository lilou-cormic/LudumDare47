using PurpleCable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int MaxAnimalsPerSeason = 10;

    private static GameManager instance;

    [SerializeField] Player Player = null;

    [SerializeField] SeasonBlock[] SeasonBlocks = null;

    [SerializeField] Animal animalPrefab = null;
    public static Animal AnimalPrefab => instance.animalPrefab;

    public static Vector2 ScreenMaxCoord;
    public static float ScreenWidth;
    public static float ScreenHeight;
    public const float PaddingFactor = 0.8f;

    public static Season CurrentSeason;

    private void Awake()
    {
        instance = this;

        Animals.Load();

        CurrentSeason = (Season)Random.Range(0, 4);

        foreach (var seasonBlock in SeasonBlocks)
        {
            seasonBlock.transform.position = Vector3.zero;
            seasonBlock.gameObject.SetActive(CurrentSeason == seasonBlock.Season);
        }
    }

    private void Start()
    {
        Animals.SetSeasons();

        ScreenMaxCoord = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        ScreenWidth = ScreenMaxCoord.x * 2;
        ScreenHeight = ScreenMaxCoord.y * 2;
    }

    private void LateUpdate()
    {
        // Up = Summer
        if (Player.transform.position.y > ScreenMaxCoord.y)
        {
            Player.transform.position += Vector3.down * ScreenHeight;

            SetSeason(Season.Summer);
        }

        // Down = Winter
        if (Player.transform.position.y < -ScreenMaxCoord.y)
        {
            Player.transform.position += Vector3.up * ScreenHeight;

            SetSeason(Season.Winter);
        }

        // Right = Autumn
        if (Player.transform.position.x > ScreenMaxCoord.x)
        {
            Player.transform.position += Vector3.left * ScreenWidth;

            SetSeason(Season.Autumn);
        }

        // Left = Spring
        if (Player.transform.position.x < -ScreenMaxCoord.x)
        {
            Player.transform.position += Vector3.right * ScreenWidth;

            SetSeason(Season.Spring);
        }
    }

    private void SetSeason(Season season)
    {
        CurrentSeason = season;

        foreach (var seasonBlock in SeasonBlocks)
        {
            seasonBlock.gameObject.SetActive(CurrentSeason == seasonBlock.Season);
        }
    }

    public static Vector3 GetClampedPosition(Vector3 position)
    {
        var maxCoordX = ScreenMaxCoord.x;
        var maxCoordY = ScreenMaxCoord.y;

        return new Vector3(Mathf.Clamp(position.x, -maxCoordX * PaddingFactor, maxCoordX * PaddingFactor),
            Mathf.Clamp(position.y, -maxCoordY * PaddingFactor, maxCoordY * PaddingFactor));
    }
}
