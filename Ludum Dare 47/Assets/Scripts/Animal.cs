using PurpleCable;
using UnityEngine;

public class Animal : Item, ISpeed
{
    public const float Speed = 1f;

    private float SpeedFactor = 1f;

    public AnimalDef Def { get; private set; }

    [SerializeField] SpriteRenderer SpriteRenderer;

    private Vector3 destination = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        SetRandomDestination();
    }

    private void OnDestroy()
    {
        GetComponentInParent<SeasonBlock>()?.RemoveAnimal(this);
    }

    public void SetDef(AnimalDef def)
    {
        Def = def;

        name = def.Name;
        SpriteRenderer.sprite = Def.Sprite;
    }

    protected override bool CanPickup(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Player");
    }

    protected override void OnPickup(Collider2D collision)
    {
        //HACK Points
        ScoreManager.AddPoints(1);
    }

    private void SetRandomDestination()
    {
        if (Random.Range(0, 4) > 2)
            direction = Random.insideUnitCircle.normalized;

        destination = GameManager.GetClampedPosition(transform.position + direction);

        direction = (destination - transform.position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            transform.position = destination;
            SetRandomDestination();
            return;
        }

        transform.position += direction * Time.deltaTime * Speed * SpeedFactor;
    }

    public void ChangeSpeed(float factor)
    {
        SpeedFactor = factor;
    }
}
