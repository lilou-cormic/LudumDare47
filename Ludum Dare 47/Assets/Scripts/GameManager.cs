using PurpleCable;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int MaxAnimalsPerSeason = 10;

    public const int FoliageLayer = 10;

    private static GameManager instance;

    [SerializeField] int Timer = 60 * 4;

    [SerializeField] Player Player = null;

    [SerializeField] SeasonBlock[] SeasonBlocks = null;

    [SerializeField] Animal animalPrefab = null;
    public static Animal AnimalPrefab => instance.animalPrefab;

    [SerializeField] AudioClip GameOverSound = null;

    public static Vector2 ScreenMaxCoord;
    public static float ScreenWidth;
    public static float ScreenHeight;
    public const float PaddingFactor = 0.8f;

    private static Season _CurrentSeason;
    public static Season CurrentSeason
    {
        get => _CurrentSeason;

        set
        {
            _CurrentSeason = value;
            CurrentSeasonChanged?.Invoke();
        }
    }

    public static event System.Action CurrentSeasonChanged;

    public static float TimeLeft { get; private set; }

    private void Awake()
    {
        instance = this;

        Animals.Load();

        CurrentSeason = (Season)Random.Range(0, 4);

        foreach (var seasonBlock in SeasonBlocks)
        {
            seasonBlock.transform.position = Vector3.zero;
            seasonBlock.gameObject.SetActive(CurrentSeason == seasonBlock.Season);

            if (seasonBlock.Season == CurrentSeason)
                seasonBlock.PlayMusic();
        }
    }

    private void Start()
    {
        TimeLeft = Timer;
        ScoreManager.ResetScore();
        Animals.SetSeasons();

        ScreenMaxCoord = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        ScreenWidth = ScreenMaxCoord.x * 2;
        ScreenHeight = ScreenMaxCoord.y * 2;

        Cursor.visible = false;
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0)
        {
            GameOverSound?.Play();

            ScoreManager.SetHighScore();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
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

            if (seasonBlock.Season == CurrentSeason)
                seasonBlock.PlayMusic();
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
